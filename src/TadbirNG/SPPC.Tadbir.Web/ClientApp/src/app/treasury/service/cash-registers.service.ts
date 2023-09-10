import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService, String } from '@sppc/shared/class';
import { RelatedItems } from '@sppc/shared/models';
import { BrowserStorageService } from '@sppc/shared/services';
import { map } from 'rxjs';
import { CashRegisterApi } from './api/cashRegistersApi';

export class CashRegistersInfo {
  id: number = 0;
  name: string;
  fiscalPeriodId: number;
  branchId: number;
  branchScope: number;
  description: string;

  static getInstance()
  {
    const instance = new CashRegistersInfo();
    instance.id = 0;
    instance.branchId = 0;
    instance.fiscalPeriodId = 0;
    instance.description = "";
    instance.name = "";
    instance.branchScope = 0;
    
    return instance;
  }
}

@Injectable({
  providedIn: 'root'
})
export class CashRegistersService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getCashRegisterRoles(cashRegisterId: number) {
    var url = String.Format(CashRegisterApi.CashRegisterUsers, cashRegisterId);
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  modifiedCashRegisterRoles(cashRegisterRoles: RelatedItems) {
    var body = JSON.stringify(cashRegisterRoles);
    var options = { headers: this.httpHeaders };
    var url = String.Format(CashRegisterApi.CashRegisterUsers, cashRegisterRoles.id);
    return this.http.put(url, body, options).pipe(map((res) => res));
  }
}
