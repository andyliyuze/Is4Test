using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.ApiResource
{
    public class ApiResourceOutput
    {
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<ApiSecretOutput> Secrets { get; set; }
        public List<ApiScopeOutput> Scopes { get; set; }
    }
}
