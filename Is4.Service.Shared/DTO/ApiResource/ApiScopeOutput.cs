﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.ApiResource
{
    public class ApiScopeOutput
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; }
    }
}
