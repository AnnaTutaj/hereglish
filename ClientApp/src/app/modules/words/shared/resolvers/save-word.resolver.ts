import * as _ from 'underscore';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, Router } from '@angular/router';
import { WordService } from '../word.service';
import { SaveWord } from '../../../../common/models/SaveWord';
import { Word } from '../../../../common/models/Word';


@Injectable()
export class SaveWordResolver implements Resolve<any> {

    defaultWord: SaveWord = {
        id: 0,
        name: '',
        meaning: '',
        definition: '',
        example: '',
        link: '',
        isLearned: false,
        categoryId: 0,
        subcategoryId: 0,
        partOfSpeechId: 0,
        features: [],
        pronunciation: {
            Uk: '',
            Us: ''
        }
    };

    word = Object.assign({}, this.defaultWord);

    constructor(
        private wordService: WordService,
        private router: Router,
    ) { }

    resolve(route: ActivatedRouteSnapshot) {
        if (route.params && route.params.id) {
            return new Promise((resolve) => {
                this.wordService.getById(route.params.id)
                    .subscribe(
                        w => {
                            this.setWord(w);
                            resolve(this.word);
                            return this.defaultWord;
                        },
                        err => {
                            if (err.status == 404) {
                                this.router.navigate(['/words']);
                                return;
                            }
                        });
            })
        }

        if (route.queryParams && route.queryParams.model) {
            const word = JSON.parse(route.queryParams.model);
            this.setWord(word);
            return this.word;
        }

        return this.defaultWord;
    }


    setWord(w: Word) {
        this.word.id = w.id;
        this.word.name = w.name;
        this.word.meaning = w.meaning;
        this.word.definition = w.definition;
        this.word.example = w.example;
        this.word.link = w.link;
        this.word.isLearned = w.isLearned;
        this.word.categoryId = w.category.id;
        this.word.subcategoryId = w.subcategory.id;
        this.word.partOfSpeechId = w.partOfSpeech.id;
        this.word.pronunciation = w.pronunciation;
        this.word.features = _.pluck(w.features, 'id');
    }
}
