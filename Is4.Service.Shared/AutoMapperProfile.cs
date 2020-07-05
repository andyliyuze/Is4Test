using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Is4.Domain;
using Is4.Service.Shared.DTO;
using Is4.Service.Shared.DTO.ApiResource;
using Is4.Service.Shared.DTO.Client;
using Is4.Service.Shared.DTO.Role;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Is4.Service.Shared
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateUserInput, User>();
            CreateMap<User, GetUserOutput>();
            CreateMap<Claim, ClaimOutput>();

            CreateMap<CreateClientInput, Client>().ForMember(a => a.AllowedGrantTypes, opt => opt.Ignore())
                .ForMember(a => a.PostLogoutRedirectUris, opt => opt.Ignore()).ForMember(a => a.RedirectUris, opt => opt.Ignore())
                   .ForMember(a => a.AllowedCorsOrigins, opt => opt.Ignore()).ForMember(a => a.IdentityProviderRestrictions, opt => opt.Ignore())
               .ForMember(a => a.AllowedScopes, opt => opt.Ignore());

            CreateMap<CreateApiResourceInput, ApiResource>();
            CreateMap<CreateApiScopeInput, ApiResourceScope>();
            CreateMap<CreateApiSecretInput, ApiResourceSecret>();

            CreateMap<ApiResourceScope, ApiScopeOutput>();
            CreateMap<ApiResourceSecret, ApiSecretOutput>();
            CreateMap<ApiResource, ApiResourceOutput>();

            CreateMap<IdentityUserClaim<string>, ClaimOutput>().ForMember(a => a.Type, opt => opt.MapFrom(b => b.ClaimType)).ForMember(a => a.Value, opt => opt.MapFrom(b => b.ClaimValue));
            CreateMap<User, GetUserOutput>();

            CreateMap<CreateRoleInput, IdentityRole>();
            CreateMap<IdentityRole, RoleOutput>();
            CreateMap<RoleClaimInput, IdentityRoleClaim<string>>();

            CreateMap<IdentityServer4.Models.AccessTokenType, string>().ConvertUsing(b => b.ToString());
            CreateMap<ClientScope, string>().ConvertUsing(b => b.Scope);
            CreateMap<ClientPostLogoutRedirectUri, string>().ConvertUsing(b => b.PostLogoutRedirectUri);
            CreateMap<ClientRedirectUri, string>().ConvertUsing(b => b.RedirectUri);
            CreateMap<ClientCorsOrigin, string>().ConvertUsing(b => b.Origin);
            CreateMap<ClientGrantType, string>().ConvertUsing(b => b.GrantType);
            CreateMap<ClientClaim, CreateClientClaimInput>();
            CreateMap<ClientProperty, CreateClientPropertyInput>();

            CreateMap<Client, ClientOuput>().ForMember(a => a.AccessTokenType, opt => opt.MapFrom(b => ((IdentityServer4.Models.AccessTokenType)b.AccessTokenType).ToString()));

        }
    }
}
