import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import { BaseService, BrowserStorageService, RelatedItems } from '@sppc/shared';
import { FiscalPeriodApi } from '@sppc/organization';
import { String } from '@sppc/shared/class';
import { Currency } from '..';


export class CurrencyInfo implements Currency {
  id: number = 0;
  name: string;
  country: string;
  code: string;
  minorUnit: string;
  multiplier: number;
  decimalCount: number;
  description?: string;
  branchScope: number = 0;
  isActive: boolean;
}

@Injectable()
export class CurrencyService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  getFiscalPeriodRoles(fPeriodId: number) {
    var url = String.Format(FiscalPeriodApi.FiscalPeriodRoles, fPeriodId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

  modifiedFiscalPeriodRoles(fPeriodIdRoles: RelatedItems) {
    var body = JSON.stringify(fPeriodIdRoles);

    var options = { headers: this.httpHeaders };

    var url = String.Format(FiscalPeriodApi.FiscalPeriodRoles, fPeriodIdRoles.id);
    return this.http.put(url, body, options)
      .map(res => res)
      .catch(this.handleError);
  }

}
