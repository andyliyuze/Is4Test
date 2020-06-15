using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.Role
{
    public class RoleOutput
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IList<RoleClaimOutput> Claims { get; set; }
    }
}
