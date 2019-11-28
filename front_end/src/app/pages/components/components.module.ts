import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { ComponentsRoutingModule } from "./components-routing.module";
import { SharedModule } from "../../shared/shared.module";

@NgModule({
  imports: [CommonModule, ComponentsRoutingModule, SharedModule],
  declarations: []
})
export class ComponentsModule {}
