// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.412
//     Template Version: 1.0
//     Generation Date: 10/01/2018 03:08:40 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class AccountApi {

    // accounts
    public static EnvironmentAccounts = environment.BaseUrl + "/accounts";

    // accounts/lookup
    public static EnvironmentAccountsLookup = environment.BaseUrl + "/accounts/lookup";

    // accounts/{accountId:min(1)}
    public static Account = environment.BaseUrl + "/accounts/{0}";

    // accounts/{accountId:min(1)}/children
    public static AccountChildren = environment.BaseUrl + "/accounts/{0}/children";

    // accounts/metadata
    public static AccountMetadata = environment.BaseUrl + "/accounts/metadata";

    // accounts/fullcode/{parentId}
    public static AccountFullCode = environment.BaseUrl + "/accounts/fullcode/{0}";
}
