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
import { environment } from "../../../environments/environment";
export class SystemApi {
}
// system/oplog
SystemApi.AllOperationLogs = environment.BaseUrl + "/system/oplog";
// system/oplog/metadata
SystemApi.OperationLogMetadata = environment.BaseUrl + "/system/oplog/metadata";
// system/oplog/company/{companyId:int}
SystemApi.CompanyOperationLogs = environment.BaseUrl + "/system/oplog/company/{0}";
// system/oplog/user/{userId:int}
SystemApi.UserOperationLogs = environment.BaseUrl + "/system/oplog/user/{0}";
// system/oplog/user/{userId:int}/company/{companyId:int}
SystemApi.UserCompanyOperationLogs = environment.BaseUrl + "/system/oplog/user/{0}/company/{1}";
//# sourceMappingURL=SystemApi.js.map