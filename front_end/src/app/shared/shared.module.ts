import {NgModule, NO_ERRORS_SCHEMA} from '@angular/core';
import { CommonModule } from '@angular/common';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import {AccordionAnchorDirective, AccordionDirective, AccordionLinkDirective} from './accordion';
import {ToggleFullScreenDirective} from './fullscreen/toggle-fullscreen.directive';
import {MenuItems} from './menu-items/menu-items';
import {PERFECT_SCROLLBAR_CONFIG, PerfectScrollbarConfigInterface, PerfectScrollbarModule} from 'ngx-perfect-scrollbar';
import {ClickOutsideModule} from 'ng-click-outside';
import { PaginationComponent } from './pagination/pagination.component';

const DEFAULT_PERFECT_SCROLLBAR_CONFIG: PerfectScrollbarConfigInterface = {
  suppressScrollX: true
};

@NgModule({
  imports: [
    CommonModule,
    PerfectScrollbarModule,
    ClickOutsideModule,
    NgbModule
  ],
  declarations: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    ToggleFullScreenDirective,
    PaginationComponent
  ],
  exports: [
    AccordionAnchorDirective,
    AccordionLinkDirective,
    AccordionDirective,
    ToggleFullScreenDirective,
    NgbModule,
    PerfectScrollbarModule,
    ClickOutsideModule,
    PaginationComponent
    ],
  providers: [
    MenuItems,
    {
      provide: PERFECT_SCROLLBAR_CONFIG,
      useValue: DEFAULT_PERFECT_SCROLLBAR_CONFIG
    }
  ],
  schemas: [ NO_ERRORS_SCHEMA ]
})
export class SharedModule { }
