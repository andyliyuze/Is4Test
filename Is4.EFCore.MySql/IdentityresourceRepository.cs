using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Is4.Domain.Repostitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Is4.EFCore.MySql
{
    public class IdentityresourceRepository : IIdentityresourceRepository
    {
        private readonly IConfigurationDbContext _dbContext;

        public IdentityresourceRepository(IConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<IdentityResource> Query()
        {
            return _dbContext.IdentityResources.AsQueryable();
        }
    }
}
