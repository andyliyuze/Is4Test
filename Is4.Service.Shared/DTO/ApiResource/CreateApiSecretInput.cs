using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.Shared.DTO.ApiResource
{
    public class CreateApiSecretInput
    {
        public string ApiName { get; set; }

        public string Description { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }

        /// <summary>
        /// hash类型
        /// </summary>
        public int HashType { get; set; }
    }
}
