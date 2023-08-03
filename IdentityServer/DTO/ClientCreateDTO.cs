using IdentityServer4.Models;
using System.ComponentModel.DataAnnotations;
using static IdentityModel.OidcConstants;

namespace IdentityServer.DTO
{
    public class ClientCreateDTO
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public List<string> GrantTypes { get; set; }

        [Required]
        public string ClientSecrets { get; set; }

        [Required]
        public List<string> AllowedScopes { get; set; }

        public List<string> RedirectUris { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public int AccessTokenLifetime { get; set; }
    }
}