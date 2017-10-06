import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Account } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
 


class AccountInfo implements Account
{
    constructor(public id: number,public code: string,public name: string,public fiscalPeriodId: number,public description?: string) {}
}

@Injectable()
export class AccountService 
{
    private _getAccountsUrl = "/Account/GetAccounts";

    private _getTotalCountUrl = "/Account/GetTotalCount";

    constructor(private http: Http) { }

    getAccounts() {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var url = this._getAccountsUrl;
        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }

    getTotalCount() {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var url = this._getTotalCountUrl;
        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());;
        
            
    }

    search(start? :number , count? :number , order?:string,filter?:string  ) {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var url = this._getAccountsUrl;

        
        if(start != undefined && count != undefined)
            url+= "/" + start.toString() + "/" + count.toString();
        

        if(filter)
        {
            url+= "/filter/" + JSON.stringify(filter);
        }


        if(order)
        {
            url+= "/order/" + order;
        }


        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }




}