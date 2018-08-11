import { ToastyService } from 'ng2-toasty';
import { WordService } from './../../services/word.service';

import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-word-view',
  templateUrl: './word-view.component.html',
  styleUrls: ['./word-view.component.css']
})
export class WordViewComponent implements OnInit {
  word: any;
  wordId: number; 

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private toasty: ToastyService,
    private wordService: WordService) { 

    route.params.subscribe(p => {
      this.wordId = +p['id'];
      if (isNaN(this.wordId) || this.wordId <= 0) {
        router.navigate(['/words']);
        return; 
      }
    });
  }

  ngOnInit() {
    this.wordService.getById(this.wordId)
      .subscribe(
        w => this.word = w,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/words']);
            return; 
          }
        });
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.wordService.delete(this.word.id)
        .subscribe(x => {
          this.router.navigate(['/words']);
        });
    }
  }

}
