import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import { SaveWord } from '../models/SaveWord';

@Injectable()
export class WordService {

  private readonly wordEndpoint = '/api/words'

  constructor(private http: Http) { }

  create(word) {
    return this.http.post(this.wordEndpoint, word)
      .map(res => res.json());
  }

  getById(id) {
      return this.http.get(this.wordEndpoint + '/' + id)
        .map(res => res.json());
  }

  get(){
    return this.http.get(this.wordEndpoint)
    .map(res => res.json());
  }

  update(word: SaveWord) {
    return this.http.put(this.wordEndpoint + '/' + word.id, word)
      .map(res => res.json());
  }

  delete(id) {
    return this.http.delete(this.wordEndpoint + '/' + id)
      .map(res => res.json());
  }

}