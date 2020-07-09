using Microsoft.AspNetCore.Identity;

namespace Is4.Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public string HeadUrl { get; set; }

        public string WeiXinOpenId { get; set; }

        public string WeiXinHeadUrl { get; set; }

        public string WeiXinNickName { get; set; }
    }
}
