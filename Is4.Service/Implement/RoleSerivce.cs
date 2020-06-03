using AutoMapper;
using Is4.Domain;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO.Role;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Is4.Service.Implement
{
    public class RoleSerivce : IRoleSerivce
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleSerivce(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
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
            return new ResponseBase<bool>() { Result = true };
        }
    }
}
