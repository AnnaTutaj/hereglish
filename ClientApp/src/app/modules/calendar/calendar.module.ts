import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { FullCalendarModule } from 'ng-fullcalendar';

import { CalendarViewComponent } from './calendar-view/calendar-view.component';

import { CalendarRoutingModule } from './calendar-routing.module';
import { CommonComponentsModule } from '../../common/components/common-components.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    FullCalendarModule,
    CalendarRoutingModule,
    CommonComponentsModule
  ],
  declarations: [
    CalendarViewComponent,
  ]
})
export class CalendarModule {
 }