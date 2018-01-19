import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Account } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams } from "@angular/common/http";

export class AccountInfo implements Account
{    
    constructor(public id: number = 0, public code: string = "", public name: string = "",
        public fiscalPeriodId: number = 0, public description: string = "",public branchId:number = 0,public level:number = 0,public fullCode:string = "0")
    { }
    
}

const BASE_URL = "http://130.185.76.7:8080";
const ADMIN_TICKET = "eyJVc2VyIjp7IklkIjoxLCJQZXJzb25GaXJzdE5hbWUiOiIiLCJQZXJzb25MYXN0TmFtZSI6IiIsIkJyYW5jaGVzIjpbMSwyXSwiUm9sZXMiOlsxXSwiUGVybWlzc2lvbnMiOlt7IkVudGl0eU5hbWUiOiJBY2NvdW50IiwiRmxhZ3MiOjE1fSx7IkVudGl0eU5hbWUiOiJUcmFuc2FjdGlvbiIsIkZsYWdzIjoxMDIzfSx7IkVudGl0eU5hbWUiOiJVc2VyIiwiRmxhZ3MiOjd9LHsiRW50aXR5TmFtZSI6IlJvbGUiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlJlcXVpc2l0aW9uVm91Y2hlciIsIkZsYWdzIjoxMjd9LHsiRW50aXR5TmFtZSI6Iklzc3VlUmVjZWlwdFZvdWNoZXIiLCJGbGFncyI6NjN9LHsiRW50aXR5TmFtZSI6IlNhbGVzSW52b2ljZSIsIkZsYWdzIjozMX0seyJFbnRpdHlOYW1lIjoiUHJvZHVjdEludmVudG9yeSIsIkZsYWdzIjoxNX1dfX0=";
const FP_ID = 1
const BRANCH_ID = 1


@Injectable()
export class AccountService 
{   
   
    private _getAccountsUrl = BASE_URL + "/accounts/fp/{0}/branch/{1}";

    private _getAllAccountsUrl = BASE_URL + "/accounts";

    private _getTotalCountUrl = BASE_URL + "/Account/TotalCount";

    private _getCountUrl = BASE_URL + "/accounts/fp/{0}/branch/{1}/count";

    private _deleteAccountsUrl = BASE_URL + "/accounts/{0}";

    private _postNewAccountsUrl = BASE_URL + "/accounts";

    private _postModifiedAccountsUrl = BASE_URL + "/accounts/{0}";

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http)
    {

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8'});        
        this.headers.append('X-Tadbir-AuthTicket', ADMIN_TICKET);
        
        this.options = new RequestOptions({ headers: this.headers });        
    }

    getAccounts() {
        var headers = this.headers;
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var url = this._getAccountsUrl;
        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }

    getTotalCount() {
        var headers = this.headers;
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");

        var url = String.Format(this._getCountUrl, FP_ID, BRANCH_ID);
        
        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());;
            
    }

    //get count of records base on Grid filters and order value
    getCount(orderby?: string, filters?: any[]) {
        var headers = this.headers;
        //headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");

        var url = String.Format(this._getCountUrl, FP_ID, BRANCH_ID);

        //var postItem = { Filters: filters, OrderBy: orderby };
        
        //return this.http.post(url,postItem, { headers: headers })
        //    .map(response => <any>(<Response>response).json());;
        let options = new RequestOptions({ headers: this.headers });

            return this.http.get(url, options)
                .map(response => <any>(<Response>response).json());;

    }

    
    
    search(start?: number, count?: number, orderby?: string, filters?: Filter[]) {
        var headers = this.headers;
        
        var gridPaging = { pageIndex: start, pageSize: count };

        var sort = new Array<GridOrderBy>();

        if (orderby)
        { 
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging : gridPaging, filters : filters, sortColumns: sort };
        
        var url = String.Format(this._getAccountsUrl, FP_ID, BRANCH_ID);

        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(postBody);

        searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url,options)
            .map(response => <any>(<Response>response).json());
    }

    
    editAccount(account: Account): Observable<string> {
        var body = JSON.stringify(account);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(this._postModifiedAccountsUrl, account.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertAccount(account: Account): Observable<string> {
        var body = JSON.stringify(account);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        return this.http.post(this._postNewAccountsUrl, body, options)
            .map(res => res)
            .catch(this.handleError);
    }
    
    delete(accountId: number) : Observable<string>
    {
        //ToDo : call api for delete entity

        var deleteByIdUrl = String.Format(this._deleteAccountsUrl, accountId.toString());

        return this.http.delete(deleteByIdUrl,this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    // this method comment beacause method in controller not implemented
    
    deleteAccounts(accounts: string[]): Observable<string> {
        //ToDo : call api for delete entity

        let body = JSON.stringify(accounts);
        let headers = this.headers
        let options = new RequestOptions({ headers: headers });


        return this.http.post(this._deleteAccountsUrl,body, this.options)
            .map(response => response.json().message)
            .catch(this.handleError);
    }
    

    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Opps!! Server error');
    }


}