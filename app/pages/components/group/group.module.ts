import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GroupRouting } from './group.routing';
import { GroupComponent } from './group.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { GridModule } from '@progress/kendo-angular-grid';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    GroupRouting,
    NgxPaginationModule,
    ReactiveFormsModule,
    GridModule,

  ],
  declarations: [GroupComponent],
})
export class GroupModule { }
