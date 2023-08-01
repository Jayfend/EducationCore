using IdentityServer.Data;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace IdentityServer.Services.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory;

        public ProfileService(UserManager<IdentityUser> userManager, IUserClaimsPrincipalFactory<IdentityUser> claimsFactory)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            // Lấy thông tin về người dùng từ nguồn dữ liệu của bạn (database, API, ...).
            // Sau đó, thêm thông tin vào danh sách Claims.
            var userId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(userId);

            var principal = await _claimsFactory.CreateAsync(user);
            var claims = principal.Claims.ToList();

            //    claims.AddRange(new List<Claim>
            //{
            //    new Claim("sub", context.Subject.GetSubjectId()),
            //    new Claim("email", user.Email),
            //    new Claim("name",user.UserName)// Thông tin email của người dùng
            //    // Các thông tin xác thực khác của người dùng
            //});
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            // Thêm các Claims vào context để IdentityServer trả về cho client.
            context.IssuedClaims.AddRange(claims.Distinct());
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            // Kiểm tra tính trạng hoạt động của người dùng.
            // Nếu người dùng không hoạt động, bạn có thể đặt IsActive = false.

            context.IsActive = true;

            return Task.CompletedTask;
        }
    }
}