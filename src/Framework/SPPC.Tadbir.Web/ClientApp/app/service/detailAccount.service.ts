import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { DetailAccountViewModel } from '../model/index';
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

export class DetailAccountViewModelInfo implements DetailAccountViewModel {
    id: number = 0;
    code: string;
    fullCode: string;
    name: string;
    level: number = 0;
    description?: string | undefined;
    parentId?: number | undefined;
    fiscalPeriodId: number = 0;
    branchId: number = 0;
}

@Injectable()
export class DetailAccountService extends BaseService {

    private _getDetailAccountsUrl = Environment.BaseUrl + "/faccounts/fp/{0}/branch/{1}";//fpId,branchId
    private _postNewDetailAccountUrl = Environment.BaseUrl + "/faccounts";
    private _putModifiedDetailAccountUrl = Environment.BaseUrl + "/faccounts/{0}";//faccountId
    private _getDetailAccountByIdUrl = Environment.BaseUrl + "/faccounts/{0}";//faccountId
    private _deleteDetailAccountUrl = Environment.BaseUrl + "/faccounts/{0}";//faccountId

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {

        super();

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
        var url = String.Format(this._getDetailAccountsUrl, this.FiscalPeriodId, this.BranchId);
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        var result: any = null;
        var totalCount = 0;

        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    editDetailAccount(detailAccountViewModel: DetailAccountViewModel): Observable<string> {
        var body = JSON.stringify(detailAccountViewModel);

        var url = String.Format(this._putModifiedDetailAccountUrl, detailAccountViewModel.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertDetailAccount(detailAccountViewModel: DetailAccountViewModel): Observable<string> {
        var body = JSON.stringify(detailAccountViewModel);

        return this.http.post(this._postNewDetailAccountUrl, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(detailAccountId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteDetailAccountUrl, detailAccountId.toString());

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    getDetailAccountById(detailAccountId: number) {
        var url = String.Format(this._getDetailAccountByIdUrl, detailAccountId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }


    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}