using AutoMapper;
using Is4.Domain.Repostitory;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.Service.Implement
{
    public class RoleSerivce : IRoleSerivce
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleSerivce(RoleManager<IdentityRole> roleManager, IRoleRepository roleRepository, IMapper mapper)
        {
            _roleManager = roleManager;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<bool>> Create(CreateRoleInput input)
        {
            if ((await _roleManager.FindByNameAsync(input.Name)) != null)
            {
                return new ResponseBase<bool>() { Result = false, Message = "Role Exsit" };
            } 
            var role = _mapper.Map<IdentityRole>(input);
            await _roleManager.CreateAsync(role);
            var claims = _mapper.Map<IList<IdentityRoleClaim<string>>>(input.RoleClaims).Select(a => { a.RoleId = role.Id; return a; }).ToList();
            await _roleRepository.AddClaims(claims);
            return new ResponseBase<bool>() { Result = true };
        }

        public async Task<ResponseBase<PaginatedList<RoleOutput>>> GetList(int pageIndex, int pageSize)
        {
            var list = await _roleRepository.Query().ToListAsync();
            var count = await _roleRepository.Query().CountAsync();
            var output = _mapper.Map<IList<RoleOutput>>(list);
            var claims = await _roleRepository.ClaimQuery().Where(a => list.Select(b => b.Id).Contains(a.RoleId)).ToListAsync();
            foreach (var item in output)
            {
                item.Claims = claims.Where(a => a.RoleId == item.Id).Select(a => new RoleClaimOutput() { ClaimType = a.ClaimType, ClaimValue = a.ClaimValue }).ToList();
            }
            return new ResponseBase<PaginatedList<RoleOutput>>()
            {
                Result = new PaginatedList<RoleOutput>(output, count, pageIndex, pageSize)
            };
        }
    }
}
