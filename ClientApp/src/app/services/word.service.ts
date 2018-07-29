import { Injectable } from '@angular/core';
import { Http } from '@angular/http'; 
import 'rxjs/add/operator/map';

@Injectable()
export class WordService {

  constructor(private http: Http) { }

  create(word) {
    return this.http.post('/api/words', word)
      .map(res => res.json());
  }
  
}