import { Component, OnInit } from '@angular/core';
import { CategoryService } from './../../services/category.service';
import { WordService } from './../../services/word.service';
import { KeyValuePair } from './../../models/KeyValuePair';
import { Word } from '../../models/Word';

@Component({
  selector: 'app-word-list',
  templateUrl: './word-list.component.html',
  styleUrls: ['./word-list.component.css']
})
export class WordListComponent implements OnInit {
  words: Word[];
  categories: KeyValuePair[];
  filter: any = {};

  constructor(
    private wordService: WordService,
    private categoryService: CategoryService) { }

  ngOnInit() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);
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

  clearFilter(){
    this.filter = {};
    this.onFilterChange();
  }

}
