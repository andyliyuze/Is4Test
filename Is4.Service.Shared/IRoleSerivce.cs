using Is4.Service.Shared.DTO.Role;
using Is4.Shared;
using System.Threading.Tasks;

namespace Is4.Service.Shared
{
    public interface IRoleSerivce : IAppService
    {
        Task<ResponseBase<bool>> Create(CreateRoleInput input);
    }
}
