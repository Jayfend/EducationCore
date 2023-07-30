using AutoMapper;
using IdentityServer.Data;
using IdentityServer.DTO;
using IdentityServer.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services.User
{
    public class UserService : IUserService
    {
        private readonly AspNetIdentityDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public UserService(AspNetIdentityDbContext context, UserManager<IdentityUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(UserRegisterDTO req)
        {
            var userByEmail = await _userManager.FindByEmailAsync(req.Email);
            var userByName = await _userManager.FindByNameAsync(req.UserName);
            if (userByEmail != null || userByName != null)
            {
                throw new ExceptionHandler(ExceptionCodes.ERROR_EDCORE_ITEM_EXIST, "Tài khoản đã tồn tại");
            }
            var newUser = _mapper.Map<UserRegisterDTO, IdentityUser>(req);
            var result = await _userManager.CreateAsync(newUser, req.Password);
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}