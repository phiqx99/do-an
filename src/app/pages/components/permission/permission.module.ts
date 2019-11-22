import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PermissionRouting } from './permission.routing';
import { PermissionComponent } from './permission.component';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    PermissionRouting,
    NgxPaginationModule,
    ReactiveFormsModule

  ],
  declarations: [PermissionComponent],
})
export class PermissionModule { }
