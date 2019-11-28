using Newtonsoft.Json;

namespace BEQLDT.Service.KeycloakService
{
    public class HeaderModel
    {
        [JsonProperty("alg")] public string Alg { get; set; }

        [JsonProperty("typ")] public string Typ { get; set; }

        [JsonProperty("kid")] public string Kid { get; set; }
    }
}
