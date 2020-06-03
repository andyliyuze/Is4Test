using AutoMapper;
using Is4.Domain;
using Is4.Domain.Repostitory;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Is4.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(UserManager<User> userManager, IUserRepository userRepository, IUserClaimRepository userClaimRepository,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userManager = userManager;
            _userClaimRepository = userClaimRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ResponseBase<bool>> Create(CreateUserInput input)
        {
            var user = _mapper.Map<User>(input);
            var result = await _userManager.CreateAsync(user, input.Password);
            return new ResponseBase<bool>() { Result = result.Succeeded, Message = string.Join(",", result.Errors.Select(a => a.Description)) };
        }

        public async Task<ResponseBase<bool>> CreateClaim(CreateUserClaimInput input)
        {
            var user = await _userManager.FindByIdAsync(input.UserId);
            var claim = new Claim(input.Type, input.Value);
            var result = await _userManager.AddClaimAsync(user, claim);
            return new ResponseBase<bool>() { Result = result.Succeeded, Message = string.Join(",", result.Errors.Select(a => a.Description)) };
        }

        public async Task<ResponseBase<PaginatedList<GetUserOutput>>> GetList(int pageIndex, int pageSize)
        {
            var count = await _userRepository.Query().CountAsync();
            var users = await _userRepository.Query().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
          
            var userIds = users.Select(a => a.Id).ToList();
            var cliams = await _userClaimRepository.Query().Where(a => userIds.Contains(a.UserId)).ToListAsync();
            var userRoles = await _userRoleRepository.Query().Where(a => userIds.Contains(a.UserId)).ToListAsync();

            var roleIds = userRoles.Select(a => a.RoleId).ToList();
            var roles = await _roleRepository.Query().Where(a => roleIds.Contains(a.Id)).ToListAsync();

            var output = _mapper.Map<IList<GetUserOutput>>(users);

            foreach (var user in output)
            { 
                var userCliams = cliams.Where(a => a.UserId == user.Id).ToList();
                user.Claims = _mapper.Map<IList<ClaimOutput>>(userCliams);
                var userRoleIds = userRoles.Where(a => a.UserId == user.Id).Select(a=>a.RoleId).ToList();
                user.Roles = roles.Where(a => userRoleIds.Contains(a.Id)).Select(a => a.Name).ToList();
            }
            return new ResponseBase<PaginatedList<GetUserOutput>>()
            {
                Result = new PaginatedList<GetUserOutput>(output, count, pageIndex, pageSize)
            };
        }

        public async Task<ResponseBase<GetUserOutput>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var claims = await _userManager.GetClaimsAsync(user);
            var output = _mapper.Map<GetUserOutput>(user);
            output.Claims = _mapper.Map<IList<ClaimOutput>>(claims);
            return new ResponseBase<GetUserOutput>() { Result = output };
        }
    }
}
