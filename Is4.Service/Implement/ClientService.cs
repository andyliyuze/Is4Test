using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Is4.Common.CustomAttributes;
using Is4.Domain.Repostitory;
using Is4.Domain.Shared;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO;
using Is4.Service.Shared.DTO.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Is4.Service.Implement
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ResponseBase<bool>> Create(CreateClientInput input)
        {
            var client = _mapper.Map<Client>(input);
            client.AllowedGrantTypes = input.AllowedGrantTypes.Select(a => new ClientGrantType() { GrantType = a }).ToList();
            client.RedirectUris = input.RedirectUris.Select(a => new ClientRedirectUri() { RedirectUri = a }).ToList();
            client.AllowedScopes = input.AllowedScopes.Select(a => new ClientScope() { Scope = a }).ToList();
            client.PostLogoutRedirectUris = input.PostLogoutRedirectUris.Select(a => new ClientPostLogoutRedirectUri() { PostLogoutRedirectUri = a }).ToList();
            await _clientRepository.Create(client);
            return new ResponseBase<bool>() { Result = true };
        }

        private void PrepareClientTypeForNewClient(CreateClientInput client)
        {
            switch (client.ClientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.WebHybrid:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Hybrid);
                    break;
                case ClientType.Spa:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Code);
                    client.RequirePkce = true;
                    client.RequireClientSecret = false;
                    break;
                case ClientType.Native:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Implicit);
                    break;
                case ClientType.Machine:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.ResourceOwnerPasswordAndClientCredentials);
                    break;
                case ClientType.Device:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.DeviceFlow);
                    client.RequireClientSecret = false;
                    client.AllowOfflineAccess = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 获取所有授予类型
        /// </summary>
        /// <returns></returns>
        [UnitOfWork(false)]
        public IList<string> GetAllGrantTypes()
        {
            return ClientConsts.GetGrantTypes();
        }

        [UnitOfWork(false)]
        public async Task<ResponseBase<PaginatedList<ClientOuput>>> GetList(int pageIndex, int pageSize)
        {
            var list = await _clientRepository.Query().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await _clientRepository.Query().CountAsync();
            var output = _mapper.Map<IList<ClientOuput>>(list);
            return new ResponseBase<PaginatedList<ClientOuput>>()
            {
                Result = new PaginatedList<ClientOuput>(output, count, pageIndex, pageSize)
            };
        }
    }
}
