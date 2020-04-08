import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { AccountApi } from './api/accountApi';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { Account } from '@sppc/finance/models';
import { BaseService } from '@sppc/shared/class';
import { String } from '@sppc/shared/class/source';
import { AccountFullData } from '../models/accountFullData';
import { CustomerTaxInfo } from '../models/customerTaxInfo';


export class AccountInfo implements Account {
  currencyId: number = 1;
  isActive: boolean = true;
  isCurrencyAdjustable: boolean = true;
  turnoverMode: string;
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

export class CustomerTaxInfoModel implements CustomerTaxInfo {
  id: number = 0;
  accountId: number;
  customerFirstName: string;
  customerName: string;
  personType: number;
  buyerType: number;
  economicCode: string;
  address: string;
  nationalCode: string;
  perCityCode: string;
  phoneNo: string;
  mobileNo: string;
  postalCode: string;
  description?: string;
}

export class AccountFullDataInfo implements AccountFullData {

  constructor() {
    this.account = new AccountInfo();
    this.customerTaxInfo = new CustomerTaxInfoModel();
  }

  account: Account;
  customerTaxInfo: CustomerTaxInfo;
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
