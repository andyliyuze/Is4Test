using Is4.Domain;
using Is4.Domain.Repostitory;
using Is4.EFCore.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Is4.EFCore.MySql
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<User> Query()
        {
            return _dbContext.Users.AsQueryable();
        }
    }
}
