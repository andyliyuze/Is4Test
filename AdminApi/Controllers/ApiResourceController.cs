using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO.ApiResource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiResourceController : ControllerBase
    {
        private readonly IApiResourceService _apiResourceService;

        public ApiResourceController(IApiResourceService apiResourceService)
        {
            _apiResourceService = apiResourceService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ResponseBase<bool>> Create([FromBody] CreateApiResourceInput input)
        {
            var result = await _apiResourceService.Create(input);
            return result;
        }

        [HttpGet]
        [Route("getList")]
        public async Task<ResponseBase<PaginatedList<ApiResourceOutput>>> GetList(int pageIndex, int pageSize)
        {
            var result = await _apiResourceService.GetList(pageIndex, pageSize);
            return result;
        }
    }
}
