import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import "rxjs/Rx";
import { HttpClient } from "@angular/common/http";
import { AccountItemBriefInfo } from './accountRelations.service';
import { BaseService, BrowserStorageService, LookupApi, FilterExpression } from '@sppc/shared';
import { AccountItemBrief, FullAccount } from '..';



export class FullAccountInfo implements FullAccount {

  constructor() {
    this.account = new AccountItemBriefInfo();
    this.detailAccount = new AccountItemBriefInfo();
    this.costCenter = new AccountItemBriefInfo();
    this.project = new AccountItemBriefInfo();
  }

  account: AccountItemBrief;
  detailAccount: AccountItemBrief;
  costCenter: AccountItemBrief;
  project: AccountItemBrief;
}


@Injectable()
export class FullAccountService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  GetAccountsLookup() {
    var options = { headers: this.httpHeaders };
    return this.http.get(LookupApi.EnvironmentAccounts, options)
      .map(response => <any>(<Response>response));

  }

  GetDetailAccountsLookup() {
    var options = { headers: this.httpHeaders };
    return this.http.get(LookupApi.EnvironmentDetailAccounts, options)
      .map(response => <any>(<Response>response));
  }

  GetCostCentersLookup() {
    var options = { headers: this.httpHeaders };
    return this.http.get(LookupApi.EnvironmentCostCenters, options)
      .map(response => <any>(<Response>response));
  }

  GetProjectsLookup() {
    var options = { headers: this.httpHeaders };
    return this.http.get(LookupApi.EnvironmentProjects, options)
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
