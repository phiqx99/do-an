using Newtonsoft.Json;
using System;

namespace BEQLDT.Service.KeycloakService
{
    public class PayloadModel
    {
        [JsonProperty("jti")] public Guid Jti { get; set; }

        [JsonProperty("exp")] public long Exp { get; set; }

        [JsonProperty("nbf")] public long Nbf { get; set; }

        [JsonProperty("iat")] public long Iat { get; set; }

        [JsonProperty("iss")] public Uri Iss { get; set; }

        [JsonProperty("aud")] public string Aud { get; set; }

        [JsonProperty("sub")] public string Sub { get; set; }

        [JsonProperty("typ")] public string Typ { get; set; }

        [JsonProperty("azp")] public string Azp { get; set; }

        [JsonProperty("auth_time")] public long AuthTime { get; set; }

        [JsonProperty("session_state")] public Guid SessionState { get; set; }

        [JsonProperty("acr")] public long Acr { get; set; }

        [JsonProperty("realm_access")] public RealmAccess RealmAccess { get; set; }

        [JsonProperty("resource_access")] public ResourceAccess ResourceAccess { get; set; }

        [JsonProperty("scope")] public string Scope { get; set; }

        [JsonProperty("email_verified")] public bool EmailVerified { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("preferred_username")] public string PreferredUsername { get; set; }

        [JsonProperty("locale")] public string Locale { get; set; }

        [JsonProperty("given_name")] public string GivenName { get; set; }

        [JsonProperty("family_name")] public string FamilyName { get; set; }

        [JsonProperty("email")] public string Email { get; set; }
    }

    public partial class RealmAccess
    {
        [JsonProperty("roles")] public string[] Roles { get; set; }
    }

    public partial class ResourceAccess
    {
        [JsonProperty("account")] public RealmAccess Account { get; set; }
    }
}
