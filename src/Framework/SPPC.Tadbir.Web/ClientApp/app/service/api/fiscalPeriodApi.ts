// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.294
//     Template Version: 1.0
//     Generation Date: 05/20/2018 02:32:38 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { Environment } from "../../enviroment";

export class FiscalPeriodApi {

    // fperiods/company/{companyId:min(1)}
    public static CompanyFiscalPeriods = Environment.BaseUrl + "/fperiods/company/{0}";

    // fperiods
    public static FiscalPeriods = Environment.BaseUrl + "/fperiods";

    // fperiods/{fpId:min(1)}
    public static FiscalPeriod = Environment.BaseUrl + "/fperiods/{0}";

    // fperiods/metadata
    public static FiscalPeriodMetadata = Environment.BaseUrl + "/fperiods/metadata";
}