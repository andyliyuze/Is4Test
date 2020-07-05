using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.ApiResource
{
    public class ScopeListOutput
    {
        public string ApiName { get; set; }

        public IList<string> Scopes { get; set; }
    }
}
