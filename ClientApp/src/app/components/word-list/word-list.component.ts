import { WordService } from './../../services/word.service';
import { Component, OnInit } from '@angular/core';
import { Word } from '../../models/Word';

@Component({
  selector: 'app-word-list',
  templateUrl: './word-list.component.html',
  styleUrls: ['./word-list.component.css']
})
export class WordListComponent implements OnInit {
  words: Word[];

  constructor(private wordService: WordService) { }

  ngOnInit() {
    this.wordService.get()
    .subscribe(words => this.words = words);
  }

}
