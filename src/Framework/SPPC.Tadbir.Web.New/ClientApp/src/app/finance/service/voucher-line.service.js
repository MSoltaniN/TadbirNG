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
var index_1 = require("./api/index");
var source_1 = require("../class/source");
var base_service_1 = require("../class/base.service");
var VoucherLineInfo = /** @class */ (function () {
    function VoucherLineInfo() {
        this.id = 0;
        this.fiscalPeriodId = 0;
        this.branchId = 0;
        this.voucherId = 0;
    }
    return VoucherLineInfo;
}());
exports.VoucherLineInfo = VoucherLineInfo;
var VoucherLineService = /** @class */ (function (_super) {
    __extends(VoucherLineService, _super);
    function VoucherLineService(http) {
        var _this = _super.call(this, http) || this;
        _this.http = http;
        _this.getAccountArticlesUrl = "http://37.59.93.7:8080/accounts/{0}/articles";
        return _this;
    }
    VoucherLineService.prototype.getAccountArticles = function (accountId) {
        var url = source_1.String.Format(this.getAccountArticlesUrl, accountId.toString());
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
    };
    VoucherLineService.prototype.getVoucherInfo = function (voucherId) {
        var url = source_1.String.Format(index_1.VoucherApi.Voucher, voucherId);
        var options = { headers: this.httpHeaders };
        return this.http.get(url, options)
            .map(function (response) { return response; });
        ;
    };
    VoucherLineService = __decorate([
        core_1.Injectable()
    ], VoucherLineService);
    return VoucherLineService;
}(base_service_1.BaseService));
exports.VoucherLineService = VoucherLineService;
//# sourceMappingURL=voucher-line.service.js.map