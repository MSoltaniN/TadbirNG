import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { Filter } from "../class/filter";
import { HttpParams, HttpClient } from "@angular/common/http";
import { Context } from "../model/context";

import { BaseComponent } from "../class/base.component"
import { ToastrService } from 'ngx-toastr';
import { BaseService } from '../class/base.service';
import { LookupApi, AccountApi, DetailAccountApi, CostCenterApi, ProjectApi } from './api/index';



@Injectable()
export class LookupService extends BaseService {

  constructor(public http: HttpClient) {
    super(http);
  }

  GetAccountsLookup() {

    var url = String.Format(AccountApi.EnvironmentAccountsLookup, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetDetailAccountsLookup() {

    var url = String.Format(DetailAccountApi.EnvironmentDetailAccountsLookup, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetCostCentersLookup() {

    var url = String.Format(CostCenterApi.EnvironmentCostCentersLookup, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetProjectsLookup() {

    var url = String.Format(ProjectApi.EnvironmentProjectsLookup, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetCurrenciesLookup() {
    var options = { headers: this.httpHeaders };
    return this.http.get(LookupApi.Currencies, options)
      .map(response => <any>(<Response>response));
  }
}
