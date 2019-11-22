import { NgModule } from "@angular/core";
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { RegisterRouting } from './register.routing';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { RegisterComponent } from './register.component';

@NgModule({
    imports:[
        CommonModule,
        SharedModule,
        RegisterRouting,
        FormsModule
    ],
  exports:[RegisterComponent],
  declarations:[RegisterComponent]

})
export class RegisterModule{}