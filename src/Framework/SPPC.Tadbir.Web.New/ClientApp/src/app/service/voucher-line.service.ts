import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { VoucherLine } from '../model/index';
import { VoucherApi } from './api/index';
import { AccountItemBrief } from "../model/accountItemBrief";

import { String } from '../class/source';
import { BaseService } from '../class/base.service';
import { HttpClient } from '@angular/common/http';


export class VoucherLineInfo implements VoucherLine {
    id: number = 0;
    debit: number = 0;
    credit: number = 0;
    description?: string | undefined;
    fiscalPeriodId: number = 0;
    branchId: number = 0;
    voucherId: number = 0;
    currencyId: number;
    fullAccount: {
        account: AccountItemBrief;
        detailAccount: AccountItemBrief;
        costCenter: AccountItemBrief;
        project: AccountItemBrief;
    };
}

@Injectable()
export class VoucherLineService extends BaseService {

    private getAccountArticlesUrl = "http://37.59.93.7:8080/accounts/{0}/articles";
    getAccountArticles(accountId: number) {

        var url = String.Format(this.getAccountArticlesUrl, accountId.toString());
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    constructor(public http: HttpClient) {
        super(http);
    }


    getVoucherInfo(voucherId: number) {
        var url = String.Format(VoucherApi.Voucher, voucherId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));;
    }

}
