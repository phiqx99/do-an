import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { TopicComponent } from "./topic.component";

const routes: Routes = [
  {
    path: "",
    component: TopicComponent
  }
];
@NgModule({
  exports: [RouterModule],
  imports: [RouterModule.forChild(routes)]
})
export class TopicRouting {}
