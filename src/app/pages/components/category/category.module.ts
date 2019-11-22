import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CategoryRouting } from './category.routing';
import { CategoryComponent } from './category.component';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    CategoryRouting,
    NgxPaginationModule,
    ReactiveFormsModule

  ],
  declarations: [CategoryComponent],
})
export class CategoryModule { }
