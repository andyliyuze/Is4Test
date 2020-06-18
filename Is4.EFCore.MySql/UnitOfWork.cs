using IdentityServer4.EntityFramework.DbContexts;
using Is4.Domain.Repostitory;
using System.Threading.Tasks;
using IdentityServer4.EntityFramework.Interfaces;
namespace Is4.EFCore.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfigurationDbContext _dbContext;

        public UnitOfWork(IConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Commit()
        {
            var i = await ((ConfigurationDbContext)_dbContext).SaveChangesAsync();
            return i;
        }
    }
}
