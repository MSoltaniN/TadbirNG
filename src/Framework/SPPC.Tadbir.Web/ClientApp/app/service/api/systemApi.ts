// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.398
//     Template Version: 1.0
//     Generation Date: 09/22/2018 09:53:32 ق.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { Environment } from "../../enviroment";

export class SystemApi {

    // system/oplog
    public static AllOperationLogs = Environment.BaseUrl + "/system/oplog";

    // system/oplog/metadata
    public static OperationLogMetadata = Environment.BaseUrl + "/system/oplog/metadata";

    // system/oplog/company/{companyId:int}
    public static CompanyOperationLogs = Environment.BaseUrl + "/system/oplog/company/{0}";

    // system/oplog/user/{userId:int}
    public static UserOperationLogs = Environment.BaseUrl + "/system/oplog/user/{0}";

    // system/oplog/user/{userId:int}/company/{companyId:int}
    public static UserCompanyOperationLogs = Environment.BaseUrl + "/system/oplog/user/{0}/company/{1}";
}