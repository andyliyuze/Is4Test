using AutoMapper;
using IdentityServer4.EntityFramework.Entities;
using Is4.Domain;
using Is4.Service.Shared.DTO;
using Is4.Service.Shared.DTO.ApiResource;
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
            CreateMap<CreateApiScopeInput, ApiScope>();
            CreateMap<CreateApiSecretInput, ApiSecret>();

            CreateMap<ApiScope, ApiScopeOutput>();
            CreateMap<ApiSecret, ApiSecretOutput>();
            CreateMap<ApiResource, ApiResourceOutput>();

            CreateMap<IdentityUserClaim<string>, ClaimOutput>().ForMember(a=>a.Type,opt=>opt.MapFrom(b=>b.ClaimType)).ForMember(a => a.Value, opt => opt.MapFrom(b => b.ClaimValue));
            CreateMap<User, GetUserOutput>();

            CreateMap<CreateRoleInput, IdentityRole>();
            
        }
    }
}
