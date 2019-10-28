import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Context } from '../models/context';
import { PermissionBrief } from '..';
import { BrowserStorageService } from '@sppc/shared/services';
import { environment } from '@sppc/env/environment';
import { FiscalPeriodApi } from '@sppc/organization/service/api';
import { UserApi } from '@sppc/admin/service/api';
import { String } from '@sppc/shared/class/source';
import { CompanyLogin } from '@sppc/shared/models';
import { LookupApi } from '@sppc/shared/services/api';


export class ContextInfo implements Context {
  userName: string = "";
  password: string = "";
  firstName: string = "";
  lastName: string = "";
  ticket: string = "";
  fpId: number = 0;
  branchId: number = 0;
  companyId: number = 0;
  branchName: string;
  companyName: string;
  fiscalPeriodName: string;
  permissions: PermissionBrief[];
  roles: number[];
}

export class CompanyLoginInfo implements CompanyLogin {
  companyId?: number;
  branchId?: number;
  fiscalPeriodId?: number;
}


@Injectable()
export class AuthenticationService {


  constructor(private http: HttpClient, public bStorageService: BrowserStorageService) {

  }

  login(username: string, password: string, remember: boolean) {
    return this.http.put(environment.BaseUrl + '/users/login', { username: username, password: password }/*, this.options*/, { observe: "response" })
      .map((response) => {
        // login successful if there's a jwt token in the response   

        if (response.headers != null) {
          let ticket = response.headers.get('X-Tadbir-AuthTicket');
          if (response.status == 200 && ticket != null) {
            var user = new ContextInfo();
            user.ticket = ticket;
            user.userName = username;

            this.bStorageService.setCurrentContext(user);
          }
        }
      })

  }

  islogin() {
    return this.bStorageService.islogin();
  }

  isRememberMe() {
    return this.bStorageService.isRememberMe();
  }

  logout() {
    // remove user from local storage to log user out
    this.bStorageService.removeCurrentContext();

    this.bStorageService.removeFiscalPeriod();

    this.bStorageService.removeCurrentRoute();
  }

  getCompanies(userName: string, ticket: string): Observable<any> {
    var header = new HttpHeaders();
    header = header.delete('X-Tadbir-AuthTicket');
    header = header.delete('Content-Type');

    header = header.append('Content-Type', 'application/json; charset=utf-8');
    header = header.append('X-Tadbir-AuthTicket', ticket);

    if (ticket == '') return Observable.empty<Response>();
    var jsonContext = atob(ticket);
    var context = JSON.parse(jsonContext);
    var userId = context.user.id;
    var url = String.Format(LookupApi.UserAccessibleCompanies, userId);
    var options = { headers: header };
    return this.http.get(url, { headers: header })
      .map(response => <any>(<Response>response));
  }

  getBranches(companyId: number, ticket: string): Observable<any> {
    var header = new HttpHeaders();
    header = header.delete('X-Tadbir-AuthTicket');
    header = header.delete('Content-Type');

    header = header.append('Content-Type', 'application/json; charset=utf-8');
    header = header.append('X-Tadbir-AuthTicket', ticket);

    if (ticket == '') return Observable.empty<Response>();
    var jsonContext = atob(ticket);
    var context = JSON.parse(jsonContext);
    var userId = context.user.id;
    var url = String.Format(LookupApi.UserAccessibleCompanyBranches, companyId, userId);
    return this.http.get(url, { headers: header })
      .map(response => <any>(<Response>response));
  }

  getFiscalPeriod(companyId: number, ticket: string): Observable<any> {
    var header = new HttpHeaders();
    header = header.delete('X-Tadbir-AuthTicket');
    header = header.delete('Content-Type');

    header = header.append('Content-Type', 'application/json; charset=utf-8');
    header = header.append('X-Tadbir-AuthTicket', ticket);

    if (ticket == '') return Observable.empty<Response>();
    var jsonContext = atob(ticket);
    var context = JSON.parse(jsonContext);
    var userId = context.user.id;
    var url = String.Format(LookupApi.UserAccessibleCompanyFiscalPeriods, companyId, userId);
    return this.http.get(url, { headers: header })
      .map(response => <any>(<Response>response));
  }

  getFiscalPeriodById(fpId: number, ticket: string) {
    var header = new HttpHeaders();
    header = header.delete('X-Tadbir-AuthTicket');
    header = header.delete('Content-Type');

    header = header.append('Content-Type', 'application/json; charset=utf-8');
    header = header.append('X-Tadbir-AuthTicket', ticket);

    return this.http.get(String.Format(FiscalPeriodApi.FiscalPeriod, fpId), { headers: header })
      .map(response => <any>(<Response>response));
  }

  getCompanyTicket(model: CompanyLogin, ticket: string): Observable<any> {
    var header = new HttpHeaders();
    //header = header.delete('X-Tadbir-AuthTicket');
    //header = header.delete('Content-Type');

    header = header.append('Content-Type', 'application/json; charset=utf-8');
    header = header.append('X-Tadbir-AuthTicket', ticket);

    var body = JSON.stringify(model);

    var url = UserApi.UserCompanyLoginStatus;
    //var options = { headers: header, observe: "response" };
    return this.http.put(url, body, { headers: header, observe: "response" })
      .map(response => <any>(<HttpResponse<any>>response));
  }
}
