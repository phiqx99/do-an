import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { UserService } from "../../shared/service/userservice.service";
import { AuthGuard } from "../auth/auth.guard";
import { AuthInterceptor } from "../auth/auth.interceptor";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";

import { ComponentsRoutingModule } from "./components-routing.module";
import { SharedModule } from "../../shared/shared.module";
import { GroupComponent } from "./group/group.component";
import { GroupUserComponent } from "./group-user/group-user.component";
import { RoleComponent } from "./role/role.component";
import { UserComponent } from "./user/user.component";
import { PermissionComponent } from "./permission/permission.component";
import { CategoryComponent } from "./category/category.component";
import { CouncilComponent } from "./council/council.component";
import { PeriodComponent } from "./period/period.component";
import { TopicComponent } from "./topic/topic.component";
import { SchoolComponent } from "./school/school.component";

@NgModule({
  imports: [CommonModule, ComponentsRoutingModule, SharedModule],
  declarations: []
})
export class ComponentsModule {}
