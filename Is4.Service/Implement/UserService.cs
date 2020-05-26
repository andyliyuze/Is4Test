using AutoMapper;
using Is4.Domain;
using Is4.Service.DTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Is4.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, IMapper mapper)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ResponseBase<bool>> Create(CreateUserInput input)
        {
            var user = _mapper.Map<User>(input);
            var result = await _userManager.CreateAsync(user, input.Password);
            return new ResponseBase<bool>() { Data = result.Succeeded, Message = string.Join(",", result.Errors.Select(a => a.Description)) };
        }
    }
}
