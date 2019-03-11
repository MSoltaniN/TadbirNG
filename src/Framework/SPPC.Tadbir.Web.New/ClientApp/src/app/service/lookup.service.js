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
var base_service_1 = require("../class/base.service");
var index_1 = require("./api/index");
var LookupService = /** @class */ (function (_super) {
    __extends(LookupService, _super);
    function LookupService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    LookupService.prototype.GetLookup = function (url) {
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    LookupService.prototype.GetAccountsLookup = function () {
        var url = index_1.AccountRelationApi.EnvironmentAccountsLookup;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    LookupService.prototype.GetDetailAccountsLookup = function () {
        var url = index_1.AccountRelationApi.EnvironmentDetailAccountsLookup;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    LookupService.prototype.GetCostCentersLookup = function () {
        var url = index_1.AccountRelationApi.EnvironmentCostCentersLookup;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    LookupService.prototype.GetProjectsLookup = function () {
        var url = index_1.AccountRelationApi.EnvironmentProjectsLookup;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    LookupService.prototype.GetCurrenciesLookup = function () {
        var options = { headers: this.httpHeaders };
        return this.http.get(index_1.LookupApi.Currencies, options)
            .map(function (response) { return response; });
    };
    LookupService.prototype.GetAccountGroupsLookup = function () {
        var url = index_1.LookupApi.AccountGroups;
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    LookupService = __decorate([
        core_1.Injectable()
    ], LookupService);
    return LookupService;
}(base_service_1.BaseService));
exports.LookupService = LookupService;
//# sourceMappingURL=lookup.service.js.map