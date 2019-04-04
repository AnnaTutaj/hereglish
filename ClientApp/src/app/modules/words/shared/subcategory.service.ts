import { Injectable } from '@angular/core';
import { Http } from '@angular/http'; 
import 'rxjs/add/operator/map';
import { AuthHttp } from "angular2-jwt";

@Injectable()
export class SubcategoryService {

  constructor(private http: Http, private authHttp: AuthHttp) { }

  create(subcategory) {
    return this.authHttp.post('/api/subcategories', subcategory)
      .map(res => res.json());
  }

}