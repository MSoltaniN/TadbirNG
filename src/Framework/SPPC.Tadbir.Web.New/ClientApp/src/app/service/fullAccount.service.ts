import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { FullAccount, AccountItemBrief } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams, HttpClient } from "@angular/common/http";
import { Context } from "../model/context";

import { BaseComponent } from "../class/base.component"
import { ToastrService } from 'ngx-toastr';
import { BaseService } from '../class/base.service';
import { LookupApi } from './api/index';
import { FilterExpression } from '../class/filterExpression';


export class FullAccountInfo implements FullAccount {
  account: AccountItemBrief;
  detailAccount: AccountItemBrief;
  costCenter: AccountItemBrief;
  project: AccountItemBrief;
}


@Injectable()
export class FullAccountService extends BaseService {

  constructor(public http: HttpClient) {
    super(http);
  }

  GetAccountsLookup() {

    var url = String.Format(LookupApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetDetailAccountsLookup() {

    var url = String.Format(LookupApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetCostCentersLookup() {

    var url = String.Format(LookupApi.FiscalPeriodBranchCostCenters, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetProjectsLookup() {

    var url = String.Format(LookupApi.FiscalPeriodBranchProjects, this.FiscalPeriodId, this.BranchId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  public getFullAccountItemList(apiUrl: string, filter?: FilterExpression) {
    var intMaxValue = 2147483647
    var gridPaging = { pageIndex: 1, pageSize: intMaxValue };
    var postItem = { Paging: gridPaging, filter: filter, sortColumns: null };
    var searchHeaders = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (searchHeaders)
      searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);
    var options = { headers: searchHeaders };

    return this.http.get(apiUrl, options)
      .map(response => <any>(<Response>response));
  }
}
