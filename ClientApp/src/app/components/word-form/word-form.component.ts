import { PartOfSpeechService } from '../../services/part-of-speech.service';
import { FeatureService } from '../../services/feature.service';
import { CategoryService } from '../../services/category.service';
import { WordService } from '../../services/word.service';
import { ToastyService, ToastyConfig } from "ng2-toasty";
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '../../../../node_modules/@angular/router';
import { Observable } from '../../../../node_modules/rxjs/Observable';

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
  word: any = {
    features: [],
    pronunciation: {}
  };

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
      this.word.id = +p['id'];
    });

    this.toastyConfig.theme = 'bootstrap';
  }

  ngOnInit() {
    if (this.word.id) {
      this.wordService.get(this.word.id)
        .subscribe(w => {
          this.word = w;
        },
          err => {
            if (err.status == 404) {
              this.router.navigate(['']);
            }
          });
    }

    this.categoryService.getCategories().subscribe(categories =>
      this.categories = categories
    );

    this.featureService.getFeatures().subscribe(features =>
      this.features = features
    );

    this.partOfSpeechService.getPartsOfSpeech().subscribe(partsOfSpeech =>
      this.partsOfSpeech = partsOfSpeech
    );
  }

  onCategoryChange() {
    let selectedCategory = this.categories.find(c => c.id == this.word.categoryId);
    this.subcategories = selectedCategory ? selectedCategory.subcategories : [];
    delete this.word.subcategoryId;
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
    this.wordService.create(this.word)
      .subscribe(x => console.log(x));
  }
}