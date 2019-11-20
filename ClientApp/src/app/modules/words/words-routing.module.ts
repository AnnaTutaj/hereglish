import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SaveWordResolver } from './shared/resolvers/save-word.resolver';

import { AuthGuard } from '../../common/services/auth-guard.service';
import { WordListComponent } from './word-list/word-list.component';
import { WordViewComponent } from './word-view/word-view.component';
import { WordFormComponent } from './word-form/word-form.component';

const wordsRoutes: Routes = [
    {
        path: 'words/new',
        component: WordFormComponent,
        canActivate: [AuthGuard],
        resolve: {
            word: SaveWordResolver
        }
    },
    {
        path: 'words/:id',
        component: WordViewComponent,
        resolve: {
            word: SaveWordResolver
        }
    },
    {
        path: 'words/update/:id',
        component: WordFormComponent,
        canActivate: [AuthGuard],
        resolve: {
            word: SaveWordResolver
        }
    },
    {
        path: 'words',
        component: WordListComponent
    },
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