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
using MassTransit;
using Is4.Domain.Shared.Events;

namespace Is4.Service.Implement
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public ClientService(IClientRepository clientRepository, IPublishEndpoint publishEndpoint, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

        public async Task<ResponseBase<bool>> AddScope(AddScopeInput input)
        {
            var item = _clientRepository.Query().FirstOrDefault(a => a.Id == input.ClientId);

            if (item == null) { return new ResponseBase<bool>() { Message = "客户端不存在", Result = false }; }

            if (item.AllowedScopes.Any(a => a.Scope.Equals(input.Scope, StringComparison.OrdinalIgnoreCase)))
            {
                return new ResponseBase<bool>() { Message = "客户端请求域已存在", Result = false };
            }
            (item.AllowedScopes ?? new List<ClientScope>()).Add(new ClientScope() { Scope = input.Scope });
            await _clientRepository.Update(item);
            await _publishEndpoint.Publish(new ValueEntered() { Value = item.ClientId });
            return new ResponseBase<bool>() { Result = true };
        }

        private void PrepareClientTypeForNewClient(CreateClientInput client)
        {
            switch (client.ClientType)
            {
                case ClientType.Empty:
                    break;
                case ClientType.Hybrid:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Hybrid);
                    break;
                case ClientType.Spa:
                    client.AllowedGrantTypes.AddRange(IdentityServer4.Models.GrantTypes.Code);
                    client.RequirePkce = true;
                    client.RequireClientSecret = false;
                    break;
                case ClientType.Implicit:
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

        /// <summary>
        /// 获取所有客户端类型，非Is4定义
        /// </summary>
        /// <returns></returns>
        [UnitOfWork(false)]
        public ResponseBase<IList<string>> GetClientTypes()
        {
            var list = new List<string>();
            foreach (var item in Enum.GetValues(typeof(ClientType)))
            {
                list.Add(item.ToString());
            }
            return new ResponseBase<IList<string>>() { Result = list };
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

        public async Task<ResponseBase<ClientOuput>> GetByClientId(string clientId)
        {
            var client = _clientRepository.Query().Where(a => a.ClientId == clientId).FirstOrDefault();
            var output = _mapper.Map<ClientOuput>(client);
            return new ResponseBase<ClientOuput>() { Result = output };
        }
    }
}
