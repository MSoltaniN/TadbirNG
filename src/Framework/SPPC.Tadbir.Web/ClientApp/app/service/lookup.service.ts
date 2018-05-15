import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { HttpParams } from "@angular/common/http";
import { Environment, MessageType } from "../enviroment";
import { Context } from "../model/context";

import { BaseComponent } from "../class/base.component"
import { ToastrService } from 'ngx-toastr';
import { BaseService } from '../class/base.service';
import { LookupApi } from './api/index';



@Injectable()
export class LookupService extends BaseService {

    constructor(public http: Http) {
        super(http);
    }

    GetAccountsLookup() {

        var url = String.Format(LookupApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

    GetDetailAccountsLookup() {

        var url = String.Format(LookupApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

    GetCostCentersLookup() {

        var url = String.Format(LookupApi.FiscalPeriodBranchCostCenters, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());

    }

    GetProjectsLookup() {

        var url = String.Format(LookupApi.FiscalPeriodBranchProjects, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

    GetCurrenciesLookup() {
        return this.http.get(LookupApi.Currencies, this.options)
            .map(response => <any>(<Response>response).json());
    }
}