// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.627
//     Template Version: 1.0
//     Generation Date: 6/16/2019 3:16:57 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
import { environment } from "../../../environments/environment";
export class SettingsApi {
}
// settings
SettingsApi.AllSettings = environment.BaseUrl + "/settings";
// settings/{settingId:min(1)}
SettingsApi.Setting = environment.BaseUrl + "/settings/{0}";
// settings/list/users/{userId:min(1)}
SettingsApi.ListSettingsByUser = environment.BaseUrl + "/settings/list/users/{0}";
// settings/list/users/{userId:min(1)}/views/{viewId:min(1)}
SettingsApi.ListSettingsByUserAndView = environment.BaseUrl + "/settings/list/users/{0}/views/{1}";
// settings/qsearch/users/{userId:min(1)}
SettingsApi.QuickSearchSettingsByUser = environment.BaseUrl + "/settings/qsearch/users/{0}";
// settings/qsearch/users/{userId:min(1)}/views/{viewId:min(1)}
SettingsApi.QuickSearchSettingsByUserAndView = environment.BaseUrl + "/settings/qsearch/users/{0}/views/{1}";
// settings/workflows
SettingsApi.WorkflowSettings = environment.BaseUrl + "/settings/workflows";
// settings/views/tree
SettingsApi.ViewTreeSettings = environment.BaseUrl + "/settings/views/tree";
// settings/views/{viewId:min(1)}/tree
SettingsApi.ViewTreeSettingsByView = environment.BaseUrl + "/settings/views/{0}/tree";
//# sourceMappingURL=settingsApi.js.map