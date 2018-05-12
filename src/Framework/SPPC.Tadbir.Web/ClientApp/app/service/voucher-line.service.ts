﻿import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { VoucherLine } from '../model/index';
import { VoucherApi } from './api/index';

import { String } from '../class/source';
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

    constructor(public http: Http) {
        super(http);
    }


    getVoucherInfo(voucherId: number) {
        var url = String.Format(VoucherApi.Voucher, voucherId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());;
    }

}