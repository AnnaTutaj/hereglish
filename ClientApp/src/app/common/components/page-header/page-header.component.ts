import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'page-header',
  templateUrl: './page-header.component.html',
  styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent implements OnInit {

  @Input('header') header: string ;
  @Input('description') description: string ;

  constructor() {
   }

  ngOnInit() {
  }

}
