import { Injectable } from '@angular/core';
import { Http, RequestOptions, Response } from '@angular/http';
import { BaseService } from '../class/base.service';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators/map';
import { AccountItemRelations } from '../model/index';



export class AccountItemRelationsInfo implements AccountItemRelations {
    id: number;
    relatedItemIds: number[];
}

@Injectable()
export class AccountRelationsService extends BaseService {
    constructor(public http: Http) {
        super(http);
    }

    public getMainComponentModel(apiUrl: string) {
        var options = new RequestOptions({ headers: this.headers });
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response));
    }

    public getChildrens(apiUrl: string) {
        var options = new RequestOptions({ headers: this.headers });
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response).json());
    }

    public getRelatedComponentModel(apiUrl: string) {
        var options = new RequestOptions({ headers: this.headers });
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response).json());
    }

}