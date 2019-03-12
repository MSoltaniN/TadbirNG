"use strict";
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.510
//     Template Version: 1.0
//     Generation Date: 2019-02-04 7:39:54 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../../environments/environment");
var ReportApi = /** @class */ (function () {
    function ReportApi() {
    }
    // reports/sys/tree
    ReportApi.ReportsHierarchy = environment_1.environment.BaseUrl + "/reports/sys/tree";
    // reports/sys
    ReportApi.Reports = environment_1.environment.BaseUrl + "/reports/sys";
    // reports/sys/view/{viewId:min(1)}
    ReportApi.ReportsByView = environment_1.environment.BaseUrl + "/reports/sys/view/{0}";
    // reports/sys/view/{viewId:min(1)}/default
    ReportApi.ReportsByViewDefault = environment_1.environment.BaseUrl + "/reports/sys/view/{0}/default";
    // reports/sys/subsys/{subsysId:min(1)}
    ReportApi.ReportsBySubsystem = environment_1.environment.BaseUrl + "/reports/sys/subsys/{0}";
    // reports/sys/{reportId:min(1)}
    ReportApi.Report = environment_1.environment.BaseUrl + "/reports/sys/{0}";
    // reports/sys/{reportId:min(1)}/default
    ReportApi.ReportDefault = environment_1.environment.BaseUrl + "/reports/sys/{0}/default";
    // reports/sys/{reportId:min(1)}/design
    ReportApi.ReportDesign = environment_1.environment.BaseUrl + "/reports/sys/{0}/design";
    // reports/sys/{reportId:min(1)}/caption
    ReportApi.ReportCaption = environment_1.environment.BaseUrl + "/reports/sys/{0}/caption";
    // reports/voucher/sum-by-date
    ReportApi.EnvironmentVoucherSummaryByDate = environment_1.environment.BaseUrl + "/reports/voucher/sum-by-date";
    // reports/voucher/std-form
    ReportApi.VoucherStandardForm = environment_1.environment.BaseUrl + "/reports/voucher/std-form";
    // reports/voucher/std-form-detail
    ReportApi.VoucherStandardFormWithDetail = environment_1.environment.BaseUrl + "/reports/voucher/std-form-detail";
    return ReportApi;
}());
exports.ReportApi = ReportApi;
//# sourceMappingURL=reportApi.js.map