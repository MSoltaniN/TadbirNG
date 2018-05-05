import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { VoucherLineViewModel } from '../model/index';
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

export class VoucherLineViewModelInfo implements VoucherLineViewModel {
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
export class VoucherLineService extends BaseService {

    private getAccountArticlesUrl = "http://37.59.93.7:8080/accounts/{0}/articles";
    getAccountArticles(accountId: number) {

        var url = String.Format(this.getAccountArticlesUrl, accountId.toString());

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }




    private _getVoucherLinesUrl = Environment.BaseUrl + "/vouchers/{0}/articles";//voucherId
    private _getCountUrl = Environment.BaseUrl + "/vouchers/{0}/articles/count";
    private _deleteVoucherLineUrl = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId
    private _postNewVoucherLineUrl = Environment.BaseUrl + "/vouchers/{0}/articles";//voucherId
    private _putModifiedVoucherLineUrl = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId
    private _getVoucherInfo = Environment.BaseUrl + "/vouchers/{0}";//voucherId
    private _getVoucherLineById = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId

    headers: Headers;
    options: RequestOptions;


    constructor(private http: Http) {
        super();

    }



    ////get count of records base on Grid filters and order value
    getCount(voucherId: number, orderby?: string, filters?: any[]) {
        var headers = this.headers;
        var url = String.Format(this._getCountUrl, voucherId);
        var postItem = { filters: filters };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());;

    }

    search(voucherId: number, start?: number, count?: number, orderby?: string, filters?: Filter[]) {

        var gridPaging = { pageIndex: start, pageSize: count };

        var sort = new Array<GridOrderBy>();

        if (orderby) {
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: sort };

        var url = String.Format(this._getVoucherLinesUrl, voucherId);

        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(encodeURIComponent(postBody));

        searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    getVoucherInfo(voucherId: number) {
        var url = String.Format(this._getVoucherInfo, voucherId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());;
    }

    editVoucherLine(voucherLine: VoucherLineViewModel): Observable<string> {
        var body = JSON.stringify(voucherLine);

        var url = String.Format(this._putModifiedVoucherLineUrl, voucherLine.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertVoucherLine(voucherId: number, voucherLine: VoucherLineViewModel): Observable<string> {
        var body = JSON.stringify(voucherLine);

        var url = String.Format(this._postNewVoucherLineUrl, voucherId);

        return this.http.post(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(VoucherLineId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteVoucherLineUrl, VoucherLineId);

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getVoucherLineById(articleId: number) {
        var url = String.Format(this._getVoucherLineById, articleId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}