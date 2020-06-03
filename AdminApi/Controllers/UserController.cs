using IdentityServer4.EntityFramework.Entities;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("create")]
        public async Task<ResponseBase<bool>> Create([FromBody] CreateUserInput input)
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

        [HttpGet]
        [Route("getList")]
        public async Task<ResponseBase<PaginatedList<GetUserOutput>>> GetList(int pageIndex, int pageSize)
        {
            var result = await _userService.GetList(pageIndex, pageSize);
            return result;
        }

    }
}
