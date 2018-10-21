import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { BaseService } from '../class/base.service';

import { Voucher, DocumentAction } from '../model/index';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from "rxjs/Observable";


export class VoucherInfo implements Voucher {
  fiscalPeriodId: number = 0;
  branchId: number = 0;
  statusId: number;
  statusName: string;
  debitSum: number;
  creditSum: number;
  id: number = 0;
  no: string;
  date: Date;
  description?: string;

}

@Injectable()
export class VoucherService extends BaseService {
  constructor(public http: HttpClient) {
    super(http);
  }


  public changeVoucherStatus(apiUrl: string): Observable<string> {

    return this.http.put(apiUrl, null, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

}
