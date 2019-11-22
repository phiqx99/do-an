import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GroupUserRouting } from './group-user.routing';
import { GroupUserComponent } from './group-user.component';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    GroupUserRouting,
    NgxPaginationModule,
    ReactiveFormsModule

  ],
  declarations: [GroupUserComponent],
})
export class GroupUserModule { }
