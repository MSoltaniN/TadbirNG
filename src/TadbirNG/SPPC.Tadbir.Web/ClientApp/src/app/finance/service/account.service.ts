
import {map} from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { AccountApi } from './api/accountApi';
import { HttpClient, HttpRequest } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { Account, AccountOwner } from '@sppc/finance/models';
import { BaseService } from '@sppc/shared/class';
import { String } from '@sppc/shared/class/source';
import { AccountFullData } from '../models/accountFullData';
import { CustomerTaxInfo } from '../models/customerTaxInfo';
import { AccountHolder } from '../models/index';
import { CurrencyApi } from './api';


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
  provinceCode: string;
  cityCode: string;
  description?: string;
}

export class AccountOwnerInfo implements AccountOwner {

  constructor() {
    this.id = 0;
    this.accountHolders = new Array<AccountHolderInfo>();
  }

  id: number;
  accountId: number;
  bankName: string;
  accountType: number;
  bankBranchName: string;
  branchIndex: string;
  accountNumber: string;
  cardNumber: string;
  shabaNumber: string;
  description?: string;
  accountHolders: Array<AccountHolder>;
}

export class AccountHolderInfo implements AccountHolder {
  id: number;
  accountOwnerId: number;
  firstName: string;
  lastName: string;
  hasSignature: boolean;
}

export class AccountFullDataInfo implements AccountFullData {

  constructor() {
    this.account = new AccountInfo();
    this.customerTaxInfo = new CustomerTaxInfoModel();
    this.accountOwner = new AccountOwnerInfo();
  }

  account: Account;
  customerTaxInfo: CustomerTaxInfo;
  accountOwner: AccountOwner;
}

@Injectable()
export class AccountService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


  getAccountById(id: number) {
    var url = String.Format(AccountApi.Account, id);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options).pipe(
      map(response => <any>(<Response>response)));
  }
}
