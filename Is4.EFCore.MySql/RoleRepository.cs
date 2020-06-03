using IdentityServer4.EntityFramework.Interfaces;
using Is4.Domain.Repostitory;
using Is4.EFCore.Shared;
using Microsoft.AspNetCore.Identity;
using System.Linq;

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
    }
}
