using AutoMapper;
using Is4.Domain;
using Is4.Domain.Repostitory;
using Is4.Service.Shared;
using Is4.Service.Shared.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Is4.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IUserClaimRepository _userClaimRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        public UserService(UserManager<User> userManager, IUserRepository userRepository, IUserClaimRepository userClaimRepository,
            IUserRoleRepository userRoleRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _userManager = userManager;
            _userClaimRepository = userClaimRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        public async Task<ResponseBase<bool>> Create(CreateUserInput input)
        {
            var user = _mapper.Map<User>(input);
            user.HeadUrl = $"/UserHead/{input.UserName}.jpg";
            var directoryPath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "UserHead");
            if (!Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);
            var basePath = Path.Combine(directoryPath, $"{input.UserName}.jpg");
            var result = await _userManager.CreateAsync(user, input.Password);
            //保存图片
            if (result.Succeeded)
            {
                var match = Regex.Match(input.Head, "data:image/(png|jpeg);base64,([\\w\\W]*)$");

                if (match.Success)
                {
                    input.Head = match.Groups[2].Value;
                }

                var photoBytes = Convert.FromBase64String(input.Head);

                using (MemoryStream ms = new MemoryStream(photoBytes, 0, photoBytes.Length))
                {
                    var img = Image.FromStream(ms);

                    SaveThumbImg(img, basePath, 300, 300);
                }
            }
            return new ResponseBase<bool>() { Result = result.Succeeded, Message = string.Join(",", result.Errors.Select(a => a.Description)) };
        }


        /// <summary>
        /// 保存缩略图
        /// </summary>    
        private void SaveThumbImg(Image original, string thumbPath, int maxWidth, int maxHeight)
        {
            Size newSize = ResizeImage(original.Width, original.Height, maxWidth, maxHeight);
            using (Image displayImage = new Bitmap(original, newSize))
            {
                try
                {
                    var by = ImageToBytes(displayImage);
                    System.IO.File.WriteAllBytes(thumbPath, by);
                    displayImage.Save(thumbPath, original.RawFormat);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    original.Dispose();
                }
            }
        }

        /// <summary>
        /// 计算新尺寸
        /// </summary>
        /// <param name="width">原始宽度</param>
        /// <param name="height">原始高度</param>
        /// <param name="maxWidth">最大新宽度</param>
        /// <param name="maxHeight">最大新高度</param>
        /// <returns></returns>
        private Size ResizeImage(int width, int height, int maxWidth, int maxHeight)
        {
            if (maxWidth <= 0)
                maxWidth = width;
            if (maxHeight <= 0)
                maxHeight = height;
            decimal MAX_WIDTH = maxWidth;
            decimal MAX_HEIGHT = maxHeight;
            decimal ASPECT_RATIO = MAX_WIDTH / MAX_HEIGHT;

            int newWidth, newHeight;
            decimal originalWidth = width;
            decimal originalHeight = height;

            if (originalWidth > MAX_WIDTH || originalHeight > MAX_HEIGHT)
            {
                decimal factor;
                if (originalWidth / originalHeight > ASPECT_RATIO)
                {
                    factor = originalWidth / MAX_WIDTH;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
                else
                {
                    factor = originalHeight / MAX_HEIGHT;
                    newWidth = Convert.ToInt32(originalWidth / factor);
                    newHeight = Convert.ToInt32(originalHeight / factor);
                }
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }
            return new Size(newWidth, newHeight);
        }


        public byte[] ImageToBytes(Image image)
        {

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                byte[] buffer = new byte[ms.Length];
                //Image.Save()会改变MemoryStream的Position，需要重新Seek到Begin
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(buffer, 0, buffer.Length);
                return buffer;
            }
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
            var count = await _userRepository.Query().CountAsync();
            var users = await _userRepository.Query().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            var userIds = users.Select(a => a.Id).ToList();
            var cliams = await _userClaimRepository.Query().Where(a => userIds.Contains(a.UserId)).ToListAsync();
            var userRoles = await _userRoleRepository.Query().Where(a => userIds.Contains(a.UserId)).ToListAsync();

            var roleIds = userRoles.Select(a => a.RoleId).ToList();
            var roles = await _roleRepository.Query().Where(a => roleIds.Contains(a.Id)).ToListAsync();

            var output = _mapper.Map<IList<GetUserOutput>>(users);

            foreach (var user in output)
            {
                var userCliams = cliams.Where(a => a.UserId == user.Id).ToList();
                user.Claims = _mapper.Map<IList<ClaimOutput>>(userCliams);
                var userRoleIds = userRoles.Where(a => a.UserId == user.Id).Select(a => a.RoleId).ToList();
                user.Roles = roles.Where(a => userRoleIds.Contains(a.Id)).Select(a => a.Name).ToList();
            }
            return new ResponseBase<PaginatedList<GetUserOutput>>()
            {
                Result = new PaginatedList<GetUserOutput>(output, count, pageIndex, pageSize)
            };
        }

        public async Task<ResponseBase<GetUserOutput>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var claims = await _userManager.GetClaimsAsync(user);
            var output = _mapper.Map<GetUserOutput>(user);
            output.Claims = _mapper.Map<IList<ClaimOutput>>(claims);
            return new ResponseBase<GetUserOutput>() { Result = output };
        }


        public User GetUserByOpenId(string openId)
        {
            var user = _userRepository.Query().Where(a => a.WeiXinOpenId == openId).FirstOrDefault();
            return user;
        }
    }
}
