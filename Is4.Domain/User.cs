using IdentityServer4.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Is4.Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public IList<IdentityUserClaim<string>> Claims { get; set; }
    }
}
