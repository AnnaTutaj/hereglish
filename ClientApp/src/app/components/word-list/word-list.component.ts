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
  query: any = {};

  constructor(
    private wordService: WordService,
    private categoryService: CategoryService,
    private partOfSpeechService: PartOfSpeechService) { }

  ngOnInit() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);

    this.partOfSpeechService.getPartsOfSpeech()
      .subscribe(partsOfSpeech => this.partsOfSpeech = partsOfSpeech);

    this.wordService.get(this.query)
      .subscribe(words => this.words = words);

    this.populateWords();
  }

  private populateWords() {
    this.wordService.get(this.query)
      .subscribe(words => this.words = words);
  }

  onFilterChange() {
    this.populateWords();
  }

  onCategoryChange() {
    this.populateSubcategories();
    delete this.query.subcategoryId;
    this.onFilterChange();
  }

  private populateSubcategories() {
    let selectedCategory = this.categories.find(c => c.id == this.query.categoryId);
    this.subcategories = selectedCategory ? selectedCategory.subcategories : [];
  }

  clearFilter() {
    this.query = {};
    this.subcategories = [];
    this.onFilterChange();
  }

  sortBy(colName) {
    if (this.query.sortBy === colName) {
      this.query.isSortAscending = !this.query.isSortAscending;
    } else {
      this.query.sortBy = colName;
      this.query.isSortAscending = true;
    }
    this.populateWords();
  }

}
