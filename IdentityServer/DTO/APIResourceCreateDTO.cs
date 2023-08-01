namespace IdentityServer.DTO
{
    public class APIResourceCreateDTO
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<string> Scopes { get; set; }
    }
}