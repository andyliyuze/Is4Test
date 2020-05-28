using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Is4.Domain.Repostitory;
using System.Threading.Tasks;

namespace Is4.EFCore.MySql
{
    public class ClientRepository : IClientRepository
    {
        private readonly IConfigurationDbContext _dbContext;

        public ClientRepository(IConfigurationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Client client)
        {
            var ss = await _dbContext.Clients.AddAsync(client);
        }
    }
}
