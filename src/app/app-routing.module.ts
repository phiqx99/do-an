import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AdminComponent } from "./layout/admin/admin.component";
import { AuthComponent } from "./layout/auth/auth.component";
import { AuthGuard } from "../app/pages/auth/auth.guard";

const routes: Routes = [
  {
    path: "",
    component: AuthComponent,
    children: [
      {
        path: "",
        redirectTo: "login",
        pathMatch: "full"
      },
      {
        path: "login",
        loadChildren: () =>
          import("./pages/auth/login/login.module").then(n => n.LoginModule)
      },
      {
        path: "register",
        loadChildren: () =>
          import("./pages/auth/register/register.module").then(
            n => n.RegisterModule
          )
      }
    ]
  },
  {
    path: "",
    component: AdminComponent,
    // canActivate: [AuthGuard],
    children: [
      {
        path: "components",
        loadChildren: () =>
          import("./pages/components/components.module").then(
            m => m.ComponentsModule
          )
      },
      {
        path: "userprofile",
        loadChildren: () =>
          import("./pages/auth/user-profile/user-profile.module").then(
            m => m.UserProfileModule
          )
      }
      // {
      //   path: "group",
      //   loadChildren: () =>
      //     import("./pages/components/group/group.module").then(
      //       m => m.GroupModule
      //     )
      // },
      // {
      //   path: "role",
      //   loadChildren: () =>
      //     import("./pages/components/role/role.module").then(m => m.RoleModule)
      // },
      // {
      //   path: "user",
      //   loadChildren: () =>
      //     import("./pages/components/user/user.module").then(m => m.UserModule)
      // }
      // {
      //   path: 'permission',
      //   loadChildren: () => import('./pages/components/permission/permission.module').then(m => m.PermissionModule)
      // },
      // {
      //   path: 'groupuser',
      //   loadChildren: () => import('./pages/components/group-user/group-user.module').then(m => m.GroupUserModule)
      // }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
