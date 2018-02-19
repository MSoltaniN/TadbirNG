import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Transaction } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams } from "@angular/common/http";
import { Environment, MessageType } from "../enviroment";




export class TransactionInfo implements Transaction {
    //TODO: مقدار پیش فرض دوره مالی و شعبه در اینجا ست شده است که باید برداشته شود
    constructor(public id: number = 0, public description: string = "", public fiscalPeriodId: number = 1, public branchId: number = 1,
        public no: string = "", public date: Date = new Date()) { }

}


@Injectable()
export class TransactionService {

    private _getTransactionsUrl = Environment.BaseUrl + "/transactions/fp/{0}/branch/{1}";
    private _getCountUrl = Environment.BaseUrl + "/transactions/fp/{0}/branch/{1}/count";
    private _deleteTransactionsUrl = Environment.BaseUrl + "/transactions/{0}";
    private _deleteMultiTransactionsUrl = Environment.BaseUrl + "/transactions";
    private _postNewTransactionsUrl = Environment.BaseUrl + "/transactions";
    private _postModifiedTransactionsUrl = Environment.BaseUrl + "/transactions/{0}";


    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
        this.headers.append('X-Tadbir-AuthTicket', Environment.AdminTicket);

        this.options = new RequestOptions({ headers: this.headers });
    }

    getTransactions() {
        var headers = this.headers;
        headers.append("If-Modified-Since", "Tue, 24 July 2017 00:00:00 GMT");
        var url = this._getTransactionsUrl;
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

    search(start?: number, count?: number, orderby?: string, filters?: Filter[]) {
        var headers = this.headers;

        var gridPaging = { pageIndex: start, pageSize: count };

        var sort = new Array<GridOrderBy>();

        if (orderby) {
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: sort };

        var url = String.Format(this._getTransactionsUrl, Environment.FiscalPeriodId, Environment.BranchId);

        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(encodeURIComponent(postBody));

        searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());
    }

    editTransaction(transaction: Transaction): Observable<string> {
        var body = JSON.stringify(transaction);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(this._postModifiedTransactionsUrl, transaction.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertTransaction(transaction: Transaction): Observable<string> {
        var body = JSON.stringify(transaction);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        return this.http.post(this._postNewTransactionsUrl, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(transactionId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteTransactionsUrl, transactionId.toString());

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    // this method comment beacause method in controller not implemented

    deleteTransactions(transactions: string[]): Observable<string> {

        let body = JSON.stringify(transactions);

        let headers = this.headers
        let options = new RequestOptions({ headers: headers, body: body });

        return this.http.delete(this._deleteMultiTransactionsUrl, this.options)
            .map(response => response.json().message)
            .catch(this.handleError);
    }


    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}