import { Injectable } from '@angular/core';
import { Http } from '@angular/http'; 
import 'rxjs/add/operator/map';

@Injectable()
export class CategoryService {

  constructor(private http: Http) { }

  getCategories() {
    return this.http.get('/api/categories')
      .map(res => res.json());
  }

}