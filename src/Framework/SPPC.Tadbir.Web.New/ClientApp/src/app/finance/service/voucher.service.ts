import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";
import { BrowserStorageService } from '@sppc/shared/services';
import { Voucher } from '@sppc/finance/models';
import { BaseService, FilterExpression, Filter } from '@sppc/shared/class';
import { InventoryBalance } from '../models/inventoryBalance';
import { VoucherApi } from './api';
import { VoucherStatusResource } from '../enum';


export class VoucherInfo implements Voucher {
  dailyNo: number;
  fiscalPeriodId: number=0;
  branchId: number=0;
  statusId: number;
  statusName: string;
  debitSum: number;
  creditSum: number;
  id: number=0;
  no: number;
  date: Date;
  reference: string;
  association: string;
  isBalanced: boolean;
  type: number;
  subjectType: number;
  saveCount: number;
  issuedById: number;
  modifiedById: number;
  confirmedById?: number;
  approvedById?: number;
  issuerName: string;
  modifierName: string;
  confirmerName: string;
  approverName: string;
  description?: string;
  originName: string;
  originId: string;
  hasPrevious: boolean;
  hasNext: boolean;
  typeName: string;
}

export class InventoryBalanceInfo implements InventoryBalance {  
  accountId: number;
  branchId: number;
  debitBalance: number;
  creditBalance: number;
}

@Injectable()
export class VoucherService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  public changeVouchersStatus(apiUrl: string, models: number[]): Observable<string> {
    let body = JSON.stringify({ paraph: '', items: models });
    return this.http.put(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }
  public changeVoucherStatus(apiUrl: string): Observable<string> {

    return this.http.put(apiUrl, null, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public getClosingAccountsVoucher(invetoryBalances: Array<InventoryBalance>) {

    var body = JSON.stringify(invetoryBalances);

    return this.http.put(VoucherApi.ClosingAccountsVoucher, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public getClosingAccountsVoucherMode1() {
    
    return this.http.get(VoucherApi.ClosingAccountsVoucher, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public getOpeningVoucherQuery() {
    return this.http.get(VoucherApi.OpeningVoucherQuery, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public getOpeningVoucher(isDefault:boolean) {

    var url = VoucherApi.OpeningVoucher;
    if (isDefault) {
      //در صورت تأیید پیغام توسط کاربر : باید سند افتتاحیه رو با کلی آرتیکل با مبلغ صفر ایجاد کنیم
      url += "?isDefault=true";
    }
    else
    {
      //در صورت انصراف از پیغام توسط کاربر : باید سند افتتاحیه خالی درست کنیم
      url += "?isDefault=false";
    }

    return this.http.get(url, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  public getVoucherNumberByStatus(apiUrl: string, filter?: FilterExpression) {
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

  public getStatusFilter(voucherStatus:string) {
    let statusFilter: Filter[] = [];    
    var statusKey = "";
    var urlKey = "";

    switch (voucherStatus) {
      case "2": {
        statusFilter.push(new Filter("StatusId", "1", "< {0}", "System.Int32"));
        statusKey = VoucherStatusResource.NotCommitted;
        urlKey = "/#/finance/voucher/committed";
        break;
      }
      case "3": {
        statusFilter.push(new Filter("StatusId", "2", "!= {0}", "System.Int32"));
        statusKey = VoucherStatusResource.NotFinalized;
        urlKey = "/#/finance/voucher/finalized";
        break;
      }
      case "4": {
        statusFilter.push(new Filter("ConfirmedById", "", "== null", ""));
        statusKey = VoucherStatusResource.NotConfirmed;
        urlKey = "/#/finance/voucher/confirmed";
        break;
      }
      case "5": {        
        statusFilter.push(new Filter("ApprovedById", "", "== null", ""));
        statusKey = VoucherStatusResource.NotApproved;
        urlKey = "/#/finance/voucher/approved";
        break;
      }
      default:
    }
    
    return { filter: statusFilter, key: statusKey, url: urlKey };
  }

}
