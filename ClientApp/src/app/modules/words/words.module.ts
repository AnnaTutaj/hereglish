import { NgModule, ErrorHandler } from '@angular/core';
import { CommonModule } from '@angular/common';

import { WordFormComponent } from './word-form/word-form.component';
import { WordListComponent } from './word-list/word-list.component';
import { WordViewComponent } from './word-view/word-view.component';

import { FroalaEditorModule, FroalaViewModule } from 'angular-froala-wysiwyg';
import { FormsModule } from '@angular/forms';
import { UiSwitchModule } from 'angular2-ui-switch'
import { NgSelectModule } from '@ng-select/ng-select';

import { CommonComponentsModule } from './../../common/components/common-components.module';

import { WordsRoutingModule } from './words-routing.module';
import { CategoryService } from './shared/category.service';
import { FeatureService } from './shared/feature.service';
import { PartOfSpeechService } from './shared/part-of-speech.service';
import { PhotoService } from './shared/photo.service';
import { WordService } from './shared/word.service';
import { SubcategoryService } from './shared/subcategory.service';

import { SaveWordResolver } from './shared/resolvers/save-word.resolver';

@NgModule({
  imports: [
    CommonModule,
    WordsRoutingModule,
    FroalaEditorModule,
    FroalaViewModule,
    FormsModule,
    UiSwitchModule,
    NgSelectModule,
    CommonComponentsModule
  ],
  declarations: [
    WordFormComponent,
    WordListComponent,
    WordViewComponent    
  ],
  providers: [
    CategoryService,
    FeatureService,
    PartOfSpeechService,
    PhotoService,
    WordService,
    SubcategoryService,
    SaveWordResolver
  ],
})
export class WordsModule { }