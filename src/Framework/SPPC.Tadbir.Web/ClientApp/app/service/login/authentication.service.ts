import { Injectable } from '@angular/core';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'

import { Environment } from "../../enviroment";

import { Context } from "../../model/context";
import { PermissionBrief, CompanyLogin } from '../../model/index';

import { String } from '../../class/source';
import { LookupApi, FiscalPeriodApi, UserApi } from '../api/index';
import { HttpClient, HttpHeaders, HttpResponse } from '@angular/common/http';

export class ContextInfo implements Context {
    userName: string = "";
    password: string = "";
    firstName: string = "";
    lastName: string = "";
    ticket: string = "";
    fpId: number = 0;
    branchId: number = 0;
    companyId: number = 0;
    permissions: PermissionBrief[];
}

export class CompanyLoginInfo implements CompanyLogin {
    companyId?: number;
    branchId?: number;
    fiscalPeriodId?: number;
}


@Injectable()
export class AuthenticationService {


    constructor(private http: HttpClient) {

    }

    login(username: string, password: string, remember: boolean) {
        return this.http.put(Environment.BaseUrl + '/users/login', { username: username, password: password }/*, this.options*/, { observe: "response"})
            .map((response) => {
                // login successful if there's a jwt token in the response   
                
                if (response.headers != null) {
                    let ticket = response.headers.get('X-Tadbir-AuthTicket');
                    if (response.status == 200 && ticket != null) {
                        var user = new ContextInfo();
                        user.ticket = ticket;
                        user.userName = username;

                        // در صورتی که تیک بخاطر سپردن بخورد در حافظه storage ذخیره می شود
                        if (remember)
                            localStorage.setItem('currentContext', JSON.stringify(user));
                        else // در صورتی که تیک بخاطر سپردن بخورد در حافظه session ذخیره می شود
                            sessionStorage.setItem('currentContext', JSON.stringify(user));
                    }
                }
            })

    }

    islogin() {
        if (localStorage.getItem('currentContext')) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            if (currentContext.userName != '') {
                return true;
            }
        }
        else if (sessionStorage.getItem('currentContext')) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            if (currentContext.userName != '') {
                return true;
            }
        }

        return false;
    }

    isRememberMe() {
        if (localStorage.getItem('currentContext')) {
            return true;
        }

        return false;
    }

    getCurrentUser(): ContextInfo | null {
        var currentUser: ContextInfo;
        var item: string | null = '';
        if (localStorage.getItem('currentContext')) {
            item = localStorage.getItem('currentContext');
        }
        else if (sessionStorage.getItem('currentContext')) {
            item = sessionStorage.getItem('currentContext');
        }

        if (item) {
            var currentUser: ContextInfo = item !== null ? JSON.parse(item) : null;
            return currentUser;
        }

        return null;
    }

    logout() {
        // remove user from local storage to log user out
        if (localStorage.getItem('currentContext'))
            localStorage.removeItem('currentContext');

        if (sessionStorage.getItem('currentContext'))
            sessionStorage.removeItem('currentContext');

        if (localStorage.getItem('fiscalPeriod'))
            localStorage.removeItem('fiscalPeriod');

        if (sessionStorage.getItem('fiscalPeriod'))
            sessionStorage.removeItem('fiscalPeriod');
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