using Is4.Service.Shared;
using Is4.Service.Shared.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.PlatformAbstractions;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("create")]
        public async Task<ResponseBase<bool>> Create([FromForm] CreateUserInput input)
        { 
            var result = await _userService.Create(input);
            return result;
        }

        [Route("createClaim")]
        public async Task<ResponseBase<bool>> CreateClaim([FromBody] CreateUserClaimInput input)
        {
            var result = await _userService.CreateClaim(input);
            return result;
        }

        [HttpGet]
        [Route("getUserById")]
        public async Task<ResponseBase<GetUserOutput>> GetUserById(string id)
        {
            var result = await _userService.GetUserById(id);
            return result;
        }

        [Route("getList")]
        public async Task<ResponseBase<PaginatedList<GetUserOutput>>> GetList(int pageIndex, int pageSize)
        {
            var name = User.Identity.Name;

            var result = await _userService.GetList(pageIndex, pageSize);
            return result;
        }

    }
}
