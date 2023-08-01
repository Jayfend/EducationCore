using IdentityServer.DTO;

namespace IdentityServer.Services.Clients
{
    public interface IClientService
    {
        public Task<bool> CreateAsync(ClientCreateDTO req);

        public Task<bool> UpdateIdentityResourcesAsync(IdentityResourcesUpdateDTO req);

        public Task<bool> CreateAPIResourcesAsync(APIResourceCreateDTO req);

        public Task<bool> CreateAPIScopeAsync(APIScopeCreateDTO req);
    }
}