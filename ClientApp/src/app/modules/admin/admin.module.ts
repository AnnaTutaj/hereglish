import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChartsModule } from 'ng2-charts';

import { AdminViewComponent } from './admin-view/admin-view.component';

import { AdminRoutingModule } from './admin-routing.module';

@NgModule({
  imports: [
    CommonModule,
    ChartsModule,
    AdminRoutingModule,
  ],
  declarations: [
    AdminViewComponent,
  ]
})
export class AdminModule { }