import * as Raven from 'raven-js'
import { ErrorHandler, Inject, NgZone, isDevMode } from "../../node_modules/@angular/core";
import { ToastyService } from "../../node_modules/ng2-toasty";

export class AppErrorHandler implements ErrorHandler {

    constructor(
        private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService) {
    }

    handleError(error: any): void {
        if (!isDevMode()) {
            Raven.captureException(error.originalError || error);
        }
        else {
            throw error;
        }

        this.ngZone.run(() => {
            this.toastyService.error({
                title: 'Error',
                msg: 'An unexpected error happened',
                showClose: true,
                timeout: 3000
            });
        });
    }
} 