import * as _ from 'underscore';
import { SaveWord } from './../../models/SaveWord';
import { Word } from '../../models/Word';
import { PartOfSpeechService } from '../../services/part-of-speech.service';
import { FeatureService } from '../../services/feature.service';
import { CategoryService } from '../../services/category.service';
import { WordService } from '../../services/word.service';
import { ToastyService, ToastyConfig } from "ng2-toasty";
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from '../../../../node_modules/rxjs/Observable';
import '../../../../node_modules/rxjs/add/observable/forkJoin';

@Component({
  selector: 'app-word-form',
  templateUrl: './word-form.component.html',
  styleUrls: ['./word-form.component.css']
})
export class WordFormComponent implements OnInit {
  categories: any[];
  subcategories: any[];
  features: any[];
  partsOfSpeech: any[];
  word: SaveWord = {
    id: 0,
    name: '',
    meaning: '',
    example: '',
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
  froalaOptions: Object;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private categoryService: CategoryService,
    private featureService: FeatureService,
    private partOfSpeechService: PartOfSpeechService,
    private toastyService: ToastyService,
    private toastyConfig: ToastyConfig,
    private wordService: WordService
  ) {
    route.params.subscribe(p => {
      this.word.id = +p['id'] || 0;
    });

    this.toastyConfig.theme = 'bootstrap';
  }

  ngOnInit() {
    this.froalaOptions = {
      placeholderText: "Give an example of using the word in a sentence",
    }

    var sources = [
      this.categoryService.getCategories(),
      this.featureService.getFeatures(),
      this.partOfSpeechService.getPartsOfSpeech()
    ];

    if (this.word.id) {
      sources.push(this.wordService.getById(this.word.id))
    }

    Observable.forkJoin(sources).subscribe(data => {
      this.categories = data[0];
      this.features = data[1];
      this.partsOfSpeech = data[2];
      if (this.word.id) {
        this.setWord(data[3]);
        this.populateSubcategories();
      }
    },
      err => {
        if (err.status == 404) {
          this.router.navigate(['']);
        }
      });
  }

  setWord(w: Word) {
    this.word.id = w.id;
    this.word.name = w.name;
    this.word.meaning = w.meaning;
    this.word.example = w.example;
    this.word.isLearned = w.isLearned;
    this.word.categoryId = w.category.id;
    this.word.subcategoryId = w.subcategory.id;
    this.word.partOfSpeechId = w.partOfSpeech.id;
    this.word.pronunciation = w.pronunciation;
    this.word.features = _.pluck(w.features, 'id');
  }

  onCategoryChange() {
    this.populateSubcategories();
    delete this.word.subcategoryId;
  }

  private populateSubcategories() {
    let selectedCategory = this.categories.find(c => c.id == this.word.categoryId);
    this.subcategories = selectedCategory ? selectedCategory.subcategories : [];
  }

  onFeatureToggle(featureId, $event) {
    if ($event.target.checked) {
      this.word.features.push(featureId);
    }
    else {
      var index = this.word.features.indexOf(featureId);
      this.word.features.splice(index, 1);
    }
  }

  submit() {
    var message = (this.word.id) ? 'The word has been sucessfully updated' : 'The word has been sucessfully added';
    var result$ = (this.word.id) ? this.wordService.update(this.word) : this.wordService.create(this.word);

    result$.subscribe(word => {
      this.toastyService.success({
        title: 'Success',
        msg: message,
        showClose: true,
        timeout: 3000
      });
      this.router.navigate(['/words/', word.id])
    });
  }
}