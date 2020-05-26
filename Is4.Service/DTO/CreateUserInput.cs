using System;
using System.Collections.Generic;
using System.Text;

namespace Is4.Service.DTO
{
    public class CreateUserInput
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
