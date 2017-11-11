import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { FullAccount } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';

export class FullAccountInfo implements FullAccount {
    constructor(public fullAccountId: number = 0, public accountId: number = 0,
        public costCenterId: number = 0, public projectId?: number , public detailId?: number)
    { }
}

@Injectable()
export class FullAccountService {

    private getFullAccountsUrl = "/FullAccount/{0}";

    constructor(private http: Http) {
        
    }


    getFullAccounts(accountId: number) {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        
        var url = String.Format(this.getFullAccountsUrl, accountId.toString());

        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }


}