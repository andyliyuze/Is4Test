using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Interfaces;
using Is4.Domain.Repostitory;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            await _dbContext.Clients.AddAsync(client);
        }

        public IQueryable<Client> Query()
        {
            return _dbContext.Clients.AsQueryable().Include(a => a.AllowedScopes).Include(a => a.RedirectUris).Include(a => a.ClientSecrets).Include(a => a.Claims)
                .Include(a => a.AllowedCorsOrigins).Include(a => a.AllowedGrantTypes).Include(a => a.IdentityProviderRestrictions).Include(a => a.PostLogoutRedirectUris)
                .Include(a => a.Properties).Include(a => a.RedirectUris);
        }

        public async Task Update(Client client)
        {
            _dbContext.Clients.Update(client);
        }
    }
}
