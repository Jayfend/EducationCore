using IdentityServer.DTO;
using IdentityServer.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public Task<bool> CreateAsync(UserRegisterDTO req)
        {
            return _userService.CreateAsync(req);
        }
    }
}