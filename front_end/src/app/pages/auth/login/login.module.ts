import { LoginRouting } from "./login.routing";
import { NgModule } from "@angular/core";
import {
  CommonModule,
  LocationStrategy,
  HashLocationStrategy
} from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SharedModule } from "../../../shared/shared.module";
import { KeycloakService } from "../loginKeycloak/keycloak.service";
import { LoginComponent } from "./login.component";
@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    LoginRouting,
    ReactiveFormsModule
  ],
  exports: [LoginComponent],
  declarations: [LoginComponent],
  providers: [
    KeycloakService,
    {
      provide: LocationStrategy,
      useClass: HashLocationStrategy
    }
  ]
})
export class LoginModule {}
