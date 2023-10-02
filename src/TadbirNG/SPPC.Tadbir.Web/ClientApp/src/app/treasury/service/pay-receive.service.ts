import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';
import { PayReceive } from '../models/payReceive';
import { map } from 'rxjs';

export class PayReceiveInfo implements PayReceive {
  id: number = 0;
  fiscalPeriodId: number;
  branchId: number;
  date: Date;
  textNo: string;
  reference: string;
  confirmedById: number;
  approvedById: number;
  currencyRate: number;
  description: string;
  issuedByName: string;
  confirmedByName: string;
  approvedByName: string;
  currencyId: number;
  isApproved: boolean;
  isConfirmed: boolean;
  hasNext: boolean;
  hasPrevious: boolean;
  accountAmountsSum: number;
  cashAmountsSum: number;
  type: number;
  isRegistered: boolean = false;
}

@Injectable({
  providedIn: 'root'
})
export class PayReceiveService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  public changeStatus(apiUrl: string) {
    return this.http.put(apiUrl, null, this.option).pipe(map((res) => res));
  }

  public registerForm(apiUrl: string) {
    return this.http.post(apiUrl, null, this.option).pipe(map((res) => res));
  }

  public undoRegister(apiUrl: string) {
    return this.http.delete(apiUrl, this.option).pipe(map((res) => res));
  }

  public getAny(apiUrl: string) {
    return this.http.get(apiUrl, this.option).pipe(map((res) => res));
  }
}
