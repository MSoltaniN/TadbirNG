// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.412
//     Template Version: 1.0
//     Generation Date: 10/01/2018 03:43:44 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class DetailAccountApi {

    // faccounts
    public static EnvironmentDetailAccounts = environment.BaseUrl + "/faccounts";

    // faccounts/lookup
    public static EnvironmentDetailAccountsLookup = environment.BaseUrl + "/faccounts/lookup";

    // faccounts/{faccountId:min(1)}
    public static DetailAccount = environment.BaseUrl + "/faccounts/{0}";

    // faccounts/{faccountId:min(1)}/children
    public static DetailAccountChildren = environment.BaseUrl + "/faccounts/{0}/children";

    // faccounts/metadata
    public static DetailAccountMetadata = environment.BaseUrl + "/faccounts/metadata";

    // faccounts/fullcode/{parentId}
    public static DetailAccountFullCode = environment.BaseUrl + "/faccounts/fullcode/{0}";
}