import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { AccountApi } from './api/accountApi';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService, BaseService, String } from '@sppc/shared';
import { Account } from '@sppc/finance';


export class AccountInfo implements Account {
  currencyId: number = 1;
  isActive: boolean = true;
  isCurrencyAdjustable: boolean = true;
  turnoverMode: number = -1;
  parentId?: number;
  groupId?: number;
  fiscalPeriodId: number;
  branchId: number;
  companyId: number;
  childCount: number;
  id: number = 0;
  branchScope: number;
  code: string;
  fullCode: string = "";
  name: string;
  level: number = 0;
  description?: string;
  //constructor(public id: number = 0, public code: string = "", public name: string = "", public groupId?: number,
  //      public fiscalPeriodId: number = 0, public description?: string = "", public branchScope: number = 0,
  //      public branchId: number = 0, public level: number = 0, public fullCode: string = "",
  //      public childCount: number = 0, public parentId: number = 0, public companyId: number = 0) { }

}


@Injectable()
export class AccountService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


  getAccountById(id: number) {
    var url = String.Format(AccountApi.Account, id);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

}
