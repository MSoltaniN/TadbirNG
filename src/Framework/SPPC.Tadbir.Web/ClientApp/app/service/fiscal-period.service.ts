import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { BaseService } from '../class/base.service';
import { FiscalPeriod, RelatedItems } from '../model/index';
import { String } from '../class/source';
import { FiscalPeriodApi } from './api/index';
import { HttpClient } from '@angular/common/http';


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

    constructor(public http: HttpClient) {
        super(http);
    }

    getFiscalPeriodRoles(fPeriodId: number) {
        var url = String.Format(FiscalPeriodApi.FiscalPeriodRoles, fPeriodId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(response => <any>(<Response>response));
    }

    modifiedFiscalPeriodRoles(fPeriodIdRoles: RelatedItems) {
        var body = JSON.stringify(fPeriodIdRoles);
        
        var options = { headers: this.httpHeaders };

        var url = String.Format(FiscalPeriodApi.FiscalPeriodRoles, fPeriodIdRoles.id);
        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

}