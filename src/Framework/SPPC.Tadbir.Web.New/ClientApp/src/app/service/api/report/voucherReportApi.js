"use strict";
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.429
//     Template Version: 1.0
//     Generation Date: 10/20/2018 12:16:12 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../../../environments/environment");
var VoucherReportApi = /** @class */ (function () {
    function VoucherReportApi() {
    }
    //reports/voucher/sum-by-date
    VoucherReportApi.VoucherSumReport = environment_1.environment.BaseUrl + "/reports/voucher/sum-by-date";
    //reports/voucher/std-form/{voucherId:min(1)}
    VoucherReportApi.VoucherStdFormReport = environment_1.environment.BaseUrl + "/reports/voucher/std-form/{0}";
    return VoucherReportApi;
}());
exports.VoucherReportApi = VoucherReportApi;
//# sourceMappingURL=voucherReportApi.js.map