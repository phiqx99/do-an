using Newtonsoft.Json;
using System;

namespace BEQLDT.Service.KeycloakService
{
    public class GrantModel
    {
        private string _accessToken;

        [JsonProperty("access_token")]
        public string AccessToken
        {
            get => _accessToken;
            set
            {
                _accessToken = value;
                AccessTokenModel = new TokenModel(_accessToken);
            }
        }

        [JsonProperty("expires_in")] public long ExpiresIn { get; set; }

        [JsonProperty("refresh_expires_in")] public long RefreshExpiresIn { get; set; }

        private string _refreshToken;

        [JsonProperty("refresh_token")]
        public string RefreshToken
        {
            get => _refreshToken;
            set
            {
                _refreshToken = value;
                RefreshTokenModel = new TokenModel(_refreshToken);
            }
        }

        [JsonProperty("token_type")] public string TokenType { get; set; }

        [JsonProperty("not-before-policy")] public long NotBeforePolicy { get; set; }

        [JsonProperty("session_state")] public Guid SessionState { get; set; }

        [JsonProperty("scope")] public string Scope { get; set; }

        public bool IsExpired()
        {
            if (AccessTokenModel == null)
                return true;
            return AccessTokenModel.IsExpired();
        }

        public TokenModel AccessTokenModel { get; set; }
        public TokenModel RefreshTokenModel { get; set; }
    }
}
