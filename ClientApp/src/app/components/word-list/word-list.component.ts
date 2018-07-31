import { Component, OnInit } from '@angular/core';

import { KeyValuePair } from './../../models/KeyValuePair';
import { Word } from '../../models/Word';

import { CategoryService } from './../../services/category.service';
import { PartOfSpeechService } from './../../services/part-of-speech.service';
import { WordService } from './../../services/word.service';

@Component({
  selector: 'app-word-list',
  templateUrl: './word-list.component.html',
  styleUrls: ['./word-list.component.css']
})
export class WordListComponent implements OnInit {
  words: Word[];
  categories: any[];
  subcategories: KeyValuePair[];
  partsOfSpeech: KeyValuePair[];
  filter: any = {};

  constructor(
    private wordService: WordService,
    private categoryService: CategoryService,
    private partOfSpeechService: PartOfSpeechService) { }

  ngOnInit() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);

    this.partOfSpeechService.getPartsOfSpeech()
      .subscribe(partsOfSpeech => this.partsOfSpeech = partsOfSpeech);

    this.wordService.get(this.filter)
      .subscribe(words => this.words = words);

    this.populateWords();
  }

  private populateWords() {
    this.wordService.get(this.filter)
      .subscribe(words => this.words = words);
  }

  onFilterChange() {
    this.populateWords();
  }

  onCategoryChange() {
    this.populateSubcategories();
    delete this.filter.subcategoryId;
    this.onFilterChange();
  }

  private populateSubcategories() {
    let selectedCategory = this.categories.find(c => c.id == this.filter.categoryId);
    this.subcategories = selectedCategory ? selectedCategory.subcategories : [];
  }

  clearFilter() {
    this.filter = {};
    this.subcategories = [];
    this.onFilterChange();
  }

}
