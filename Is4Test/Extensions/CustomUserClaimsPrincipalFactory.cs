using Is4.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Is4Test.Extensions
{
    public class CustomUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>, IUserClaimsPrincipalFactory<User>
    {
        public CustomUserClaimsPrincipalFactory(UserManager<User> userManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {

        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);
            claimsIdentity.AddClaim(new Claim("depId", "123"));
            claimsIdentity.AddClaim(new Claim("userid", user.Id));
            return claimsIdentity;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var result = await base.CreateAsync(user);
            return result;
        }
    }
}
