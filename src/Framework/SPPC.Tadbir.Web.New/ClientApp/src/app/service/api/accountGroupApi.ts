// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.558
//     Template Version: 1.0
//     Generation Date: 4/30/2019 12:55:54 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class AccountGroupApi {

    // accgroups
    public static AccountGroups = environment.BaseUrl + "/accgroups";

    // accgroups/{groupId:min(1)}
    public static AccountGroup = environment.BaseUrl + "/accgroups/{0}";

    // accgroups/metadata
    public static AccountGroupMetadata = environment.BaseUrl + "/accgroups/metadata";

    // accgroups/{groupId:min(1)}/accounts
    public static GroupLedgerAccounts = environment.BaseUrl + "/accgroups/{0}/accounts";

    // accgroups/brief
    public static AccountGroupBrief = environment.BaseUrl + "/accgroups/brief";
}