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
var base_service_1 = require("../class/base.service");
var source_1 = require("../class/source");
var index_1 = require("./api/index");
var FiscalPeriodInfo = /** @class */ (function () {
    function FiscalPeriodInfo() {
        this.id = 0;
        this.startDate = new Date();
        this.endDate = new Date();
    }
    return FiscalPeriodInfo;
}());
exports.FiscalPeriodInfo = FiscalPeriodInfo;
var FiscalPeriodService = /** @class */ (function (_super) {
    __extends(FiscalPeriodService, _super);
    function FiscalPeriodService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    FiscalPeriodService.prototype.getFiscalPeriodRoles = function (fPeriodId) {
        var url = source_1.String.Format(index_1.FiscalPeriodApi.FiscalPeriodRoles, fPeriodId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    FiscalPeriodService.prototype.modifiedFiscalPeriodRoles = function (fPeriodIdRoles) {
        var body = JSON.stringify(fPeriodIdRoles);
        var options = { headers: this.httpHeaders };
        var url = source_1.String.Format(index_1.FiscalPeriodApi.FiscalPeriodRoles, fPeriodIdRoles.id);
        return this.http.put(url, body, options)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    FiscalPeriodService = __decorate([
        core_1.Injectable()
    ], FiscalPeriodService);
    return FiscalPeriodService;
}(base_service_1.BaseService));
exports.FiscalPeriodService = FiscalPeriodService;
//# sourceMappingURL=fiscal-period.service.js.map