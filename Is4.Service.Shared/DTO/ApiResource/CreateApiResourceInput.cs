using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.ApiResource
{
    public class CreateApiResourceInput
    {       
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<CreateApiSecretInput> Secrets { get; set; }
        public List<CreateApiScopeInput> Scopes { get; set; }       
    }
}
