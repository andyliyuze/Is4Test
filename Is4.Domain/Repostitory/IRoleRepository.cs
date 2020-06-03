using Is4.Shared;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Is4.Domain.Repostitory
{
    public interface IRoleRepository : IRepository
    {
        IQueryable<IdentityRole> Query();
    }
}
