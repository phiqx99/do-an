using RestSharp.Deserializers;

namespace BEQLDT.Service.KeycloakService
{
    public class GrantCodeModel
    {

        [DeserializeAs(Name = "grant_type")]
        public string grant_type { get; set; }

        [DeserializeAs(Name = "code")]

        public string code { get; set; }

        [DeserializeAs(Name = "client_id")]

        public string client_id { get; set; }


        [DeserializeAs(Name = "redirect_uri")]

        public string redirect_uri { get; set; }

    }
}
