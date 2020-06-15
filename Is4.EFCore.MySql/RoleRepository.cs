using Is4.Domain.Repostitory;
using Is4.EFCore.Shared;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.EFCore.MySql
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<IdentityRole> Query()
        {
            return _dbContext.Roles.AsQueryable();
        }

        public IQueryable<IdentityRoleClaim<string>> ClaimQuery()
        {
            return _dbContext.RoleClaims.AsQueryable();
        }

        public async Task AddClaims(IList<IdentityRoleClaim<string>> claims)
        {
            await _dbContext.RoleClaims.AddRangeAsync(claims);
            await _dbContext.SaveChangesAsync();
        }
    }
}
