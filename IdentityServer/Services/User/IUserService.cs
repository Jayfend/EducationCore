using IdentityServer.DTO;

namespace IdentityServer.Services.User
{
    public interface IUserService
    {
        Task<bool> CreateAsync(UserRegisterDTO req);
    }
}