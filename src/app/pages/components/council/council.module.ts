import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CouncilRouting } from './council.routing';
import { CouncilComponent } from './council.component';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    CouncilRouting,
    NgxPaginationModule,
    ReactiveFormsModule

  ],
  declarations: [CouncilComponent],
})
export class CouncilModule { }
