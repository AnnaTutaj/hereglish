import * as Raven from 'raven-js'
import { ErrorHandler, Inject, NgZone, isDevMode } from "@angular/core";
import { ToastyService } from "ng2-toasty";
import { UNAUTHORIZED, BAD_REQUEST, FORBIDDEN, INTERNAL_SERVER_ERROR, NOT_FOUND } from "http-status-codes";

export class AppErrorHandler implements ErrorHandler {

    private readonly UNAUTHORIZED_ERROR_MESSAGE: string = "Speak friend and enter or try login first";
    private readonly FORBIDDEN_ERROR_MESSAGE: string = "Strictly forbidden...";
    private readonly BAD_REQUEST_ERROR_MESSAGE: string = "Ooops! Something went wrong";
    private readonly NOT_FOUND_ERROR_MESSAGE: string = "Hurray! It's next 404 to your NotFoundPages Collection";
    private readonly INTERNAL_SERVER_ERROR: string = "Server gone wild...";
    private readonly DEFAULT_ERROR_MESSAGE: string = "An unexpected error happened";


    constructor(
        private ngZone: NgZone,
        @Inject(ToastyService) private toastyService: ToastyService) {
    };

    handleError(error: any) {
        let httpErrorCode = error.status;
        switch (httpErrorCode) {
            case UNAUTHORIZED:
                this.showError(this.UNAUTHORIZED_ERROR_MESSAGE, error);
                break;
            case FORBIDDEN:
                this.showError(this.FORBIDDEN_ERROR_MESSAGE, error);
                break;
            case BAD_REQUEST:
                this.showError(this.BAD_REQUEST_ERROR_MESSAGE, error);
                break;
            case NOT_FOUND:
                this.showError(this.NOT_FOUND_ERROR_MESSAGE, error);
                break;
            case INTERNAL_SERVER_ERROR:
                this.showError(this.INTERNAL_SERVER_ERROR, error);
                break;
            default:
                this.showError(this.DEFAULT_ERROR_MESSAGE, error);
        }
    }

    private showError(message: string, error: any) {
        this.ngZone.run(() => {
            this.toastyService.error({
                title: 'Error',
                msg: message,
                theme: 'bootstrap',
                showClose: true,
                timeout: 3000
            });
        });

        if (!isDevMode()) {
            Raven.captureException(error.originalError || error);
        }
        else {
            throw error;
        }
    }

} 