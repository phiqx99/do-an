import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SharedModule } from "../../../shared/shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { SchoolRouting } from "./school.routing";
import { SchoolComponent } from "./school.component";
import { NgxPaginationModule } from "ngx-pagination";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    SchoolRouting,
    NgxPaginationModule,
    ReactiveFormsModule
  ],
  declarations: [SchoolComponent]
})
export class SchoolModule {}
