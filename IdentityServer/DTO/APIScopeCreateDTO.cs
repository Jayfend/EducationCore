namespace IdentityServer.DTO
{
    public class APIScopeCreateDTO
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> UserClaims { get; set; }
    }
}