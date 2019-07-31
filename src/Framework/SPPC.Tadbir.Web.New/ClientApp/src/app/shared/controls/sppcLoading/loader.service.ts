import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';


@Injectable()
export class LoaderService {
    public isLoading = new BehaviorSubject(false);

    constructor() { }
}

