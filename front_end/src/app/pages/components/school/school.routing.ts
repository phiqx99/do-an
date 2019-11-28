import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { SchoolComponent } from "./school.component";

const routes: Routes = [
  {
    path: "",
    component: SchoolComponent
  }
];
@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forChild(routes)]
})
export class SchoolRouting {}
