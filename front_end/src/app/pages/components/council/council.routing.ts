import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CouncilComponent } from './council.component';

const routes: Routes = [
{
    path: '', component: CouncilComponent
}
];
@NgModule({
    exports: [RouterModule],
    imports: [RouterModule.forChild(routes)]
})
export class CouncilRouting { }
