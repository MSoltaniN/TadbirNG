import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Voucher } from "@sppc/finance/models";
import { BaseService, Filter, FilterExpression } from "@sppc/shared/class";
import { BrowserStorageService } from "@sppc/shared/services";
import { BehaviorSubject } from "rxjs";
import { map } from "rxjs/operators";
import { VoucherMessageResource } from "../enum";
import { InventoryBalance } from "../models/inventoryBalance";
import { VoucherApi } from "./api";

export class VoucherInfo implements Voucher {
  dailyNo: number;
  fiscalPeriodId: number = 0;
  branchId: number = 0;
  statusId: number;
  statusName: string;
  debitSum: number;
  creditSum: number;
  id: number = 0;
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
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  // use to empty voucheLine in New voucher Form
  changeVoucher$ = new BehaviorSubject<any>('');

  public changeVouchersStatus(apiUrl: string, models: number[]) {
    let body = JSON.stringify({ paraph: "", items: models });
    return this.http.put(apiUrl, body, this.option).pipe(map((res) => res));
  }
  public changeVoucherStatus(apiUrl: string) {
    return this.http.put(apiUrl, null, this.option).pipe(map((res) => res));
  }

  public getClosingAccountsVoucher(invetoryBalances: Array<InventoryBalance>) {
    var body = JSON.stringify(invetoryBalances);

    return this.http
      .put(VoucherApi.ClosingAccountsVoucher, body, this.option)
      .pipe(map((res) => res));
  }

  public getClosingAccountsVoucherMode1() {
    return this.http
      .get(VoucherApi.ClosingAccountsVoucher, this.option)
      .pipe(map((res) => res));
  }

  public getOpeningVoucherQuery() {
    return this.http
      .get(VoucherApi.OpeningVoucherQuery, this.option)
      .pipe(map((res) => res));
  }

  public getOpeningVoucher(isDefault: boolean) {
    var url = VoucherApi.OpeningVoucher;
    if (isDefault) {
      //در صورت تأیید پیغام توسط کاربر : باید سند افتتاحیه رو با کلی آرتیکل با مبلغ صفر ایجاد کنیم
      url += "?isDefault=true";
    } else {
      //در صورت انصراف از پیغام توسط کاربر : باید سند افتتاحیه خالی درست کنیم
      url += "?isDefault=false";
    }

    return this.http.get(url, this.option).pipe(map((res) => res));
  }

  public getVoucherNumberByStatus(apiUrl: string, filter?: FilterExpression) {
    var intMaxValue = 2147483647;
    var gridPaging = { pageIndex: 1, pageSize: intMaxValue };
    var postItem = { Paging: gridPaging, filter: filter, sortColumns: null };
    var searchHeaders = this.httpHeaders;
    var postBody = JSON.stringify(postItem);
    var base64Body = btoa(encodeURIComponent(postBody));
    if (searchHeaders)
      searchHeaders = searchHeaders.append("X-Tadbir-GridOptions", base64Body);
    var options = { headers: searchHeaders };

    return this.http
      .get(apiUrl, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  public getStatusFilter(
    voucherStatus: string,
    branchId: string = undefined,
    forVoucherEntity: boolean = false
  ) {
    let statusFilter: Filter[] = [];
    var statusKey = "";
    var urlKey = "";
    var entity = "Voucher";
    if (!forVoucherEntity) entity = "";
    var condition: string = "";
    if (branchId) condition = "?branchId=" + branchId;

    switch (voucherStatus) {
      case "2": {
        statusFilter.push(
          new Filter(entity + "StatusId", "1", "== {0}", "System.Int32")
        );
        if (branchId)
          statusFilter.push(
            new Filter(entity + "BranchId", branchId, " == {0}", "System.Int32")
          );
        statusKey = VoucherMessageResource.NotCommitted;
        urlKey = "/#/finance/voucher/committed" + condition;
        break;
      }
      case "3": {
        statusFilter.push(
          new Filter(entity + "StatusId", "3", "!= {0}", "System.Int32")
        );
        if (branchId)
          statusFilter.push(
            new Filter(entity + "BranchId", branchId, " == {0}", "System.Int32")
          );
        statusKey = VoucherMessageResource.NotFinalized;
        urlKey = "/#/finance/voucher/finalized" + condition;
        break;
      }
      case "4": {
        statusFilter.push(
          new Filter(entity + "ConfirmedById", "", "== null", "")
        );
        if (branchId)
          statusFilter.push(
            new Filter(entity + "BranchId", branchId, " == {0}", "System.Int32")
          );
        statusKey = VoucherMessageResource.NotConfirmed;
        urlKey = "/#/finance/voucher/confirmed" + condition;
        break;
      }
      case "5": {
        statusFilter.push(
          new Filter(entity + "ApprovedById", "", "== null", "")
        );
        if (branchId)
          statusFilter.push(
            new Filter(entity + "BranchId", branchId, " == {0}", "System.Int32")
          );
        statusKey = VoucherMessageResource.NotApproved;
        urlKey = "/#/finance/voucher/approved" + condition;
        break;
      }
      default:
    }

    return { filter: statusFilter, key: statusKey, url: urlKey };
  }
}
