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
var report_base_service_1 = require("../../class/report.base.service");
var ReportingService = /** @class */ (function (_super) {
    __extends(ReportingService, _super);
    function ReportingService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        return _this;
    }
    ReportingService.prototype.saveReport = function (apiUrl, report) {
        var body = JSON.stringify(report);
        return this.http.put(apiUrl, body, this.option)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    ReportingService.prototype.saveAsReport = function (apiUrl, report) {
        var body = JSON.stringify(report);
        return this.http.post(apiUrl, body, this.option)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    ReportingService.prototype.setDefaultForAll = function (apiUrl) {
        return this.http.put(apiUrl, null, this.option)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    ReportingService.prototype.deleteReport = function (apiUrl) {
        return this.http.delete(apiUrl, this.option)
            .map(function (res) { return res; })
            .catch(this.handleError);
    };
    ReportingService = __decorate([
        core_1.Injectable()
    ], ReportingService);
    return ReportingService;
}(report_base_service_1.ReportBaseService));
exports.ReportingService = ReportingService;
var ParameterInfo = /** @class */ (function () {
    function ParameterInfo() {
    }
    return ParameterInfo;
}());
exports.ParameterInfo = ParameterInfo;
var LocalReportInfo = /** @class */ (function () {
    function LocalReportInfo() {
    }
    return LocalReportInfo;
}());
exports.LocalReportInfo = LocalReportInfo;
//# sourceMappingURL=reporting.service.js.map