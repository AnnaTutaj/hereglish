import { Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from 'ng-fullcalendar';

import { EventService } from '../../../common/services/event.service';

import { Options } from 'fullcalendar';


@Component({
  templateUrl: './calendar-view.component.html',
  styleUrls: ['./calendar-view.component.css']
})

export class CalendarViewComponent implements OnInit {
  calendarOptions: Options;
  displayEvent: any;
  @ViewChild(CalendarComponent) ucCalendar: CalendarComponent;

  headerText = "Calendar";

  constructor(
    private eventService: EventService
  ) { }

  ngOnInit() {
    this.eventService.getEvents().subscribe(data => {
      this.calendarOptions = {
        editable: true,
        eventLimit: false,
        header: {
          left: 'prev,next today',
          center: 'title',
          right: 'month, basicDay, basicWeek, listYear'
        },
        events: data,
        buttonText: {
          listYear: 'year'
        }
      };
    });
  }
}