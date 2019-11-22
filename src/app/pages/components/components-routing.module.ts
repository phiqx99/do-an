import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";

const routes: Routes = [
  {
    path: "userprofile",
    loadChildren: () =>
      import("./../auth/user-profile/user-profile.module").then(
        m => m.UserProfileModule
      )
  },
  {
    path: "group",
    loadChildren: () =>
      import("./../components/group/group.module").then(m => m.GroupModule)
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "groupuser",
    loadChildren: () =>
      import("./../components/group-user/group-user.module").then(
        m => m.GroupUserModule
      )
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "role",
    loadChildren: () =>
      import("./../components/role/role.module").then(m => m.RoleModule)
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "user",
    loadChildren: () =>
      import("./../components/user/user.module").then(m => m.UserModule)
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "council",
    loadChildren: () =>
      import("./../components/council/council.module").then(
        m => m.CouncilModule
      )
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "category",
    loadChildren: () =>
      import("./category/category.module").then(m => m.CategoryModule)
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "period",
    loadChildren: () =>
      import("./period/period.module").then(m => m.PeriodModule)
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "topic",
    loadChildren: () => import("./topic/topic.module").then(m => m.TopicModule)
    // data : {permittedRoles: ['admin']}
  },
  {
    path: "school",
    loadChildren: () =>
      import("./school/school.module").then(m => m.SchoolModule)
    // data : {permittedRoles: ['admin']}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ComponentsRoutingModule {}
