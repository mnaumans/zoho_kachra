
namespace ZohoIntegrationMVC.Configurations
{
    public class AppSettings
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUri { get; set; }
        public string BaseUrl { get; set; }
        public string Scopes { get; set; }
        public string PhotoUrl { get; set; }
        public string AccessType { get; set; }

        public string UsersUrl { get; set; }
        public string Organization_id { get; set; }
    }
}
