using IdentityServer4.EntityFramework.Entities;
using Is4.Shared;
using System.Linq;

namespace Is4.Domain.Repostitory
{
    public interface IIdentityresourceRepository : IRepository
    {
        IQueryable<IdentityResource> Query();
    }
}
