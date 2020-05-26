using Is4.Service.DTO;
using System.Threading.Tasks;

namespace Is4.Service
{
    public interface IUserService
    {
        Task<ResponseBase<bool>> Create(CreateUserInput input);
    }
}
