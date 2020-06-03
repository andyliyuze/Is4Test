using Is4.Shared;
using System.Threading.Tasks;

namespace Is4.Service.Shared
{
    public interface IUserRoleService : IAppService
    {
        Task<ResponseBase<bool>> Create(string userId, string roleId);
    }
}
