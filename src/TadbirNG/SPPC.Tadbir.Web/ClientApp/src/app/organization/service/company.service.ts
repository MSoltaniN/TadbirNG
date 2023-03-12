import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Branch, CompanyDb, FiscalPeriod } from "@sppc/organization/models";
import { BaseService } from "@sppc/shared/class";
import { String } from "@sppc/shared/class/source";
import { RelatedItems } from "@sppc/shared/models";
import { BrowserStorageService } from "@sppc/shared/services";
import { Observable } from "rxjs";
import { map } from "rxjs/operators";
import { CompanyApi } from "./api";

export class CompanyDbInfo implements CompanyDb {
  id: number = 0;
  name: string;
  dbName: string;
  dbPath: string;
  description?: string | undefined;
  server: string;
  userName: string;
  password: string;
}

@Injectable()
export class CompanyService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  public companyValidation(apiUrl: string, model: CompanyDb) {
    var body = JSON.stringify(model);
    return this.http.post(apiUrl, body, this.option).pipe(map((res) => res));
  }

  public insertInitialCompany(
    apiUrl: string,
    company: CompanyDb,
    branch: Branch,
    fiscalperiod: FiscalPeriod
  ): Observable<any> {
    var body = JSON.stringify({
      company: company,
      branch: branch,
      fiscalperiod: fiscalperiod,
    });
    return this.http.post(apiUrl, body, this.option).pipe(map((res) => res));
  }

  getCompanyRoles(companyId: number) {
    var url = String.Format(CompanyApi.CompanyRoles, companyId);
    var options = { headers: this.httpHeaders };
    return this.http
      .get(url, options)
      .pipe(map((response) => <any>(<Response>response)));
  }

  modifiedCompanyRoles(companyRoles: RelatedItems) {
    var body = JSON.stringify(companyRoles);

    var options = { headers: this.httpHeaders };

    var url = String.Format(CompanyApi.CompanyRoles, companyRoles.id);

    return this.http.put(url, body, options).pipe(map((res) => res));
  }
}
