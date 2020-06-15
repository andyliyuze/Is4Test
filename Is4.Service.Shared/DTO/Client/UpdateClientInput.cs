using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.Client
{
    public class UpdateClientInput
    {
        /// <summary>
        /// 指定注销后允许重定向到的uri
        /// </summary>
        public List<string> PostLogoutRedirectUris { get; set; }

        /// <summary>
        /// 指定可与此客户端一起使用的外部idp(如果列表为空，则允许所有idp)。默认为空。
        /// </summary>
        public List<string> IdentityProviderRestrictions { get; set; }

        /// <summary>
        /// 指定允许返回令牌或授权码的uri
        /// </summary>
        public List<string> RedirectUris { get; set; }

        /// <summary>
        /// 获取或设置JavaScript客户端允许的CORS源。
        /// </summary>
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
