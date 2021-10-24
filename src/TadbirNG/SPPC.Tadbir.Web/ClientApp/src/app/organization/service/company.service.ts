import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BrowserStorageService } from '@sppc/shared/services';
import { CompanyDb, FiscalPeriod, Branch } from '@sppc/organization/models';
import { BaseService } from '@sppc/shared/class';
import { Observable } from 'rxjs';
import { CompanyApi } from './api';
import { String } from '@sppc/shared/class/source';
import { RelatedItems } from '@sppc/shared/models';
import { RoleApi } from '@sppc/admin/service/api';


//export class CompanyInfo implements Company {
//  parentId?: number | undefined;
//  childCount: number = 0;
//  id: number = 0;
//  name: string;
//  description?: string | undefined;

//}

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

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }


  public companyValidation(apiUrl: string, model: CompanyDb): Observable<string> {
    var body = JSON.stringify(model);
    return this.http.post(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }


  public insertInitialCompany(apiUrl: string, company: CompanyDb, branch: Branch, fiscalperiod: FiscalPeriod): Observable<any> {
    var body = JSON.stringify({ company: company, branch: branch, fiscalperiod: fiscalperiod });
    return this.http.post(apiUrl, body, this.option)
      .map(res => res)
      .catch(this.handleError);
  }

  getCompanyRoles(companyId: number) {
    var url = String.Format(CompanyApi.CompanyRoles, companyId);
    var options = { headers: this.httpHeaders };
    return this.http.get(url, options)
      .map(response => <any>(<Response>response));
  }

  modifiedCompanyRoles(companyRoles: RelatedItems) {
    var body = JSON.stringify(companyRoles);

    var options = { headers: this.httpHeaders };

    var url = String.Format(CompanyApi.CompanyRoles, companyRoles.id);

    return this.http.put(url, body, options)
      .map(res => res)
      .catch(this.handleError);
  }
}