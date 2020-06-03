using Is4.Shared;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.Domain.Repostitory
{
    public interface IUserRoleRepository: IRepository
    {
        Task AddAsync(IdentityUserRole<string> userRole);

        IQueryable<IdentityUserRole<string>> Query();
    }
}
