
import {map} from 'rxjs/operators';
import { Injectable } from '@angular/core';
import {  Response } from '@angular/http';
import "rxjs/Rx";
import { HttpClient } from "@angular/common/http";
import { AccountRelationApi } from '@sppc/finance/service/api';
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';
import { LookupApi } from '@sppc/shared/services/api';
import { BaseService } from '@sppc/shared/class/base.service';
import { String } from '@sppc/shared/class/source';


@Injectable()
export class LookupService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  GetLookup(url: string) {
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));
  }

  GetAccountsLookup() {

    var url = AccountRelationApi.EnvironmentAccountsLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));

  }

  GetFiscalPeriodsLookupAsync(companyId:number,userId:number) {

    var url = LookupApi.UserAccessibleCompanyFiscalPeriods;
    url = String.Format(url, companyId, userId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));

  }

  GetCompanyLookupAsync(userId: number) {

    var url = LookupApi.UserAccessibleCompanies;
    url = String.Format(url, userId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));

  }

  GetUserLookupAsync() {

    var url = LookupApi.Users;    
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));
  }

  GetEntityLookupAsync() {

    var url = LookupApi.EntityTypes;    
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));
  }

  GetSystemEntityLookupAsync() {

    var url = LookupApi.SystemEntityTypes;    
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));
  }

  GetDetailAccountsLookup() {

    var url = AccountRelationApi.EnvironmentDetailAccountsLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));

  }

  GetCostCentersLookup() {

    var url = AccountRelationApi.EnvironmentCostCentersLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));

  }

  GetProjectsLookup() {

    var url = AccountRelationApi.EnvironmentProjectsLookup;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));

  }

  GetCurrenciesLookup() {
    var options = { headers: this.httpHeaders };
    return this.http.get(LookupApi.Currencies, options).pipe(
      map(response => <any>(<Response>response)));
  }

  GetAccountGroupsLookup() {
    var url = LookupApi.AccountGroups;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));
  }

  GetInventoryAccountsLookup() {
    var url = LookupApi.InventoryAccounts;
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));
  }
}
