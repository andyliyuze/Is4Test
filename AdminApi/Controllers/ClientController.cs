using Is4.Service.Shared;
using Is4.Service.Shared.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ResponseBase<bool>> Create([FromBody] CreateClientInput input)
        {
            var result = await _clientService.Create(input);
            return result;
        }

        [HttpGet]
        [Route("getAllGrantTypes")]
        public IList<string> GetAllGrantTypes()
        {
            return _clientService.GetAllGrantTypes();
        }
    }
}
