﻿import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Account } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams } from "@angular/common/http";
import { Environment } from "../enviroment";
import { Context } from "../model/context";


export class AccountInfo implements Account
{    
    constructor(public id: number = 0, public code: string = "", public name: string = "",
        public fiscalPeriodId: number = 0, public description: string = "",public branchId:number = 0,public level:number = 0,public fullCode:string = "0")
    { }
    
}


@Injectable()
export class AccountService 
{   

    private _getAccountsUrl = Environment.BaseUrl + "/accounts/fp/{0}/branch/{1}";

    private _getAllAccountsUrl = Environment.BaseUrl + "/accounts";

    private _getTotalCountUrl = Environment.BaseUrl + "/Account/TotalCount";

    private _getCountUrl = Environment.BaseUrl + "/accounts/fp/{0}/branch/{1}/count";

    private _deleteAccountsUrl = Environment.BaseUrl + "/accounts/{0}";

    private _deleteGroupAccountsUrl = Environment.BaseUrl + "/accounts";

    private _postNewAccountsUrl = Environment.BaseUrl + "/accounts";

    private _postModifiedAccountsUrl = Environment.BaseUrl + "/accounts/{0}";

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http)
    {

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8'});        

        //this section written in base class
        var ticket = '';

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            this.currentContext = JSON.parse(item != null ? item.toString() : "");

            ticket = this.currentContext ? this.currentContext.ticket.toString() : '';
        }

        //this section written in base class


        this.headers.append('X-Tadbir-AuthTicket', ticket);



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

        var url = String.Format(this._getCountUrl, Environment.FiscalPeriodId, Environment.BranchId);
        
        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());;
            
    }

    //get count of records base on Grid filters and order value
    getCount(orderby?: string, filters?: any[]) {
        var headers = this.headers;
        
        var url = String.Format(this._getCountUrl, Environment.FiscalPeriodId, Environment.BranchId);
        
        var postItem = { filters: filters };
        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(encodeURIComponent(postBody));

        searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });
        
        return this.http.get(url, options)
                .map(response => <any>(<Response>response).json());;

    }


    currentContext?: Context = undefined;
    
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


        //this section written in base class
        var fpId = '';
        var branchId = '';

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            this.currentContext = JSON.parse(item != null ? item.toString() : "");

            fpId = this.currentContext ? this.currentContext.fpId.toString() : '';
            branchId = this.currentContext ? this.currentContext.branchId.toString() : '';

        }

        //this section written in base class

        var url = String.Format(this._getAccountsUrl, fpId, branchId);

        var searchHeaders = this.headers;
        
        var postBody = JSON.stringify(postItem);
        
        var base64Body = btoa(encodeURIComponent(postBody));

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

        var acc: string = '';

        let accs: Array<number> = Array();


        for (var i = 0; i < accounts.length; i++)
        {
            var acc = accounts[i].split(' ')[0];
            accs.push(parseInt(acc));
        }

        let body = JSON.stringify({ paraph: '', items : accs});
        let headers = this.headers
        let options = new RequestOptions({ headers: headers });


        return this.http.put(this._deleteGroupAccountsUrl,body, this.options)
            .map(response => response)
            .catch(this.handleError);
    }
    

    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Opps!! Server error');
    }


}