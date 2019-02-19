import { Injectable } from '@angular/core';
import {  Response } from '@angular/http';
import "rxjs/Rx";
import { HttpClient } from "@angular/common/http";
import { BaseService } from '../class/base.service';
import { LookupApi, AccountRelationApi } from './api/index';



@Injectable()
export class LookupService extends BaseService {

  constructor(public http: HttpClient) {
    super(http);
  }

  GetLookup(url: string) {
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
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
