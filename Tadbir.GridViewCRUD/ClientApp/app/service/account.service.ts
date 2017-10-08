﻿import { Injectable } from '@angular/core';
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
    private _getAccountsUrl = "/Account/GetLazyAccounts";

    private _getTotalCountUrl = "/Account/GetTotalCount";


    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http)
    {

        this.headers = new Headers({
            'Content-Type': 'application/json'            
        });
        this.options = new RequestOptions({ headers: this.headers });

    }

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

    search(start? :number , count? :number , orderby?:string,filters?:string  ) {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");


        var url = this._getAccountsUrl;

        let params: URLSearchParams = new URLSearchParams();

              
        if(start != undefined && count != undefined)
        {
            params.append("start", start.toString());
            params.append("count", count.toString());    
        }        

        
        if(filters)
        {
            params.set("filter", JSON.stringify(filters));     
        }



        if(orderby)
        {
            params.set("filter", orderby);     
        }

        var postItem = { Start: start, Count: count, Filters: filters, Order: orderby };

        

        this.options = new RequestOptions({ headers: this.headers });



        return this.http.post(url,JSON.stringify(postItem), Option)
            .map(response => <any>(<Response>response).json());
    }




}