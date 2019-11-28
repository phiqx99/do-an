import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { GroupComponent } from './group.component';

const routes: Routes = [
{
    path: '', component: GroupComponent
}
];
@NgModule({
    exports: [RouterModule],
    imports: [RouterModule.forChild(routes)]
})
export class GroupRouting { }
