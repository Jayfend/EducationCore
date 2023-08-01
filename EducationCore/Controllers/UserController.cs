using EducationCore.Application.Contracts.DTO;
using EducationCore.Application.Contracts.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<bool> CreateAsync(UserCreateDTO req)
        {
            return await _userService.CreateAsync(req);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<UserInfoDTO> GetAsync([FromQuery] string userName)
        {
            return await _userService.GetAsync(userName);
        }
    }
}