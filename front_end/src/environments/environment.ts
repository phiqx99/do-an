import { KeycloakConfig } from "keycloak-angular";

// tslint:disable-next-line:max-line-length
const r =
  Math.random()
    .toString(36)
    .substring(2, 20) +
  Math.random()
    .toString(36)
    .substring(2, 20) +
  Math.random()
    .toString(36)
    .substring(2, 20) +
  Math.random()
    .toString(36)
    .substring(2, 20);
const random = r.slice(0, 32);
// Add here your keycloak setup infos
const keycloakConfig: KeycloakConfig = {
  url: "https://id.udn.vn:8443/auth",
  realm: "SDC-Test",
  clientId: "QLDT",
  state: random,
  redirect_uri: "http://localhost:4200/",
  credentials: {
    secret: "64616751-25aa-4d12-9a73-12a76600a5eet"
  }
};

export const environment = {
  keycloak: keycloakConfig,
  apiUrl: "https://localhost:44346/api/",
  production: false
};
