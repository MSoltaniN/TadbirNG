import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { FiscalPeriodApi } from './api/index';
import { HttpClient } from '@angular/common/http';
import { String, BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';
import { FiscalPeriod } from '..';
import { RelatedItems } from '@sppc/shared/models';



export class FiscalPeriodInfo implements FiscalPeriod {
    companyId: number;
    id: number=0;
    name: string;
    startDate: Date = new Date();
    endDate: Date = new Date();
    description?: string | undefined;

}

@Injectable()
export class FiscalPeriodService extends BaseService{

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
