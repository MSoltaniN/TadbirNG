import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { HttpClient, HttpRequest, HttpHeaders } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { FiscalPeriodApi } from '@sppc/organization/service/api';
import { CurrencyApi } from '@sppc/finance/service/api';
import { String, BaseService } from '@sppc/shared/class';
import { Currency, CurrencyRate } from '@sppc/finance/models';
import { RelatedItems } from '@sppc/shared/models';
import { Time } from '@angular/common';
import { environment } from '@sppc/env/environment';
import { Observable } from 'rxjs';


  export class CurrencyEntity implements Currency {
    id: number = 0;
    name: string;
    country: string;
    code: string;
    minorUnit: string;
    minorUnitKey: string;
    multiplier: number;
    decimalCount: number;
    description?: string;
    branchScope: number = 0;
    isActive: boolean;
    branchId: number;
    branchName: string;
    taxCode: number;
    isDefaultCurrency: boolean = false;
  }

export class CurrencyRateInfo implements CurrencyRate {
  currencyId: number;
  branchId: number;
  branchName: string;
  id: number = 0;
  date: Date;
  time: Time;
  multiplier: number;
  branchScope: number;
  description: string;
}

@Injectable()
export class CurrencyService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  postFile(file: File) {
    var currentContext = this.bStorageService.getCurrentUser();
    const formData: FormData = new FormData();
    formData.append(file.name, file, file.name);
    formData.append("X-Tadbir-AuthTicket", currentContext ? currentContext.ticket : "");

    const uploadReq = new HttpRequest('POST', CurrencyApi.TaxCurrencies, formData, { reportProgress: true, });

    return this.http.request(uploadReq);
  }

  public insertDefaultCurrency(apiUrl: string): Observable<string> {

    return this.http.post(apiUrl, {},this.option)
      .map(res => res)
      .catch(this.handleError);
  }

}
