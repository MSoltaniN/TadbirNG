// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.569
//     Template Version: 1.0
//     Generation Date: 2019-05-07 11:24:14 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
import { environment } from "../../../environments/environment";
export class ReportApi {
}
// reports/sys/tree
ReportApi.ReportsHierarchy = environment.BaseUrl + "/reports/sys/tree";
// reports/sys
ReportApi.Reports = environment.BaseUrl + "/reports/sys";
// reports/sys/view/{viewId:min(1)}
ReportApi.ReportsByView = environment.BaseUrl + "/reports/sys/view/{0}";
// reports/sys/view/{viewId:min(1)}/default
ReportApi.ReportsByViewDefault = environment.BaseUrl + "/reports/sys/view/{0}/default";
// reports/sys/subsys/{subsysId:min(1)}
ReportApi.ReportsBySubsystem = environment.BaseUrl + "/reports/sys/subsys/{0}";
// reports/sys/{reportId:min(1)}
ReportApi.Report = environment.BaseUrl + "/reports/sys/{0}";
// reports/sys/{reportId:min(1)}/default
ReportApi.ReportDefault = environment.BaseUrl + "/reports/sys/{0}/default";
// reports/sys/{reportId:min(1)}/design
ReportApi.ReportDesign = environment.BaseUrl + "/reports/sys/{0}/design";
// reports/sys/{reportId:min(1)}/caption
ReportApi.ReportCaption = environment.BaseUrl + "/reports/sys/{0}/caption";
// reports/metadata/{viewId:min(1)}
ReportApi.ReportMetadataByView = environment.BaseUrl + "/reports/metadata/{0}";
// reports/sys/quickreport
ReportApi.EnvironmentQuickReport = environment.BaseUrl + "/reports/sys/quickreport/{0}";
// reports/voucher/sum-by-date
ReportApi.EnvironmentVoucherSummaryByDate = environment.BaseUrl + "/reports/voucher/sum-by-date";
// reports/voucher/std-form
ReportApi.VoucherStandardForm = environment.BaseUrl + "/reports/voucher/std-form";
// reports/voucher/std-form-detail
ReportApi.VoucherStandardFormWithDetail = environment.BaseUrl + "/reports/voucher/std-form-detail";
//# sourceMappingURL=reportApi.js.map