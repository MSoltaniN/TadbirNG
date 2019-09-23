// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.719
//     Template Version: 1.0
//     Generation Date: 07/01/1398 04:34:33 ب.ظ
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "../../../environments/environment";

export class TestBalanceApi {

    // testbal/lookup/types
    public static TestBalanceTypeLookup = environment.BaseUrl + "/testbal/lookup/types";

    // testbal/levels/{level:min(1)}/2-col
    public static TwoColumnLevelBalance = environment.BaseUrl + "/testbal/levels/{0}/2-col";

    // testbal/levels/{level:min(1)}/4-col
    public static FourColumnLevelBalance = environment.BaseUrl + "/testbal/levels/{0}/4-col";

    // testbal/levels/{level:min(1)}/6-col
    public static SixColumnLevelBalance = environment.BaseUrl + "/testbal/levels/{0}/6-col";

    // testbal/levels/{level:min(1)}/8-col
    public static EightColumnLevelBalance = environment.BaseUrl + "/testbal/levels/{0}/8-col";

    // testbal/levels/{level:min(1)}/10-col
    public static TenColumnLevelBalance = environment.BaseUrl + "/testbal/levels/{0}/10-col";

    // testbal/{accountId:min(1)}/items/2-col
    public static TwoColumnChildItemsBalance = environment.BaseUrl + "/testbal/{0}/items/2-col";

    // testbal/{accountId:min(1)}/items/4-col
    public static FourColumnChildItemsBalance = environment.BaseUrl + "/testbal/{0}/items/4-col";

    // testbal/{accountId:min(1)}/items/6-col
    public static SixColumnChildItemsBalance = environment.BaseUrl + "/testbal/{0}/items/6-col";

    // testbal/{accountId:min(1)}/items/8-col
    public static EightColumnChildItemsBalance = environment.BaseUrl + "/testbal/{0}/items/8-col";

    // testbal/{accountId:min(1)}/items/10-col
    public static TenColumnChildItemsBalance = environment.BaseUrl + "/testbal/{0}/items/10-col";
}