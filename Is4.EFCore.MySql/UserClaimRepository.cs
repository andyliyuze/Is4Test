using Is4.Domain.Repostitory;
using Is4.EFCore.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;

namespace Is4.EFCore.MySql
{
    public class UserClaimRepository : IUserClaimRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserClaimRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<IdentityUserClaim<string>> Query()
        {
            return _dbContext.UserClaims.AsQueryable();
        }
    }
}
