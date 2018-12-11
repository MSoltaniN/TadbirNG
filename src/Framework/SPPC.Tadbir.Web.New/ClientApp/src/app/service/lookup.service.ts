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
import { LookupApi, AccountApi, DetailAccountApi, CostCenterApi, ProjectApi, AccountRelationApi } from './api/index';



@Injectable()
export class LookupService extends BaseService {

  constructor(public http: HttpClient) {
    super(http);
  }

  GetAccountsLookup() {

    var url = AccountRelationApi.EnvironmentAccountsLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetDetailAccountsLookup() {

    var url = AccountRelationApi.EnvironmentDetailAccountsLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetCostCentersLookup() {

    var url = AccountRelationApi.EnvironmentCostCentersLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetProjectsLookup() {

    var url = AccountRelationApi.EnvironmentProjectsLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));

  }

  GetCurrenciesLookup() {
    var options = { headers: this.httpHeaders };
    return this.http.get(LookupApi.Currencies, options)
      .map(response => <any>(<Response>response));
  }

  GetAccountGroupsLookup() {
    var url = LookupApi.AccountGroups;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }
}
