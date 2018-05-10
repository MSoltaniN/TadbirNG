import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { VoucherLine } from '../model/index';
import { VoucherApi } from './api/index';
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


export class VoucherLineInfo implements VoucherLine {
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




    //private _getVoucherLinesUrl = Environment.BaseUrl + "/vouchers/{0}/articles";//voucherId
    //private _getCountUrl = Environment.BaseUrl + "/vouchers/{0}/articles/count";
    //private _deleteVoucherLineUrl = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId
    //private _postNewVoucherLineUrl = Environment.BaseUrl + "/vouchers/{0}/articles";//voucherId
    //private _putModifiedVoucherLineUrl = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId
    //private _getVoucherInfo = Environment.BaseUrl + "/vouchers/{0}";//voucherId
    //private _getVoucherLineById = Environment.BaseUrl + "/vouchers/articles/{0}";//articleId

    headers: Headers;
    options: RequestOptions;


    constructor(private http: Http) {
        super();

    }



    ////get count of records base on Grid filters and order value
    getCount(voucherId: number, orderby?: string, filters?: any[]) {
        var headers = this.headers;
        var url = String.Format(VoucherApi.VoucherArticleCount, voucherId);
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

        var url = String.Format(VoucherApi.VoucherArticles, voucherId);

        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(encodeURIComponent(postBody));

        searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    getVoucherInfo(voucherId: number) {
        var url = String.Format(VoucherApi.Voucher, voucherId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());;
    }

    editVoucherLine(voucherLine: VoucherLine): Observable<string> {
        var body = JSON.stringify(voucherLine);

        var url = String.Format(VoucherApi.VoucherArticle, voucherLine.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertVoucherLine(voucherId: number, voucherLine: VoucherLine): Observable<string> {
        var body = JSON.stringify(voucherLine);

        var url = String.Format(VoucherApi.VoucherArticles, voucherId);

        return this.http.post(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(VoucherLineId: number): Observable<string> {

        var deleteByIdUrl = String.Format(VoucherApi.VoucherArticle, VoucherLineId);

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getVoucherLineById(articleId: number) {
        var url = String.Format(VoucherApi.VoucherArticle, articleId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}