import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CalendarViewComponent } from './calendar-view/calendar-view.component';

const calendarRoutes: Routes = [
    { path: 'calendar', component: CalendarViewComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(calendarRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class CalendarRoutingModule { }