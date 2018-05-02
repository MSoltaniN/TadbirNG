﻿import { Injectable } from '@angular/core';
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
import { Context } from "../model/context";

import { BaseComponent } from "../class/base.component"
import { ToastrService } from 'ngx-toastr';
import { BaseService } from '../class/base.service';


export class TransactionInfo implements Transaction {

    constructor(public id: number = 0, public description: string = "", public fiscalPeriodId: number = 0, public branchId: number = 0,
        public no: string = "", public date: Date = new Date()) {

    }

}

@Injectable()
export class TransactionService extends BaseService {

    private _getTransactionsUrl = Environment.BaseUrl + "/vouchers/fp/{0}/branch/{1}";
    private _getCountUrl = Environment.BaseUrl + "/vouchers/fp/{0}/branch/{1}/count";
    private _deleteTransactionsUrl = Environment.BaseUrl + "/vouchers/{0}";
    private _deleteMultiTransactionsUrl = Environment.BaseUrl + "/vouchers";
    private _postNewTransactionsUrl = Environment.BaseUrl + "/vouchers";
    private _postModifiedTransactionsUrl = Environment.BaseUrl + "/vouchers/{0}";
    private _getTransactionByIdUrl = Environment.BaseUrl + "/vouchers/{0}";

    private fiscalPeriodId: string;
    private branchId: string;

    constructor(private http: Http) {
        super();
    }


    getTransactions() {
        var headers = this.headers;
        var url = String.Format(this._getTransactionsUrl, this.FiscalPeriodId, this.BranchId);
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
        if (orderby) {
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: sort };
        var url = String.Format(this._getTransactionsUrl, this.FiscalPeriodId, this.BranchId);
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

    editTransaction(transaction: Transaction): Observable<string> {
        var body = JSON.stringify(transaction);
        
        var url = String.Format(this._postModifiedTransactionsUrl, transaction.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertTransaction(transaction: Transaction): Observable<string> {
        var body = JSON.stringify(transaction);  

        return this.http.post(this._postNewTransactionsUrl, body, this.options)
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

        return this.http.delete(this._deleteMultiTransactionsUrl, options)
            .map(response => response.json().message)
            .catch(this.handleError);
    }

    getTransactionById(transactionId: number) {
        var url = String.Format(this._getTransactionByIdUrl, transactionId);
        var options = new RequestOptions({ headers: this.headers });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());
    }


    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}