using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Is4Test.Extensions
{
    public class ImplicitProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //获取IdentityServer给我们定义的Cliams和我们在SignAsync添加的Claims
            var claims = context.Subject.Claims.ToList();

            context.IssuedClaims = claims;

            var userid = claims.Where(a => a.Type.Equals("sub", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (userid != null) 
            {
                context.IssuedClaims.Add(new System.Security.Claims.Claim("userid", userid.Value));
            }
            return Task.CompletedTask;
        }
         

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }
        
    }
}
