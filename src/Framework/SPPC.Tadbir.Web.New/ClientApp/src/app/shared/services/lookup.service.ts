import { Injectable } from '@angular/core';
import {  Response } from '@angular/http';
import "rxjs/Rx";
import { HttpClient } from "@angular/common/http";
import { AccountRelationApi } from '@sppc/finance/service/api';
import { BrowserStorageService } from '@sppc/shared/services';
import { LookupApi } from '@sppc/shared/services/api';
import { BaseService } from '../class';



@Injectable()
export class LookupService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
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
