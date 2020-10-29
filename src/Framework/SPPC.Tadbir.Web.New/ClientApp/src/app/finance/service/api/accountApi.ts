// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.1013
//     Template Version: 1.0
//     Generation Date: 2020-10-29 11:02:55 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class AccountApi {

    // accounts
    public static EnvironmentAccounts = environment.BaseUrl + "/accounts";

    // accounts/ledger
    public static LedgerAccounts = environment.BaseUrl + "/accounts/ledger";

    // accounts/ledger/{groupId:min(1)}
    public static LedgerAccountsByGroupId = environment.BaseUrl + "/accounts/ledger/{0}";

    // accounts/{accountId:min(1)}
    public static Account = environment.BaseUrl + "/accounts/{0}";

    // accounts/{accountId:min(1)}/children
    public static AccountChildren = environment.BaseUrl + "/accounts/{0}/children";

    // accounts/{accountId:int}/children/new
    public static NewChildAccount = environment.BaseUrl + "/accounts/{0}/children/new";

    // accounts/{accountId:int}/fullcode
    public static AccountFullCode = environment.BaseUrl + "/accounts/{0}/fullcode";

    // accounts/count
    public static AccountsCount = environment.BaseUrl + "/accounts/count";

    // accounts/{accountId:min(1)}/fulldata
    public static AccountFullData = environment.BaseUrl + "/accounts/{0}/fulldata";
}