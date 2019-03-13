import { ConfigService } from './common/services/config.service';
import { EventService } from './common/services/event.service';
import { AdminAuthGuard } from './common/services/admin-auth-guard.service';
import { AuthGuard } from './common/services/auth-guard.service';
import { AuthService } from './common/services/auth.service';

import * as Raven from 'raven-js'
import { ToastyModule } from 'ng2-toasty';
import { AUTH_PROVIDERS } from 'angular2-jwt';
import { UiSwitchModule } from 'angular2-ui-switch'

import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';
import { NgModule, ErrorHandler } from '@angular/core';
import { DatePipe } from '@angular/common';

import { AppComponent } from './app.component';
import { AppErrorHandler } from './app.error-hander';

import { NavMenuComponent } from './common/components/nav-menu/nav-menu.component';
import { UnauthorizatedAccessComponent } from './common/components/unauthorizated-access.component';

import { WordsModule } from './modules/words/words.module';
import { AdminModule } from './modules/admin/admin.module';
import { CalendarModule } from './modules/calendar/calendar.module';

import { AppRoutingModule } from './app-routing.module';

Raven
.config('https://3428ccb1649d48e38ad21196ba93758c@sentry.io/1252571')
.install();

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    UnauthorizatedAccessComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    HttpModule,
    HttpClientModule,

    WordsModule,
    AdminModule,
    CalendarModule,
    AppRoutingModule,

    ToastyModule.forRoot(),
    UiSwitchModule
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    AuthService,
    AuthGuard,
    AUTH_PROVIDERS,
    AdminAuthGuard,
    DatePipe,
    ConfigService,
    EventService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
