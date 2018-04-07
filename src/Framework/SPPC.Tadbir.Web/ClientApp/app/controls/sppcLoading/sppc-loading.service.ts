import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class SppcLoadingService {

    public spinnerSubject: BehaviorSubject<any> = new BehaviorSubject<any>(false);

    constructor() { }

    show() {
        this.spinnerSubject.next(true);
    }

    hide() {
        this.spinnerSubject.next(false);
    }

    getMessage(): Observable<any> {
        return this.spinnerSubject.asObservable();
    }

}
