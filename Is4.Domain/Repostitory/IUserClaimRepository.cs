using Is4.Shared;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Is4.Domain.Repostitory
{
    public interface IUserClaimRepository : IRepository
    {
        IQueryable<IdentityUserClaim<string>> Query();
    }
}
