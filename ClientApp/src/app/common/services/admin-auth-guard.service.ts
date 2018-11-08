import { AuthGuard } from './auth-guard.service';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';

@Injectable()
export class AdminAuthGuard extends AuthGuard {

    constructor(auth: AuthService) {
        super(auth);
    }

    canActivate() {
        var isAuthenticated = super.canActivate();

        if (!isAuthenticated) {
            return false;
        }

        if (!this.auth.isInRole('Admin')) {
            this.auth.unauthorizedAccess();
            return false;
        }

        return true;
    }
}