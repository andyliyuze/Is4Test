using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Is4.Service.Shared.DTO
{
    public class GetUserOutput
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public IList<ClaimOutput> Claims { get; set; }

        public IList<string> Roles { get; set; }
    }
}
