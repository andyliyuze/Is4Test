using Is4.Domain.Repostitory;
using Is4.EFCore.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.EFCore.MySql
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(IdentityUserRole<string> userRole)
        {
            await _dbContext.UserRoles.AddAsync(userRole);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<IdentityUserRole<string>> Query()
        {
            return _dbContext.UserRoles.AsQueryable();
        }
    }
}
