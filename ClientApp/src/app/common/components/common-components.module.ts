import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PageHeaderComponent } from './page-header/page-header.component';
import { PaginationComponent } from './pagination/pagination.component';

@NgModule({
  imports: [
    CommonModule
  ],
  declarations: [
    PageHeaderComponent,
    PaginationComponent
  ],
  exports: [
    PageHeaderComponent,
    PaginationComponent
  ]
})
export class CommonComponentsModule { }