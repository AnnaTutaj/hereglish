import { Component, OnInit } from '@angular/core';

@Component({
    template: '<h1>Woah there cowboy!</h1><h2>You dont have permission to view this page</h2>'
})

export class UnauthorizatedAccessComponent implements OnInit {
    constructor() { }

    ngOnInit() { }
}