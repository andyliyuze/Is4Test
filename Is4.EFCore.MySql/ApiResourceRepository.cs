using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Is4.Domain.Repostitory;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.EFCore.MySql
{
    public class ApiResourceRepository : IApiResourceRepository
    {
        private readonly IConfigurationDbContext _dbContext;

        public ApiResourceRepository(IConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Create(ApiResource apiResource)
        {
            await _dbContext.ApiResources.AddAsync(apiResource);
        }

        public IQueryable<ApiResource> Query()
        {
            return _dbContext.ApiResources.AsQueryable().Include(a=>a.Scopes).Include(a=>a.UserClaims).Include(a=>a.Secrets).Include(a=>a.Properties);
        }
    }
}
