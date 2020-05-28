using Is4.Service.Shared.DTO;
using Is4.Shared;
using System.Threading.Tasks;

namespace Is4.Service.Shared
{
    public interface IUserService : IAppService
    {
        Task<ResponseBase<bool>> Create(CreateUserInput input);

        Task<ResponseBase<bool>> CreateClaim(CreateUserClaimInput input);

        Task<ResponseBase<GetUserOutput>> GetUserById(string id);
    }
}
