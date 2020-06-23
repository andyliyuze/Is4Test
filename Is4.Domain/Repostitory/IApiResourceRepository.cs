using IdentityServer4.EntityFramework.Entities;
using Is4.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.Domain.Repostitory
{
    public interface IApiResourceRepository : IRepository
    {
        Task Create(ApiResource apiResource);

        IQueryable<ApiResource> Query();

        Task Update(ApiResource client);
    }
}
