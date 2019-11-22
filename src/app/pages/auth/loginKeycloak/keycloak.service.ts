import { Injectable } from "@angular/core";

import { environment } from "../../../../environments/environment";
import { HttpClient } from "@angular/common/http";

@Injectable()
export class KeycloakService {
  constructor(private http: HttpClient) {}
  readonly rootURL = "https://localhost:44346/api";
  auth: any = {};
  keycloakAuth: any = {
    url: environment.keycloak.url,
    realm: environment.keycloak.realm,
    clientId: environment.keycloak.clientId,
    redirect_uri: environment.keycloak.redirect_uri,
    state: environment.keycloak.state,
    "ssl-required": "external",
    "public-client": true
  };

  gettoken(code: any, state: any) {
    return this.http.get(
      this.rootURL + "/account/loginkeycloak?state=" + state + "&code=" + code
    );
  }

  login() {
    let loginUrl: any;
    loginUrl =
      this.keycloakAuth.url +
      "/realms/SDC-Test/protocol/openid-connect/auth?&client_id=" +
      this.keycloakAuth.clientId +
      "&state=" +
      this.keycloakAuth.state +
      "&redirect_uri=" +
      this.keycloakAuth.redirect_uri +
      "&response_type=code";
    window.location.href = loginUrl;
  }

  logout() {
    let logoutUrl: any;
    logoutUrl =
      this.keycloakAuth.url +
      "/realms/SDC-Test/protocol/openid-connect/logout?redirect_uri=" +
      this.keycloakAuth.redirect_uri;
    window.location.href = logoutUrl;
    window.localStorage.removeItem("userToken");
  }
}
