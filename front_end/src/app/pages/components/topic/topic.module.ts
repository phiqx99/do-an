import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SharedModule } from "../../../shared/shared.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { TopicRouting } from "./topic.routing";
import { TopicComponent } from "./topic.component";
import { NgxPaginationModule } from "ngx-pagination";

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    TopicRouting,
    NgxPaginationModule,
    ReactiveFormsModule
  ],
  declarations: [TopicComponent]
})
export class TopicModule {}
