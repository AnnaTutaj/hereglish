import { NgZone } from '@angular/core';
import { PhotoService } from '../../services/photo.service';
import { ToastyService } from 'ng2-toasty';
import { WordService } from '../../services/word.service';

import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProgressService } from '../../services/progress.service';

@Component({
  selector: 'app-word-view',
  templateUrl: './word-view.component.html',
  styleUrls: ['./word-view.component.css']
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
    private progressService: ProgressService) {

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
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this.progressService.uploadProgress
      .subscribe(progress => {
        console.log(progress)
        this.zone.run(() => {
          this.progress = progress;
        });
      },
        null,
        () => {
          this.progress.null
        })

    this.photoService.upload(this.wordId, nativeElement.files[0])
      .subscribe(photo => {
        this.photos.push(photo)
      });
  }

}