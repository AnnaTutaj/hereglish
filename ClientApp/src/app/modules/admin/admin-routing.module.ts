import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AdminAuthGuard } from '../../common/services/admin-auth-guard.service';

import { AdminViewComponent } from './admin-view/admin-view.component';

const adminRoutes: Routes = [
    { path: 'admin', component: AdminViewComponent, canActivate: [AdminAuthGuard] },
];

@NgModule({
    imports: [
        RouterModule.forChild(adminRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class AdminRoutingModule { }