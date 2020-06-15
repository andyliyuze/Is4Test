using Is4.Shared;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.Domain.Repostitory
{
    public interface IRoleRepository : IRepository
    {
        IQueryable<IdentityRole> Query();

        IQueryable<IdentityRoleClaim<string>> ClaimQuery();

        Task AddClaims(IList<IdentityRoleClaim<string>> claims);
    }
}
