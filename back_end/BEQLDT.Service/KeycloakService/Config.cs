namespace BEQLDT.Service.KeycloakService
{
    public class Config
    {
        public string Realm { get; set; }
        public string AuthServerUrl { get; set; }
        public bool SslRequire { get; set; }
        public string ClientId { get; set; }
        public string Secret { get; set; }
        public bool IsPublicClient { get; set; }
        public string RedirectUrl { get; set; }


        public string RealmUrl => AuthServerUrl + "/realms/" + Realm;
        public string RealmAdminUrl => AuthServerUrl + "/admin/realms/" + Realm;

        public Config()
        {
            ClientId = "QLDT";
            RedirectUrl = "http://localhost:4200/";
            AuthServerUrl = "https://id.udn.vn:8443/auth";
            Secret = "64616751-25aa-4d12-9a73-12a76600a5ee";
            IsPublicClient = false;
            Realm = "SDC-Test";
            SslRequire = false;
        }
    }
}
