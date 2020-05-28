using Is4.Common;
using Is4.Service.Shared.DTO;
using Is4.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Is4.Service.Shared
{
    public interface IClientService : IAppService, IAutoUnitOfWork
    {
        Task<ResponseBase<bool>> Create(CreateClientInput input);

        IList<string> GetAllGrantTypes();
    }
}
