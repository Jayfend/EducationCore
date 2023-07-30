using System.ComponentModel.DataAnnotations;

namespace IdentityServer.DTO
{
    public class UserRegisterDTO
    {
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}