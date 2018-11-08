import { BrowserXhr } from '@angular/http';
import { NgZone } from '@angular/core';
import { ToastyService } from 'ng2-toasty';
import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../../../common/services/auth.service';
import { BrowserXhrWithProgress } from '../shared/progress.service';
import { PhotoService } from '../shared/photo.service';
import { ProgressService } from '../shared/progress.service';
import { WordService } from '../shared/word.service';

@Component({
  selector: 'app-word-view',
  templateUrl: './word-view.component.html',
  styleUrls: ['./word-view.component.css'],
  providers: [
    { provide: BrowserXhr, useClass: BrowserXhrWithProgress },
    ProgressService
  ]
})
export class WordViewComponent implements OnInit {
  @ViewChild('fileInput') fileInput: ElementRef;
  word: any;
  wordId: number;
  photos: any[];
  progress: any;

  constructor(
    private zone: NgZone,
    private route: ActivatedRoute,
    private router: Router,
    private toasty: ToastyService,
    private photoService: PhotoService,
    private wordService: WordService,
    private progressService: ProgressService,
    private auth: AuthService) {

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

    this.photoService.getPhotos(this.wordId)
      .subscribe(photos => this.photos = photos);
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.wordService.delete(this.word.id)
        .subscribe(x => {
          this.router.navigate(['/words']);
        });
    }
  }

  uploadPhoto() {
    this.progressService.startTracking()
    //fixme progress bar doesn't appears
      .subscribe(progress => {
        this.zone.run(() => {
          this.progress = progress;
        });
      },
        null,
        () => {
          this.progress.null
        });
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;
    var file = nativeElement.files[0];

    this.photoService.upload(this.wordId, file)
    
      .subscribe(photo => {
        this.photos.push(photo);
        nativeElement.value = '';
      },
        err => {
          this.toasty.error({
            title: 'Error',
            msg: err.text(),
            theme: 'bootstrap',
            showClose: true,
            timeout: 3000
          });
        });

  }

}