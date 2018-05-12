﻿import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { FullAccount } from '../model/index';
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
import { LookupApi } from './api/index';


export class FullAccountInfo implements FullAccount {

    constructor(public accountId: number = 0, public detailId: number = 0, public costCenterId: number = 0, public projectId: number = 0) { }

}


@Injectable()
export class FullAccountService extends BaseService {

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

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

    GetProjectsLookup() {

        var url = String.Format(LookupApi.FiscalPeriodBranchProjects, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }
}