import { AuthService } from './services/auth.service';
import * as Raven from 'raven-js'
import { CategoryService } from './services/category.service'
import { FeatureService } from './services/feature.service'
import { PartOfSpeechService } from './services/part-of-speech.service'
import { PhotoService } from './services/photo.service'
import { WordService } from './services/word.service'

import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule, BrowserXhr } from '@angular/http';
import { NgModule, ErrorHandler } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ToastyModule } from 'ng2-toasty';
import { FroalaEditorModule, FroalaViewModule } from 'angular-froala-wysiwyg';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { CounterComponent } from './components/counter/counter.component';
import { FetchDataComponent } from './components/fetch-data/fetch-data.component';
import { WordFormComponent } from './components/word-form/word-form.component';
import { AppErrorHandler } from './app.error-hander';
import { WordListComponent } from './components/word-list/word-list.component';
import { PaginationComponent } from './components/shared/pagination.component';
import { WordViewComponent } from './components/word-view/word-view.component';
import { ProgressService, BrowserXhrWithProgress } from './services/progress.service';


Raven
.config('https://3428ccb1649d48e38ad21196ba93758c@sentry.io/1252571')
.install();

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    PaginationComponent,
    WordFormComponent,
    WordListComponent,
    WordViewComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    HttpModule,
    HttpClientModule,
    RouterModule.forRoot([
      { path: '', redirectTo: 'words', pathMatch: 'full' },
      { path: 'home', component: HomeComponent, pathMatch: 'full' },
      { path: 'words/new', component: WordFormComponent },
      { path: 'words/update/:id', component: WordFormComponent },
      { path: 'words/:id', component: WordViewComponent },
      { path: 'words', component: WordListComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ]),
    ToastyModule.forRoot(),
    FroalaEditorModule.forRoot(), FroalaViewModule.forRoot()
  ],
  providers: [
    { provide: ErrorHandler, useClass: AppErrorHandler },
    { provide: BrowserXhr, useClass: BrowserXhrWithProgress },
    AuthService,
    CategoryService,
    FeatureService,
    PartOfSpeechService,
    PhotoService,
    WordService,
    ProgressService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
