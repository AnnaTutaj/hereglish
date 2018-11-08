import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthGuard } from '../../common/services/auth-guard.service';
import { WordListComponent } from './word-list/word-list.component';
import { WordViewComponent } from './word-view/word-view.component';
import { WordFormComponent } from './word-form/word-form.component';

const wordsRoutes: Routes = [
    { path: 'words/new', component: WordFormComponent, canActivate: [AuthGuard]},
    { path: 'words/:id', component: WordViewComponent },
    { path: 'words/update/:id', component: WordFormComponent, canActivate: [AuthGuard] },
    { path: 'words', component: WordListComponent },
];

@NgModule({
    imports: [
        RouterModule.forChild(wordsRoutes)
    ],
    exports: [
        RouterModule
    ]
})
export class WordsRoutingModule { }