using IdentityServer4.EntityFramework.DbContexts;
using Is4.Domain.Repostitory;
using Is4.EFCore.Shared;
using System.Threading.Tasks;

namespace Is4.EFCore.MySql
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConfigurationDbContext _dbContext;

        public UnitOfWork(ConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Commit()
        {
            var i = await _dbContext.SaveChangesAsync();
            return i;
        }
    }
}
