import { Injectable } from '@angular/core';
import { Http } from '@angular/http'; 
import 'rxjs/add/operator/map';

@Injectable()
export class PartOfSpeechService {

  constructor(private http: Http) { }

  getPartsOfSpeech() {
    return this.http.get('/api/parts-of-speech')
      .map(res => res.json());
  }
}
