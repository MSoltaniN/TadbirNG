// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.323
//     Template Version: 1.0
//     Generation Date: 06/12/2018 10:51:49 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class FiscalPeriodApi {

    // fperiods/company/{companyId:min(1)}
    public static CompanyFiscalPeriods = environment.BaseUrl + "/fperiods/company/{0}";

    // fperiods
    public static FiscalPeriods = environment.BaseUrl + "/fperiods";

    // fperiods/{fpId:min(1)}
    public static FiscalPeriod = environment.BaseUrl + "/fperiods/{0}";

    // fperiods/metadata
    public static FiscalPeriodMetadata = environment.BaseUrl + "/fperiods/metadata";

    // fperiods/{fpId:min(1)}/roles
    public static FiscalPeriodRoles = environment.BaseUrl + "/fperiods/{0}/roles";

    // fperiods/validation
    public static FiscalPeriodValidation = environment.BaseUrl + "/fperiods/validation";
}
