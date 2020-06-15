using IdentityServer4.Models;
using Is4.Service.Shared.DTO.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Is4.Service.Shared.DTO
{
    public class CreateClientInput
    {
        public CreateClientInput()
        {
            AllowedScopes = new List<string>();
            PostLogoutRedirectUris = new List<string>();
            RedirectUris = new List<string>();
            IdentityProviderRestrictions = new List<string>();
            AllowedCorsOrigins = new List<string>();
            AllowedGrantTypes = new List<string>();
            Claims = new List<CreateClientClaimInput>();
            Properties = new List<CreateClientPropertyInput>();
        }

        public ClientType ClientType { get; set; }

        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;

        public int AccessTokenLifetime { get; set; } = 3600;

        public int? ConsentLifetime { get; set; }

        public AccessTokenType AccessTokenType { get; set; }

        public bool AllowAccessTokensViaBrowser { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public int AuthorizationCodeLifetime { get; set; } = 300;

        public string FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public string BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; } = true;

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClientUri { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// 启用本地登陆
        /// </summary>
        public bool EnableLocalLogin { get; set; } = true;

        public int Id { get; set; }

        /// <summary>
        /// ID Token 存活时间
        /// </summary>
        public int IdentityTokenLifetime { get; set; } = 300;

     
        public bool IncludeJwtId { get; set; }
        public string LogoUri { get; set; }

        public string ClientClaimsPrefix { get; set; } = "client_";

        public string PairWiseSubjectSalt { get; set; }

        public string ProtocolType { get; set; } = "oidc";


        public int RefreshTokenExpiration { get; set; } = 1;

        public int RefreshTokenUsage { get; set; } = 1;

        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;

        public bool RequireClientSecret { get; set; } = true;

        /// <summary>
        /// 是否需要经过用户同意
        /// </summary>
        public bool RequireConsent { get; set; } = true;
        public bool RequirePkce { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        /// <summary>
        /// 指定注销后允许重定向到的uri，这里也限制了前端可以设置的post_logout_redirect_uri值
        /// </summary>
        public List<string> PostLogoutRedirectUris { get; set; }

        public List<string> IdentityProviderRestrictions { get; set; }

        /// <summary>
        /// 指定允许返回令牌或授权码的uri，这里也限制了前端可以设置的redirect_uri值
        /// </summary>
        public List<string> RedirectUris { get; set; }

        public List<string> AllowedCorsOrigins { get; set; }

        public List<string> AllowedGrantTypes { get; set; }

        /// <summary>
        /// 客户端允许访问的Scope ，包括ApiScope与IdentityScope
        /// </summary>
        public List<string> AllowedScopes { get; set; }

        /// <summary>
        /// 客户端声明
        /// </summary>
        public List<CreateClientClaimInput> Claims { get; set; }

        /// <summary>
        /// 客户端属性
        /// </summary>
        public List<CreateClientPropertyInput> Properties { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? Updated { get; set; }

        /// <summary>
        /// 上次获取授权时间
        /// </summary>
        public DateTime? LastAccessed { get; set; }

        /// <summary>
        /// 用户单点登陆存货时间
        /// </summary>
        public int? UserSsoLifetime { get; set; }
        public string UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; } = 300;
    }
}
