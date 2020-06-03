using System.Threading.Tasks;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO.UserRole;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleService _userRoleService;

        public UserRoleController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ResponseBase<bool>> Create(CreateUserRoleInput input)
        {
            return await _userRoleService.Create(input.UserId, input.RoleId);
        }
    }
}
