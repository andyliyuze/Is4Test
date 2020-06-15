using IdentityServer4.EntityFramework.Entities;
using Is4.Shared;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.Domain.Repostitory
{
    public interface IClientRepository: IRepository
    {
        Task Create(Client client);

        IQueryable<Client> Query();
    }
}
