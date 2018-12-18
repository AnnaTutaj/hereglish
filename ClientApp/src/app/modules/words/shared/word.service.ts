import { Injectable } from '@angular/core';
import { Http, ResponseContentType } from '@angular/http';
import 'rxjs/add/operator/map';
import { SaveWord } from '../../../common/models/SaveWord';
import { AuthHttp } from "angular2-jwt";

@Injectable()
export class WordService {

  private readonly wordEndpoint = '/api/words'

  constructor(private http: Http, private authHttp: AuthHttp) { }

  create(word) {
    return this.authHttp.post(this.wordEndpoint, word)
      .map(res => res.json());
  }

  getById(id) {
    return this.http.get(this.wordEndpoint + '/' + id)
      .map(res => res.json());
  }

  get(filter) {
    return this.http.get(this.wordEndpoint + '?' + this.toQueryString(filter))
      .map(res => res.json());
  }

  toQueryString(obj) {
    var queryParts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) {
        queryParts.push(encodeURIComponent(property) + "=" + encodeURIComponent(value));
      }
    }

    return queryParts.join('&');
  }

  update(word: SaveWord) {
    return this.authHttp.put(this.wordEndpoint + '/' + word.id, word)
      .map(res => res.json());
  }

  delete(id) {
    return this.authHttp.delete(this.wordEndpoint + '/' + id)
      .map(res => res.json());
  }

  getPdf() {
    return this.http.get(this.wordEndpoint + "/" + "pdf", { responseType: ResponseContentType.Blob })
      .map((res) => {
        return new Blob([res.blob()], { type: 'application/vnd.ms-excel' })
      })
  }

}