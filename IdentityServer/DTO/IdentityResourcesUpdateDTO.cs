using IdentityServer4.EntityFramework.Entities;

namespace IdentityServer.DTO
{
    public class IdentityResourcesUpdateDTO
    {
        public string ScopeName { get; set; }
        public List<IdentityResourceClaim> UserClaims { get; set; }
    }
}