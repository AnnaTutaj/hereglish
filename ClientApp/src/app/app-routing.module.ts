import { NgModule }              from '@angular/core';
import { RouterModule, Routes }  from '@angular/router';

import { UnauthorizatedAccessComponent } from './common/components/unauthorizated-access.component';

const appRoutes: Routes = [
  { path: '', redirectTo: 'words', pathMatch: 'full' },
  { path: 'unauthorized-access', component: UnauthorizatedAccessComponent },

];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      // { enableTracing: true } // <-- uncomment to debugging purposes only
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {}