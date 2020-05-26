using Is4.Service;
using Is4.Service.DTO;
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
    }
}
