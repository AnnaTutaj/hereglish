import { Injectable } from '@angular/core';

@Injectable()
export class ConfigService {

    constructor() { }

    params = {
        pageSize: {
            small: { default: 10, items: [5, 10, 20, 30] },
            medium: { default: 20, items: [10, 20, 30, 50, 100] },
            large: { default: 50, items: [30, 50, 100, 150, 200] },
        },

    }


}