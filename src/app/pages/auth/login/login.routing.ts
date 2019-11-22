import { LoginComponent } from './login.component';
import { Routes, RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';

const routes: Routes = [
    {
        path: '', component: LoginComponent
    }
];
@NgModule({
exports: [RouterModule],
imports: [RouterModule.forChild(routes)]
})
export class LoginRouting { }
