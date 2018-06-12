import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { BaseService } from '../class/base.service';
import { FiscalPeriod, RelatedItems } from '../model/index';
import { String } from '../class/source';
import { FiscalPeriodApi } from './api/index';


export class FiscalPeriodInfo implements FiscalPeriod {
    companyId: number;
    id: number=0;
    name: string;
    startDate: Date = new Date();
    endDate: Date = new Date();
    description?: string | undefined;

}

@Injectable()
export class FiscalPeriodService extends BaseService{

    constructor(public http: Http) {
        super(http);
    }

    getFiscalPeriodRoles(fPeriodId: number) {
        var url = String.Format(FiscalPeriodApi.FiscalPeriodRoles, fPeriodId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());
    }

    modifiedFiscalPeriodRoles(fPeriodIdRoles: RelatedItems) {
        var body = JSON.stringify(fPeriodIdRoles);
        var headers = this.headers;
        var url = String.Format(FiscalPeriodApi.FiscalPeriodRoles, fPeriodIdRoles.id);
        return this.http.put(url, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

}