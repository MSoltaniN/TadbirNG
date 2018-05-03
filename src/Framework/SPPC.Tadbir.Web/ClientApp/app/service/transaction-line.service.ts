import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { TransactionLineViewModel } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams } from "@angular/common/http";
import { Environment, MessageType } from "../enviroment";
import { Context } from "../model/context";
import { BaseService } from '../class/base.service';
//import { FullAccountInfo } from './index';

//export class TransactionLineInfo implements TransactionLine {
//    constructor(public id: number = 0, public debit: number = 0, public credit: number = 0, public description?: string,
//        public fiscalPeriodId: number = 0, public branchId: number = 0, public transactionId: number = 0, public currencyId: number = 0,
//        public accountId: number = 0) {

//    }
//}

export class TransactionLineViewModelInfo implements TransactionLineViewModel {
    id: number = 0;
    debit: number = 0;
    credit: number = 0;
    description?: string | undefined;
    fiscalPeriodId: number = 0;
    branchId: number = 0;
    voucherId: number = 0;
    currencyId: number = 0;
    fullAccount: {
        accountId: number;
        detailId: number;
        costCenterId: number;
        projectId: number;
    };
}

@Injectable()
export class TransactionLineService extends BaseService {

    private getAccountArticlesUrl = "http://37.59.93.7:8080/accounts/{0}/articles";
    getAccountArticles(accountId: number) {

        var url = String.Format(this.getAccountArticlesUrl, accountId.toString());

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }




    //private _getTransactionLinesUrl = Environment.BaseUrl + "/transactions/{0}/details";//transactionId
    private _getTransactionLinesUrl = Environment.BaseUrl + "/vouchers/{0}/articles";//voucherId
    ////private _deleteMultiTransactionLinesUrl = Environment.BaseUrl + "/transactions";
    private _getCountUrl = Environment.BaseUrl + "/vouchers/{0}/articles/count";
    private _deleteTransactionLineUrl = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId
    private _postNewTransactionLineUrl = Environment.BaseUrl + "/vouchers/{0}/articles";//voucherId
    private _putModifiedTransactionLineUrl = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId
    private _getTransactionInfo = Environment.BaseUrl + "/vouchers/{0}";//voucherId
    private _getTransactionLineById = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId

    headers: Headers;
    options: RequestOptions;


    constructor(private http: Http) {
        super();

    }



    ////get count of records base on Grid filters and order value
    getCount(transactionId: number, orderby?: string, filters?: any[]) {
        var headers = this.headers;
        var url = String.Format(this._getCountUrl, transactionId);
        var postItem = { filters: filters };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());;

    }

    search(transactionId: number, start?: number, count?: number, orderby?: string, filters?: Filter[]) {

        var gridPaging = { pageIndex: start, pageSize: count };

        var sort = new Array<GridOrderBy>();

        if (orderby) {
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: sort };

        var url = String.Format(this._getTransactionLinesUrl, transactionId);

        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(encodeURIComponent(postBody));

        searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    getTransactionInfo(transactionId: number) {
        var url = String.Format(this._getTransactionInfo, transactionId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());;
    }

    editTransactionLine(transactionLine: TransactionLineViewModel): Observable<string> {
        var body = JSON.stringify(transactionLine);

        var url = String.Format(this._putModifiedTransactionLineUrl, transactionLine.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertTransactionLine(transactionId: number, transactionLine: TransactionLineViewModel): Observable<string> {
        var body = JSON.stringify(transactionLine);

        var url = String.Format(this._postNewTransactionLineUrl, transactionId);

        return this.http.post(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(transactionLineId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteTransactionLineUrl, transactionLineId);

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getTransactionLineById(articleId: number) {
        var url = String.Format(this._getTransactionLineById, articleId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}