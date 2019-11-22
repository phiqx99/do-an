import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PeriodRouting } from './period.routing';
import { PeriodComponent } from './period.component';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    PeriodRouting,
    NgxPaginationModule,
    ReactiveFormsModule

  ],
  declarations: [PeriodComponent],
})
export class PeriodModule { }
