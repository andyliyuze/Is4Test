using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.Client
{
    public class ClientOuput
    {
        public ClientOuput()
        {
            AllowedScopes = new List<string>();
            PostLogoutRedirectUris = new List<string>();
            RedirectUris = new List<string>();
            AllowedCorsOrigins = new List<string>();
            AllowedGrantTypes = new List<string>();
            Claims = new List<CreateClientClaimInput>();
            Properties = new List<CreateClientPropertyInput>();
        }

        public ClientType ClientType { get; set; }

        public int AccessTokenLifetime { get; set; } = 3600;

        public AccessTokenType AccessTokenType { get; set; }

        public bool AllowAccessTokensViaBrowser { get; set; }

        public bool AllowRememberConsent { get; set; } = true;

        public string FrontChannelLogoutUri { get; set; }

        public string BackChannelLogoutUri { get; set; }

        public string ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientUri { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; } = true;

        public int Id { get; set; }

        public string LogoUri { get; set; }

        public bool RequireClientSecret { get; set; } = true;

        /// <summary>
        /// 是否需要经过用户同意
        /// </summary>
        public bool RequireConsent { get; set; } = true;

        public bool RequirePkce { get; set; }

        public List<string> PostLogoutRedirectUris { get; set; }

        /// <summary>
        /// 客户端重定向Uris
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
    }
}