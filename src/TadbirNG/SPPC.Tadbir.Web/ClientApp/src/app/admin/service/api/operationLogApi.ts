// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.832
//     Template Version: 1.0
//     Generation Date: 2020-03-04 5:08:38 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class OperationLogApi {

    // system/oplog
    public static OperationLogs = environment.BaseUrl + "/system/oplog";

    // system/sys-oplog
    public static SysOperationLogs = environment.BaseUrl + "/system/sys-oplog";

    // system/oplog/all
    public static AllOperationLogs = environment.BaseUrl + "/system/oplog/all";

    // system/sys-oplog/all
    public static AllSysOperationLogs = environment.BaseUrl + "/system/sys-oplog/all";

    // system/oplog/archive
    public static OperationLogsArchive = environment.BaseUrl + "/system/oplog/archive";

    // system/sys-oplog/archive
    public static SysOperationLogsArchive = environment.BaseUrl + "/system/sys-oplog/archive";

    // system/oplog/metadata
    public static OperationLogMetadata = environment.BaseUrl + "/system/oplog/metadata";
}