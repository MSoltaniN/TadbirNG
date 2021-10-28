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

import { environment } from "@sppc/env/environment";

export class ReportApi {

    // reports/tree
    public static ReportsHierarchy = environment.BaseUrl + "/reports/tree";

    // reports
    public static Reports = environment.BaseUrl + "/reports";

    // reports/view/{viewId:min(1)}
    public static ReportsByView = environment.BaseUrl + "/reports/view/{0}";

    // reports/view/{viewId:min(1)}/default
    public static ReportsByViewDefault = environment.BaseUrl + "/reports/view/{0}/default";

    // reports/view/{viewId:min(1)}/quickreport
    public static ReportsByViewQuickReportUrl = environment.BaseUrl + "/reports/view/{0}/quickreport";   

    // reports/subsys/{subsysId:min(1)}
    public static ReportsBySubsystem = environment.BaseUrl + "/reports/subsys/{0}";

    // reports/{reportId:min(1)}
    public static Report = environment.BaseUrl + "/reports/{0}";

    // reports/{reportId:min(1)}/default
    public static ReportDefault = environment.BaseUrl + "/reports/{0}/default";

    // reports/{reportId:min(1)}/design
    public static ReportDesign = environment.BaseUrl + "/reports/{0}/design";

    // reports/{reportId:min(1)}/caption
    public static ReportCaption = environment.BaseUrl + "/reports/{0}/caption";

    // reports/metadata/{viewId:min(1)}
    public static ReportMetadataByView = environment.BaseUrl + "/reports/metadata/{0}";

    // reports/quickreport
    public static EnvironmentQuickReport = environment.BaseUrl + "/reports/quickreport/{0}";
}
