import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RoleRouting } from './role.routing';
import { RoleComponent } from './role.component';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    RoleRouting,
    NgxPaginationModule,
    ReactiveFormsModule

  ],
  declarations: [RoleComponent],
})
export class RoleModule { }
