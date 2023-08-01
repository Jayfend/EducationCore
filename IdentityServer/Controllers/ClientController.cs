using IdentityServer.DTO;
using IdentityServer.Services.Clients;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public async Task<bool> CreateAsync(ClientCreateDTO req)
        {
            return await _clientService.CreateAsync(req);
        }

        [HttpPut("IdentityResources")]
        public async Task<bool> UpdateIdentityResourcesAsync(IdentityResourcesUpdateDTO req)
        {
            return await _clientService.UpdateIdentityResourcesAsync(req);
        }

        [HttpPost("APIResources")]
        public async Task<bool> CreateAPIResourcesAsync(APIResourceCreateDTO req)
        {
            return await _clientService.CreateAPIResourcesAsync(req);
        }

        [HttpPost("ApiScope")]
        public async Task<bool> CreateAPIScopeAsync(APIScopeCreateDTO req)
        {
            return await _clientService.CreateAPIScopeAsync(req);
        }
    }
}