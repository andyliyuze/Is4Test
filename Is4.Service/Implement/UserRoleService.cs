using Is4.Domain;
using Is4.Domain.Repostitory;
using Is4.Service.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Is4.Service.Implement
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }
         
        public async Task<ResponseBase<bool>> Create(string userId, string roleId)
        {
            await _userRoleRepository.AddAsync(new IdentityUserRole<string>() { RoleId = roleId, UserId = userId });
            return new ResponseBase<bool>() { Result = true };
        }
    }
}
