import { HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Response } from "@angular/http";
import { UserApi } from "@sppc/admin/service/api";
import { environment } from "@sppc/env/environment";
import { FiscalPeriodApi } from "@sppc/organization/service/api";
import { BaseService } from "@sppc/shared/class/base.service";
import { String } from "@sppc/shared/class/source";
import { CompanyLogin } from "@sppc/shared/models";
import { BrowserStorageService } from "@sppc/shared/services";
import { LookupApi } from "@sppc/shared/services/api";
import { Observable, of } from "rxjs";
import { map } from "rxjs/operators";
import { PermissionBrief } from "..";
import { Context } from "../models/context";

export class ContextInfo implements Context {
  userName: string = "";
  firstName: string = "";
  lastName: string = "";
  ticket: string = "";
  fpId: number = 0;
  branchId: number = 0;
  companyId: number = 0;
  inventoryMode: number = 0;
  branchName: string;
  companyName: string;
  fiscalPeriodName: string;
  permissions: PermissionBrief[];
  roles: number[];
  lastLoginDate: string = null;
}

export class CompanyLoginInfo implements CompanyLogin {
  companyId?: number;
  branchId?: number;
  fiscalPeriodId?: number;
}

@Injectable()
export class AuthenticationService extends BaseService {
  constructor(
    public http: HttpClient,
    public bStorageService: BrowserStorageService
  ) {
    super(http, bStorageService);
  }

  login(username: string, password: string, remember: boolean) {
    var header = new HttpHeaders();
    header = header.append(
      "Accept-Language",
      this.httpHeaders.get("Accept-Language")
    );

    return this.http
      .put(
        environment.BaseUrl + "/users/login",
        { username: username, password: password } /*, this.options*/,
        { headers: header, observe: "response" }
      )
      .pipe(
        map((response) => {
          if (response.headers != null) {
            let ticket = response.headers.get("X-Tadbir-AuthTicket");

            //var contextInfo = JSON.parse(atob(ticket));
            var contextInfo = this.parseJwt(ticket);

            if (response.status == 200 && ticket != null) {
              var user = new ContextInfo();

              user.ticket = ticket;
              user.userName = username;
              user.lastLoginDate = contextInfo.TadbirContext.LastLoginDate;
              //user.roles = contextInfo.user.roles;
              user.roles = contextInfo.TadbirContext.Roles;
              this.bStorageService.setContext(user, remember);
            }
          }
        })
      );
  }

  parseJwt(token) {
    var base64Url = token.split(".")[1];
    var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    var jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    return JSON.parse(jsonPayload);
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

    header = header.delete("X-Tadbir-AuthTicket");
    header = header.delete("Content-Type");
    header = header.append(
      "Accept-Language",
      this.httpHeaders.get("Accept-Language")
    );
    header = header.append("Content-Type", "application/json; charset=utf-8");
    header = header.append("X-Tadbir-AuthTicket", ticket);

    if (ticket == "") return of();
    //var jsonContext = atob(ticket);
    //var context = JSON.parse(jsonContext);
    var contextInfo = this.parseJwt(ticket);
    var userId = contextInfo.TadbirContext.Id;
    var url = String.Format(LookupApi.UserAccessibleCompanies, userId);
    var options = { headers: header };
    return this.http
      .get(url, { headers: header })
      .pipe(map((response) => <any>(<Response>response)));
  }

  getBranches(companyId: number, ticket: string): Observable<any> {
    var header = new HttpHeaders();

    header = header.delete("X-Tadbir-AuthTicket");
    header = header.delete("Content-Type");
    header = header.append(
      "Accept-Language",
      this.httpHeaders.get("Accept-Language")
    );
    header = header.append("Content-Type", "application/json; charset=utf-8");
    header = header.append("X-Tadbir-AuthTicket", ticket);

    if (ticket == "") return of();
    //var jsonContext = atob(ticket);
    //var context = JSON.parse(jsonContext);
    var contextInfo = this.parseJwt(ticket);

    var userId = contextInfo.TadbirContext.Id;
    var url = String.Format(
      LookupApi.UserAccessibleCompanyBranches,
      companyId,
      userId
    );
    return this.http
      .get(url, { headers: header })
      .pipe(map((response) => <any>(<Response>response)));
  }

  getFiscalPeriod(companyId: number, ticket: string): Observable<any> {
    var header = new HttpHeaders();
    header = header.delete("X-Tadbir-AuthTicket");
    header = header.delete("Content-Type");
    header = header.append(
      "Accept-Language",
      this.httpHeaders.get("Accept-Language")
    );
    header = header.append("Content-Type", "application/json; charset=utf-8");
    header = header.append("X-Tadbir-AuthTicket", ticket);

    if (ticket == "") return of();
    var contextInfo = this.parseJwt(ticket);

    var userId = contextInfo.TadbirContext.Id;
    var url = String.Format(
      LookupApi.UserAccessibleCompanyFiscalPeriods,
      companyId,
      userId
    );
    return this.http
      .get(url, { headers: header })
      .pipe(map((response) => <any>(<Response>response)));
  }

  getFiscalPeriodById(fpId: number, ticket: string) {
    var header = new HttpHeaders();
    header = header.delete("X-Tadbir-AuthTicket");
    header = header.delete("Content-Type");
    header = header.append(
      "Accept-Language",
      this.httpHeaders.get("Accept-Language")
    );
    header = header.append("Content-Type", "application/json; charset=utf-8");
    header = header.append("X-Tadbir-AuthTicket", ticket);

    return this.http
      .get(String.Format(FiscalPeriodApi.FiscalPeriod, fpId), {
        headers: header,
      })
      .pipe(map((response) => <any>(<Response>response)));
  }

  getCompanyTicket(model: CompanyLogin, ticket: string): Observable<any> {
    var header = new HttpHeaders();
    header = header.append(
      "Accept-Language",
      this.httpHeaders.get("Accept-Language")
    );
    header = header.append("Content-Type", "application/json; charset=utf-8");
    header = header.append("X-Tadbir-AuthTicket", ticket);

    var body = JSON.stringify(model);

    var url = UserApi.UserCompanyLoginStatus;
    //var options = { headers: header, observe: "response" };
    return this.http
      .put(url, body, { headers: header, observe: "response" })
      .pipe(map((response) => <any>(<HttpResponse<any>>response)));
  }

  checkSpecialPassword(
    specialPassword: string,
    ticket: string
  ): Observable<any> {
    var header = new HttpHeaders();
    header = header.append(
      "Accept-Language",
      this.httpHeaders.get("Accept-Language")
    );
    header = header.append("Content-Type", "application/json; charset=utf-8");
    header = header.append("X-Tadbir-AuthTicket", ticket);

    var body = JSON.stringify(specialPassword);

    if (ticket == "") return of();
    var url = UserApi.SpecialPassword;
    return this.http
      .put(url, body, { headers: header })
      .pipe(map((response) => <any>(<Response>response)));
  }
}
