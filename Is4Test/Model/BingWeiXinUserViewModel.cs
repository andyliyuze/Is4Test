using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Is4Test.Model
{
    public class BingWeiXinUserViewModel
    {
        public string ReturnUrl { get; set; }

        public string WeiXinOpenId { get; set; }

        public string WeiXinNickName { get; set; }

        public string WeiXinHeadUrl { get; set; }
    }

    public class WeiXinUserBindInputModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }

        public string WeiXinOpenId { get; set; }

        public string WeiXinNickName { get; set; }

        public string WeiXinHeadUrl { get; set; }
    }
}
