using Is4.Service.Shared.DTO.ApiResource;
using Is4.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Is4.Service.Shared
{
    public interface IApiResourceService : IAppService, IAutoUnitOfWork
    {
        Task<ResponseBase<bool>> Create(CreateApiResourceInput createApiResourceInput);

        Task<ResponseBase<PaginatedList<ApiResourceOutput>>> GetList(int pageIndex, int pageCount);

        Task<ResponseBase<bool>> CreateSecret(CreateApiSecretInput createApiSecretInput);

        Task<ResponseBase<List<ScopeListOutput>>> GetAllScopes();
    }
}
