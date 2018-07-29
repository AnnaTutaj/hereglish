import { PartOfSpeechService } from '../../services/part-of-speech.service';
import { FeatureService } from '../../services/feature.service';
import { CategoryService } from '../../services/category.service';
import { Component, OnInit } from '@angular/core';

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
    private categoryService: CategoryService,
    private featureService: FeatureService,
    private partOfSpeechService: PartOfSpeechService) { }

  ngOnInit() {
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

}
