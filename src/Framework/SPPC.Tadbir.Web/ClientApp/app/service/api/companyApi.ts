// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.385
//     Template Version: 1.0
//     Generation Date: 08/29/2018 10:13:17 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { Environment } from "../../enviroment";

export class CompanyApi {

    // companies/company/{companyId:min(1)}
    public static CompanyChildren = Environment.BaseUrl + "/companies/company/{0}";

    // companies
    public static Companies = Environment.BaseUrl + "/companies";

    // companies/{companyId:min(1)}
    public static Company = Environment.BaseUrl + "/companies/{0}";

    // companies/metadata
    public static CompanyMetadata = Environment.BaseUrl + "/companies/metadata";
}