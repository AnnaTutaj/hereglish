import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';

@Injectable()
export class EventService {
  data: any = [
    {
      title: 'First event',
      start: '2019-03-01'
    },
    {
      title: 'Second event',
      start: '2019-04-01',
      end: '2019-04-02'
    }
  ];

  public getEvents(): Observable<any> {
    let data = this.data;
    return Observable.of(data);
  }
};
