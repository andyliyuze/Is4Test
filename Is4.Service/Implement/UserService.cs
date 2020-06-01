using AutoMapper;
using IdentityServer4.Models;
using Is4.Domain;
using Is4.Domain.Repostitory;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Is4.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UserService(UserManager<User> userManager, IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<ResponseBase<bool>> Create(CreateUserInput input)
        {
            var user = _mapper.Map<User>(input);
            var result = await _userManager.CreateAsync(user, input.Password);
            return new ResponseBase<bool>() { Result = result.Succeeded, Message = string.Join(",", result.Errors.Select(a => a.Description)) };
        }

        public async Task<ResponseBase<bool>> CreateClaim(CreateUserClaimInput input)
        {
            var user = await _userManager.FindByIdAsync(input.UserId);
            var claim = new Claim(input.Type, input.Value);
            var result = await _userManager.AddClaimAsync(user, claim);
            return new ResponseBase<bool>() { Result = result.Succeeded, Message = string.Join(",", result.Errors.Select(a => a.Description)) };
        }

        public async Task<ResponseBase<PaginatedList<GetUserOutput>>> GetList(int pageIndex, int pageSize)
        {
            var result = _userRepository.Query().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return null;
        }

        public async Task<ResponseBase<GetUserOutput>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var claims = await _userManager.GetClaimsAsync(user);
            var output = _mapper.Map<GetUserOutput>(user);
            output.Claims = _mapper.Map<IList<ClaimOutput>>(claims);
            return new ResponseBase<GetUserOutput>() { Result = output };
        }
    }
}
