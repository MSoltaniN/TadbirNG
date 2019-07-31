"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/map");
var source_1 = require("../../class/source");
var index_1 = require("../api/index");
var http_1 = require("@angular/common/http");
var environment_1 = require("../../../environments/environment");
var ContextInfo = /** @class */ (function () {
    function ContextInfo() {
        this.userName = "";
        this.password = "";
        this.firstName = "";
        this.lastName = "";
        this.ticket = "";
        this.fpId = 0;
        this.branchId = 0;
        this.companyId = 0;
    }
    return ContextInfo;
}());
exports.ContextInfo = ContextInfo;
var CompanyLoginInfo = /** @class */ (function () {
    function CompanyLoginInfo() {
    }
    return CompanyLoginInfo;
}());
exports.CompanyLoginInfo = CompanyLoginInfo;
var AuthenticationService = /** @class */ (function () {
    function AuthenticationService(http) {
        this.http = http;
    }
    AuthenticationService.prototype.login = function (username, password, remember) {
        return this.http.put(environment_1.environment.BaseUrl + '/users/login', { username: username, password: password } /*, this.options*/, { observe: "response" })
            .map(function (response) {
            // login successful if there's a jwt token in the response   
            if (response.headers != null) {
                var ticket = response.headers.get('X-Tadbir-AuthTicket');
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
        });
    };
    AuthenticationService.prototype.islogin = function () {
        if (localStorage.getItem('currentContext')) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            if (currentContext.userName != '') {
                return true;
            }
        }
        else if (sessionStorage.getItem('currentContext')) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            if (currentContext.userName != '') {
                return true;
            }
        }
        return false;
    };
    AuthenticationService.prototype.isRememberMe = function () {
        if (localStorage.getItem('currentContext')) {
            return true;
        }
        return false;
    };
    AuthenticationService.prototype.getCurrentUser = function () {
        var currentUser;
        var item = '';
        if (localStorage.getItem('currentContext')) {
            item = localStorage.getItem('currentContext');
        }
        else if (sessionStorage.getItem('currentContext')) {
            item = sessionStorage.getItem('currentContext');
        }
        if (item) {
            var currentUser = item !== null ? JSON.parse(item) : null;
            return currentUser;
        }
        return null;
    };
    AuthenticationService.prototype.logout = function () {
        // remove user from local storage to log user out
        if (localStorage.getItem('currentContext'))
            localStorage.removeItem('currentContext');
        if (sessionStorage.getItem('currentContext'))
            sessionStorage.removeItem('currentContext');
        if (localStorage.getItem('fiscalPeriod'))
            localStorage.removeItem('fiscalPeriod');
        if (sessionStorage.getItem('fiscalPeriod'))
            sessionStorage.removeItem('fiscalPeriod');
        sessionStorage.removeItem(environment_1.SessionKeys.CurrentRoute);
    };
    AuthenticationService.prototype.getCompanies = function (userName, ticket) {
        var header = new http_1.HttpHeaders();
        header = header.delete('X-Tadbir-AuthTicket');
        header = header.delete('Content-Type');
        header = header.append('Content-Type', 'application/json; charset=utf-8');
        header = header.append('X-Tadbir-AuthTicket', ticket);
        if (ticket == '')
            return Observable_1.Observable.empty();
        var jsonContext = atob(ticket);
        var context = JSON.parse(jsonContext);
        var userId = context.user.id;
        var url = source_1.String.Format(index_1.LookupApi.UserAccessibleCompanies, userId);
        var options = { headers: header };
        return this.http.get(url, { headers: header })
            .map(function (response) { return response; });
    };
    AuthenticationService.prototype.getBranches = function (companyId, ticket) {
        var header = new http_1.HttpHeaders();
        header = header.delete('X-Tadbir-AuthTicket');
        header = header.delete('Content-Type');
        header = header.append('Content-Type', 'application/json; charset=utf-8');
        header = header.append('X-Tadbir-AuthTicket', ticket);
        if (ticket == '')
            return Observable_1.Observable.empty();
        var jsonContext = atob(ticket);
        var context = JSON.parse(jsonContext);
        var userId = context.user.id;
        var url = source_1.String.Format(index_1.LookupApi.UserAccessibleCompanyBranches, companyId, userId);
        return this.http.get(url, { headers: header })
            .map(function (response) { return response; });
    };
    AuthenticationService.prototype.getFiscalPeriod = function (companyId, ticket) {
        var header = new http_1.HttpHeaders();
        header = header.delete('X-Tadbir-AuthTicket');
        header = header.delete('Content-Type');
        header = header.append('Content-Type', 'application/json; charset=utf-8');
        header = header.append('X-Tadbir-AuthTicket', ticket);
        if (ticket == '')
            return Observable_1.Observable.empty();
        var jsonContext = atob(ticket);
        var context = JSON.parse(jsonContext);
        var userId = context.user.id;
        var url = source_1.String.Format(index_1.LookupApi.UserAccessibleCompanyFiscalPeriods, companyId, userId);
        return this.http.get(url, { headers: header })
            .map(function (response) { return response; });
    };
    AuthenticationService.prototype.getFiscalPeriodById = function (fpId, ticket) {
        var header = new http_1.HttpHeaders();
        header = header.delete('X-Tadbir-AuthTicket');
        header = header.delete('Content-Type');
        header = header.append('Content-Type', 'application/json; charset=utf-8');
        header = header.append('X-Tadbir-AuthTicket', ticket);
        return this.http.get(source_1.String.Format(index_1.FiscalPeriodApi.FiscalPeriod, fpId), { headers: header })
            .map(function (response) { return response; });
    };
    AuthenticationService.prototype.getCompanyTicket = function (model, ticket) {
        var header = new http_1.HttpHeaders();
        //header = header.delete('X-Tadbir-AuthTicket');
        //header = header.delete('Content-Type');
        header = header.append('Content-Type', 'application/json; charset=utf-8');
        header = header.append('X-Tadbir-AuthTicket', ticket);
        var body = JSON.stringify(model);
        var url = index_1.UserApi.UserCompanyLoginStatus;
        //var options = { headers: header, observe: "response" };
        return this.http.put(url, body, { headers: header, observe: "response" })
            .map(function (response) { return response; });
    };
    AuthenticationService = __decorate([
        core_1.Injectable()
    ], AuthenticationService);
    return AuthenticationService;
}());
exports.AuthenticationService = AuthenticationService;
//# sourceMappingURL=authentication.service.js.map