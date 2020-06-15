using Microsoft.AspNetCore.Http;

namespace Is4.Service.Shared.DTO
{
    public class CreateUserInput
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IFormFile Head { get; set; }
    }
}
