import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { DatePipe } from '@angular/common';
import { saveAs } from 'file-saver';

declare var $: any;

import { KeyValuePair } from '../../../common/models/KeyValuePair';

import { AuthService } from '../../../common/services/auth.service';
import { CategoryService } from '../shared/category.service';
import { PartOfSpeechService } from '../shared/part-of-speech.service';
import { WordService } from '../shared/word.service';
@Component({
  selector: 'app-word-list',
  templateUrl: './word-list.component.html',
  styleUrls: ['./word-list.component.css']
})
export class WordListComponent implements OnInit {
  private readonly PAGE_SIZE = 20;

  headerText: string = "Words";
  descriptionText: string = "List of words";

  queryResult: any = {};
  categories: any[];
  subcategories: KeyValuePair[];
  partsOfSpeech: KeyValuePair[];
  isNotLearned: any;
  isLearned: any;
  query: any = {
    pageSize: this.PAGE_SIZE
  };
  headers = [
    { title: 'Word', key: 'name', isSortable: true },
    { title: 'Meaning', key: 'meaning', isSortable: true },
    { title: 'UK' },
    { title: 'US' },
    { title: 'Example' },
    { title: 'Subcat.', key: 'subcategory', isSortable: true },
    { title: 'Cat.', key: 'category', isSortable: true },
    { title: 'Created At', key: 'createdAt', isSortable: true },
    {}
  ];

  constructor(
    private wordService: WordService,
    private categoryService: CategoryService,
    private partOfSpeechService: PartOfSpeechService,
    private auth: AuthService,
    private datePipe: DatePipe) { }

  ngOnInit() {
    this.categoryService.getCategories()
      .subscribe(categories => this.categories = categories);

    this.partOfSpeechService.getPartsOfSpeech()
      .subscribe(partsOfSpeech => this.partsOfSpeech = partsOfSpeech);

    this.populateWords();
  }

  ngAfterViewInit() {
    $(document).ready(function () {
      $(function () {
        $('[data-toggle="tooltip"]').tooltip({ trigger: "hover" })

        $(".btn").on("click", function () {
          $("div[role=tooltip]").remove();
        });

      })
    });
  }

  private populateWords() {
    this.wordService.get(this.query)
      .subscribe(result => this.queryResult = result);
  }

  onFilterChange() {
    this.query.page = 1;
    this.populateWords();
  }

  onCategoryChange() {
    this.populateSubcategories();
    delete this.query.subcategoryId;
    this.onFilterChange();
  }

  onChangeLearnedSwitch($event) {
    this.query.isLearned = $event ? true : null
    if (this.query.isLearned) {
      this.isNotLearned = false;
    }
    this.onFilterChange();
  }

  onChangeNotLearnedSwitch($event) {
    this.query.isLearned = $event ? false : null
    if (!this.query.isLearned) {
      this.isLearned = false;
    }
    this.onFilterChange();
  }

  private populateSubcategories() {
    let selectedCategory = this.categories.find(c => c.id == this.query.categoryId);
    this.subcategories = selectedCategory ? selectedCategory.subcategories : [];
  }

  clearFilter() {
    this.query = {
      page: 1,
      pageSize: this.PAGE_SIZE
    };
    this.subcategories = [];
    this.populateWords();
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

  onPageChange(page) {
    this.query.page = page;
    this.populateWords();
  }

  downloadXls() {
    var date = new Date();
    var dateFile = this.datePipe.transform(date, "yyyy-MM-dd");
    var extension = "xls"
    var fileName = "words" + "-" + dateFile + "." + extension;
    this.wordService.getPdf().subscribe(response => {
      saveAs(response, fileName);
    })
  }
}
