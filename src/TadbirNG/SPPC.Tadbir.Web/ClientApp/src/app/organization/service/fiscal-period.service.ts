import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

import { FiscalPeriod } from "@sppc/organization/models";
import { BaseService, String } from "@sppc/shared/class";
import { RelatedItems } from "@sppc/shared/models";
import { BrowserStorageService } from "@sppc/shared/services";
import { map } from "rxjs/operators";
import { FiscalPeriodApi } from "./api/index";

export class FiscalPeriodInfo implements FiscalPeriod {
  companyId: number;
  id: number = 0;
  name: string;
  startDate: Date = new Date();
  endDate: Date = new Date();
  description?: string | undefined;
}

@Injectable()
export class FiscalPeriodService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  getFiscalPeriodRoles(fPeriodId: number) {
    var url = String.Format(FiscalPeriodApi.FiscalPeriodRoles, fPeriodId);
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  modifiedFiscalPeriodRoles(fPeriodIdRoles: RelatedItems) {
    var body = JSON.stringify(fPeriodIdRoles);

    var options = { headers: this.httpHeaders };

    var url = String.Format(
      FiscalPeriodApi.FiscalPeriodRoles,
      fPeriodIdRoles.id
    );
    return this.http.put(url, body, options).pipe(map((res) => res));
  }

  public fiscalPeriodValidation(apiUrl: string, model: FiscalPeriod) {
    var body = JSON.stringify(model);
    return this.http.post(apiUrl, body, this.option).pipe(map((res) => res));
  }
}
