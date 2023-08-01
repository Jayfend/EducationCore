using IdentityServer.DTO;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Services.Clients
{
    public class ClientService : IClientService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ClientService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<bool> CreateAsync(ClientCreateDTO req)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                var newClient = new IdentityServer4.Models.Client()
                {
                    ClientId = req.ClientId,
                    ClientName = req.ClientName,
                    ClientSecrets =  {
                    new IdentityServer4.Models.Secret(req.ClientSecrets.Sha256())
                                },
                    RedirectUris = req.RedirectUris,
                    AllowedGrantTypes = req.GrantTypes,
                    AllowedScopes = req.AllowedScopes,
                    AllowOfflineAccess = req.AllowOfflineAccess
                };
                await _context.Clients.AddAsync(newClient.ToEntity());
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> UpdateIdentityResourcesAsync(IdentityResourcesUpdateDTO req)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                var profile = await _context.IdentityResources.Where(x => x.Name == req.ScopeName.ToLower()).Include(x => x.UserClaims).FirstOrDefaultAsync();

                profile.UserClaims.AddRange(req.UserClaims);

                _context.IdentityResources.Update(
                   profile);
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> CreateAPIResourcesAsync(APIResourceCreateDTO req)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                var apiResource = new IdentityServer4.Models.ApiResource()
                {
                    Name = req.Name,
                    DisplayName = req.DisplayName,
                    Scopes = req.Scopes,
                };

                await _context.ApiResources.AddAsync(
                 apiResource.ToEntity());
                await _context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<bool> CreateAPIScopeAsync(APIScopeCreateDTO req)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                var apiScope = new IdentityServer4.Models.ApiScope()
                {
                    Name = req.Name,
                    DisplayName = req.DisplayName,
                    UserClaims = req.UserClaims
                };

                await _context.ApiScopes.AddAsync(
                 apiScope.ToEntity());
                await _context.SaveChangesAsync();
                return true;
            }
        }
    }
}