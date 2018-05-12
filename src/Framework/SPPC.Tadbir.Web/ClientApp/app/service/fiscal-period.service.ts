import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Environment } from "../enviroment";
import { BaseService } from '../class/base.service';
import { LookupApi } from './api/index';

@Injectable()
export class FiscalPeriodService extends BaseService{

    constructor(public http: Http) {
        super(http);
    }

    /**
     * این تابع دوره مالی رو براساس شرکت برمیگرداند
     * @param companyId کد شرکت
     */
    getFiscalPeriod(companyId : number,ticket : string) {
        var userId = '';        
        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
        this.headers.append('X-Tadbir-AuthTicket', ticket);
        if (ticket == '') return null;
        var url = String.Format(LookupApi.UserAccessibleCompanyFiscalPeriods, companyId, this.UserId);        
        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());
    }

}