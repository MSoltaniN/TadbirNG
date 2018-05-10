import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Voucher, DocumentAction } from '../model/index';
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

import { BaseComponent } from "../class/base.component"
import { ToastrService } from 'ngx-toastr';
import { BaseService } from '../class/base.service';


export class VoucherInfo implements Voucher {
    fiscalPeriodId: number = 0;
    branchId: number = 0;
    workItemId: number;
    workItemTargetId: number;
    workItemAction: string;
    debitSum: number;
    creditSum: number;
    document: {
        typeId: number;
        statusId: number;
        statusName: string;
        actions: Array<DocumentAction>;
        id: number;
        entityNo: string;
        no: string;
        operationalStatus: string;
    };
    id: number = 0;
    no: string;
    date: Date = new Date();
    description?: string | undefined;


    //constructor(public id: number = 0, public description: string = "", public fiscalPeriodId: number = 0, public branchId: number = 0,
    //    public no: string = "", public date: Date = new Date()) {

    //}

}

@Injectable()
export class VoucherService extends BaseService {

    //private _getVouchersUrl = Environment.BaseUrl + "/vouchers/fp/{0}/branch/{1}";
    //private _getCountUrl = Environment.BaseUrl + "/vouchers/fp/{0}/branch/{1}/count";
    //private _deleteVouchersUrl = Environment.BaseUrl + "/vouchers/{0}";
    //private _deleteMultiVouchersUrl = Environment.BaseUrl + "/vouchers";
    //private _postNewVouchersUrl = Environment.BaseUrl + "/vouchers";
    //private _postModifiedVouchersUrl = Environment.BaseUrl + "/vouchers/{0}";
    //private _getVoucherByIdUrl = Environment.BaseUrl + "/vouchers/{0}";

    private fiscalPeriodId: string;
    private branchId: string;

    constructor(private http: Http) {
        super();
    }


    getVouchers() {
        var headers = this.headers;
        var url = String.Format(VoucherApi.FiscalPeriodBranchVouchers, this.FiscalPeriodId, this.BranchId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    getTotalCount() {
        var url = String.Format(VoucherApi.FiscalPeriodBranchItemCount, this.FiscalPeriodId, this.BranchId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());;
    }

    //get count of records base on Grid filters and order value
    getCount(orderby?: string, filters?: any[]) {

        var headers = this.headers;
        var url = String.Format(VoucherApi.FiscalPeriodBranchItemCount, this.FiscalPeriodId, this.BranchId);
        var postItem = { filters: filters };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });
        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());
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
        var url = String.Format(VoucherApi.FiscalPeriodBranchVouchers, this.FiscalPeriodId, this.BranchId);
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

    editVoucher(voucher: Voucher): Observable<string> {
        var body = JSON.stringify(voucher);

        var url = String.Format(VoucherApi.Voucher, voucher.id);

        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertVoucher(voucher: Voucher): Observable<string> {
        var body = JSON.stringify(voucher);

        return this.http.post(VoucherApi.Vouchers, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(voucherId: number): Observable<string> {

        var deleteByIdUrl = String.Format(VoucherApi.Voucher, voucherId.toString());

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    // this method comment beacause method in controller not implemented

    deleteVouchers(vouchers: string[]): Observable<string> {

        let body = JSON.stringify(vouchers);

        let headers = this.headers
        let options = new RequestOptions({ headers: headers, body: body });

        return this.http.delete(VoucherApi.Vouchers, options)
            .map(response => response.json().message)
            .catch(this.handleError);
    }

    getVoucherById(voucherId: number) {
        var url = String.Format(VoucherApi.Voucher, voucherId);
        var options = new RequestOptions({ headers: this.headers });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());
    }


    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}