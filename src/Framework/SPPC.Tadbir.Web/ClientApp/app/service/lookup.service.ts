﻿import { Injectable } from '@angular/core';
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



@Injectable()
export class LookupService extends BaseService {

    //FullAccount
    private _getAccountsUrl = Environment.BaseUrl + "/lookup/accounts/fp/{0}/branch/{1}";//fpId,branchId
    private _getDetailAccountsUrl = Environment.BaseUrl + "/lookup//faccounts/fp/{0}/branch/{1}";//fpId,branchId
    private _getCostCentersUrl = Environment.BaseUrl + "/lookup/costcenters/fp/{0}/branch/{1}";//fpId,branchId
    private _getProjectsUrl = Environment.BaseUrl + "/lookup/projects/fp/{0}/branch/{1}";//fpId,branchId

    //Curency
    private _getCurrenciesUrl = Environment.BaseUrl+"/lookup/currencies"
    

    headers: Headers;
    options: RequestOptions;

    constructor(private http: Http) {

        super();

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });
        this.headers.append('X-Tadbir-AuthTicket', this.Ticket);
        this.options = new RequestOptions({ headers: this.headers });

    }

    GetAccountsLookup() {

        var url = String.Format(this._getAccountsUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());

    }

    GetDetailAccountsLookup() {

        var url = String.Format(this._getDetailAccountsUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());

    }

    GetCostCentersLookup() {

        var url = String.Format(this._getCostCentersUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());

    }

    GetProjectsLookup() {

        var url = String.Format(this._getProjectsUrl, this.FiscalPeriodId, this.BranchId);

        return this.http.get(url, { headers: this.headers })
            .map(response => <any>(<Response>response).json());

    }

    GetCurrenciesLookup() {

        return this.http.get(this._getCurrenciesUrl, { headers: this.headers })
            .map(response => <any>(<Response>response).json());

    }
}