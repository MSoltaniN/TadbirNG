"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
require("rxjs/Rx");
var source_1 = require("../class/source");
var base_service_1 = require("../class/base.service");
var index_1 = require("./api/index");
var FullAccountInfo = /** @class */ (function () {
    function FullAccountInfo() {
    }
    return FullAccountInfo;
}());
exports.FullAccountInfo = FullAccountInfo;
var FullAccountService = /** @class */ (function (_super) {
    __extends(FullAccountService, _super);
    function FullAccountService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    FullAccountService.prototype.GetAccountsLookup = function () {
        var url = source_1.String.Format(index_1.LookupApi.FiscalPeriodBranchAccounts, this.FiscalPeriodId, this.BranchId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    FullAccountService.prototype.GetDetailAccountsLookup = function () {
        var url = source_1.String.Format(index_1.LookupApi.FiscalPeriodBranchDetailAccounts, this.FiscalPeriodId, this.BranchId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    FullAccountService.prototype.GetCostCentersLookup = function () {
        var url = source_1.String.Format(index_1.LookupApi.FiscalPeriodBranchCostCenters, this.FiscalPeriodId, this.BranchId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    FullAccountService.prototype.GetProjectsLookup = function () {
        var url = source_1.String.Format(index_1.LookupApi.FiscalPeriodBranchProjects, this.FiscalPeriodId, this.BranchId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    FullAccountService.prototype.getFullAccountItemList = function (apiUrl, filter) {
        var intMaxValue = 2147483647;
        var gridPaging = { pageIndex: 1, pageSize: intMaxValue };
        var postItem = { Paging: gridPaging, filter: filter, sortColumns: null };
        var searchHeaders = this.httpHeaders;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders = searchHeaders.append('X-Tadbir-GridOptions', base64Body);
        var options = { headers: searchHeaders };
        return this.http.get(apiUrl, options)
            .map(function (response) { return response; });
    };
    FullAccountService = __decorate([
        core_1.Injectable()
    ], FullAccountService);
    return FullAccountService;
}(base_service_1.BaseService));
exports.FullAccountService = FullAccountService;
//# sourceMappingURL=fullAccount.service.js.map