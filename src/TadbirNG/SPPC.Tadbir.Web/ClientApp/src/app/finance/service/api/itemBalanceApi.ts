// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.771
//     Template Version: 1.0
//     Generation Date: 2019-12-10 11:01:37 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class ItemBalanceApi {

    // itembal/views/{viewId:min(2)}/lookup/types
    public static ItemBalanceTypeLookup = environment.BaseUrl + "/itembal/views/{0}/lookup/types";

    // itembal/views/{viewId:min(2)}/levels/{level:min(1)}/2-col
    public static TwoColumnLevelBalance = environment.BaseUrl + "/itembal/views/{0}/levels/{1}/2-col";

    // itembal/views/{viewId:min(2)}/levels/{level:min(1)}/4-col
    public static FourColumnLevelBalance = environment.BaseUrl + "/itembal/views/{0}/levels/{1}/4-col";

    // itembal/views/{viewId:min(2)}/levels/{level:min(1)}/6-col
    public static SixColumnLevelBalance = environment.BaseUrl + "/itembal/views/{0}/levels/{1}/6-col";

    // itembal/views/{viewId:min(2)}/levels/{level:min(1)}/8-col
    public static EightColumnLevelBalance = environment.BaseUrl + "/itembal/views/{0}/levels/{1}/8-col";

    // itembal/views/{viewId:min(2)}/levels/{level:min(1)}/10-col
    public static TenColumnLevelBalance = environment.BaseUrl + "/itembal/views/{0}/levels/{1}/10-col";

    // itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/2-col
    public static TwoColumnChildItemsBalance = environment.BaseUrl + "/itembal/views/{0}/items/{1}/2-col";

    // itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/4-col
    public static FourColumnChildItemsBalance = environment.BaseUrl + "/itembal/views/{0}/items/{1}/4-col";

    // itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/6-col
    public static SixColumnChildItemsBalance = environment.BaseUrl + "/itembal/views/{0}/items/{1}/6-col";

    // itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/8-col
    public static EightColumnChildItemsBalance = environment.BaseUrl + "/itembal/views/{0}/items/{1}/8-col";

    // itembal/views/{viewId:min(2)}/items/{itemId:min(1)}/10-col
    public static TenColumnChildItemsBalance = environment.BaseUrl + "/itembal/views/{0}/items/{1}/10-col";
}
