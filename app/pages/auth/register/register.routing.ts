import {RegisterComponent} from './register.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes:Routes =[
{
    path:'', component:RegisterComponent
}
];
@NgModule({
    exports:[RouterModule],
    imports:[RouterModule.forChild(routes)]
})  
export class RegisterRouting{}