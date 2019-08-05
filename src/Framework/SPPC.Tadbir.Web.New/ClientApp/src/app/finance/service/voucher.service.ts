import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs/Observable";
import { BrowserStorageService } from '@sppc/shared/services';
import { Voucher } from '@sppc/finance/models';
import { BaseService } from '@sppc/shared/class';


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
}

@Injectable()
export class VoucherService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


  public changeVoucherStatus(apiUrl: string): Observable<string> {

    return this.http.put(apiUrl, null, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

}
