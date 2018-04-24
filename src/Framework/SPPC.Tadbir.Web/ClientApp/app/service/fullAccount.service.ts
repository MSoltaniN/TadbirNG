import { Injectable } from '@angular/core';
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


export class FullAccountInfo implements FullAccount {

    constructor(public id: number = 0, public accountId: number = 0, public detailId: number = 0, public costCenterId: number = 0, public projectId: number = 0) { }

}


@Injectable()
export class FullAccountService extends BaseService {

    private _getAccountsUrl = Environment.BaseUrl + "/lookup/accounts/fp/{0}/branch/{1}";//fpId,branchId
    private _getDetailAccountsUrl = Environment.BaseUrl + "/lookup//faccounts/fp/{0}/branch/{1}";//fpId,branchId
    private _getCostCentersUrl = Environment.BaseUrl + "/lookup/costcenters/fp/{0}/branch/{1}";//fpId,branchId
    private _getProjectsUrl = Environment.BaseUrl + "/lookup/projects/fp/{0}/branch/{1}";//fpId,branchId
   
    constructor(private http: Http) {
        super();        
    }

    GetAccountsLookup() {

        var url = String.Format(this._getAccountsUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

    GetDetailAccountsLookup() {

        var url = String.Format(this._getDetailAccountsUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

    GetCostCentersLookup() {

        var url = String.Format(this._getCostCentersUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }

    GetProjectsLookup() {

        var url = String.Format(this._getProjectsUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());

    }
}