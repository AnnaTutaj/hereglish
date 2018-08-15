import { Http } from '@angular/http';
import { Injectable } from '@angular/core';

@Injectable()
export class PhotoService {

    constructor(private http: Http) { }

    upload(wordId, photo) {
        var formData = new FormData();
        formData.append('file', photo);
        return this.http.post(`/api/words/${wordId}/photos`, formData)
            .map(res => res.json());
    }

}