using System.Threading.Tasks;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleSerivce _roleSerivce;

        public RoleController(IRoleSerivce roleSerivce)
        {
            _roleSerivce = roleSerivce;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ResponseBase<bool>> Create(CreateRoleInput input)
        {
            return await _roleSerivce.Create(input);
        }

        [HttpGet]
        [Route("getList")]
        [AllowAnonymous]
        public async Task<ResponseBase<PaginatedList<RoleOutput>>> GetList(int pageIndex, int pageSize)
        {
            return await _roleSerivce.GetList(pageIndex, pageSize);
        }
    }
}
