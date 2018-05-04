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
import { Environment } from "../enviroment";
import { Context } from "../model/context";
import { BaseService } from '../class/base.service';


export class AccountInfo implements Account
{    
    constructor(public id: number = 0, public code: string = "", public name: string = "",
        public fiscalPeriodId: number = 0, public description: string = "",
        public branchId: number = 0, public level: number = 0, public fullCode: string = "0",
        public childCount: number = 0, public parentId: number = 0)
    { }
    
}

export class GridResult  {
    constructor(public data: any , public totalCount: number) { }
}


@Injectable()
export class AccountService extends BaseService
{   

    private _getAccountsUrl = Environment.BaseUrl + "/accounts/fp/{0}/branch/{1}";

    private _getAllAccountsUrl = Environment.BaseUrl + "/accounts";

    private _getTotalCountUrl = Environment.BaseUrl + "/Account/TotalCount";

    private _getCountUrl = Environment.BaseUrl + "/accounts/fp/{0}/branch/{1}/count";

    private _deleteAccountsUrl = Environment.BaseUrl + "/accounts/{0}";

    private _deleteGroupAccountsUrl = Environment.BaseUrl + "/accounts";

    private _postNewAccountsUrl = Environment.BaseUrl + "/accounts";

    private _postModifiedAccountsUrl = Environment.BaseUrl + "/accounts/{0}";

    private _getAccountByIdUrl = Environment.BaseUrl + "/accounts/{0}";

   

    constructor(private http: Http)
    {
        super();             
    }

    getAccounts() {
        
        var url = String.Format(this._getAccountsUrl, this.FiscalPeriodId, this.BranchId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    getTotalCount() {
        
        var url = String.Format(this._getCountUrl, this.FiscalPeriodId, this.BranchId);
        
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());;
            
    }

    //get count of records base on Grid filters and order value
    getCount(orderby?: string, filters?: any[]) {
        var headers = this.headers;
        
        var url = String.Format(this._getCountUrl, this.FiscalPeriodId, this.BranchId);
        
        var postItem = { filters: filters };
        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(encodeURIComponent(postBody));

        if (searchHeaders)
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
        
        var url = String.Format(this._getAccountsUrl, this.FiscalPeriodId, this.BranchId);

        var searchHeaders = this.headers;
        
        var postBody = JSON.stringify(postItem);
        
        var base64Body = btoa(encodeURIComponent(postBody));

        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        
        var options = new RequestOptions({ headers: searchHeaders });
        
        var result: any = null;
        var totalCount = 0;

        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
       
    }

   
   

    
    editAccount(account: Account): Observable<string> {
        var body = JSON.stringify(account);
        
        var url = String.Format(this._postModifiedAccountsUrl, account.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertAccount(account: Account): Observable<string> {
        var body = JSON.stringify(account);
        
        return this.http.post(this._postNewAccountsUrl, body, this.options)
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
    
        return this.http.put(this._deleteGroupAccountsUrl,body, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getAccountById(accountId: number) {
        var url = String.Format(this._getAccountByIdUrl, accountId);
    
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}