import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SharedModule } from "../../../shared/shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { UserRouting } from "./user.routing";
import { UserComponent } from "./user.component";
import { NgxPaginationModule } from "ngx-pagination";
import { DatePipe } from "@angular/common";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    UserRouting,
    NgxPaginationModule,
    ReactiveFormsModule
  ],
  providers: [DatePipe],
  declarations: [UserComponent]
})
export class UserModule {}
