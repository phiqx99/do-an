using Newtonsoft.Json;
using System;

namespace BEQLDT.Service.KeycloakService
{
    public class TokenModel
    {
        public TokenModel(string data)
        {
            var token = data.Split('.');
            Header = JsonConvert.DeserializeObject<HeaderModel>(UserService.Base64Decode(token[0]));
            Payload = JsonConvert.DeserializeObject<PayloadModel>(UserService.Base64Decode(token[1]));
            Signature = token[2];
            Signed = token[0] + ":" + token[1];
        }

        public HeaderModel Header { get; set; }
        public PayloadModel Payload { get; set; }
        public string Signature { get; set; }
        public string Signed { get; set; }

        public bool IsExpired()
        {
            var currentTime = DateTime.Now.Ticks;
            return Payload.Exp < currentTime || Payload.Iat < currentTime - 86400;
        }
    }
}
