import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Account } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";

export class AccountInfo implements Account
{    
    constructor(public accountId: number = 0, public code: string = "", public name: string = "",
        public fiscalPeriodId: number = 0, public description: string = "",public branchId:number = 0)
    { }
    
}

@Injectable()
export class AccountService 
{
    private _getAccountsUrl = "/Account/fp/{0}/branch/{1}";

    private _getAllAccountsUrl = "/Account/List";

    private _getTotalCountUrl = "/Account/TotalCount";

    private _getCountUrl = "/Account/Count";

    private _deleteAccountsUrl = "/Account/Delete/{0}";

    private _postNewAccountsUrl = "/Account/Insert";

    private _postModifiedAccountsUrl = "/Account/Edit";

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

    //get count of records base on Grid filters and order value
    getCount(orderby?: string, filters?: any[]) {
        var headers = new Headers();
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var url = this._getCountUrl;

        var postItem = { Filters: filters, OrderBy: orderby };

        return this.http.post(url,postItem, { headers: headers })
            .map(response => <any>(<Response>response).json());;


    }

    
    
    search(start? :number , count? :number , orderby?:string,filters?:Filter[]  ) {
        var headers = new Headers({ 'Content-Type': 'application/json' });
        var options = new RequestOptions({ headers: headers });

        //this path for filter branch and fiscalpriod
        //var url = this._getAccountsUrl;  

        var url = this._getAllAccountsUrl;





        /*
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
        }*/

        var postItem = { StartIndex: start, Count: count, Filters: filters, OrderBy: orderby };
        

        /*
        var fpId = '1';
        var branchId = '1';

        var newUrl = String.Format(this._getAccountsUrl, fpId, branchId);
        */

        var newUrl = this._getAllAccountsUrl;

        return this.http.post(newUrl, JSON.stringify(postItem), options)
            .map(response => <any>(<Response>response).json());
    }

    
    editAccount(account: Account): Observable<string> {
        let body = JSON.stringify(account);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this._postModifiedAccountsUrl, body, options)
            .map(res => res.json().message)
            .catch(this.handleError);
    }

    insertAccount(account: Account): Observable<string> {
        let body = JSON.stringify(account);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this._postNewAccountsUrl, body, options)
            .map(res => res.json().message)
            .catch(this.handleError);
    }
    
    delete(accountId: number) : Observable<string>
    {
        //ToDo : call api for delete entity

        var deleteByIdUrl = String.Format(this._deleteAccountsUrl, accountId.toString());

        return this.http.post(deleteByIdUrl,this.options)
            .map(response => response.json().message)
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Opps!! Server error');
    }


}