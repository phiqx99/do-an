import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GroupUserComponent } from './group-user.component';

const routes: Routes = [
{
    path: '', component: GroupUserComponent
}
];
@NgModule({
    exports: [RouterModule],
    imports: [RouterModule.forChild(routes)]
})
export class GroupUserRouting { }
