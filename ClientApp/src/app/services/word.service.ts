import { Injectable } from '@angular/core';
import { Http } from '@angular/http'; 
import 'rxjs/add/operator/map';
import { SaveWord } from '../models/SaveWord';

@Injectable()
export class WordService {

  constructor(private http: Http) { }

  create(word) {
    return this.http.post('/api/words', word)
      .map(res => res.json());
  }

  get(id) {
    return this.http.get('/api/words/' + id)
      .map(res => res.json());
  }

  update(word : SaveWord) {
    return this.http.put('/api/words/' + word.id, word)
      .map(res => res.json());
  }
  
  
}