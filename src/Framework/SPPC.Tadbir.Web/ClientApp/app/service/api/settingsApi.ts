// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.334
//     Template Version: 1.0
//     Generation Date: 7/1/2018 11:15:11 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { Environment } from "../../enviroment";

export class SettingsApi {
    
    // settings
    public static AllSettings = Environment.BaseUrl + "/settings";

    // settings/list/users/{userId:min(1)}
    public static ListSettingsByUser = Environment.BaseUrl + "/settings/list/users/{0}";

    // settings/list/users/{userId:min(1)}/views/{viewId:min(1)}
    public static ListSettingsByUserAndView = Environment.BaseUrl + "/settings/list/users/{0}/views/{1}";

    // settings/workflows
    public static WorkflowSettings = Environment.BaseUrl + "/settings/workflows";

    
}