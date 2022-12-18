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

import { environment } from "@sppc/env/environment";

export class DashboardApi {
  // dashboard/current
  public static CurrentDashboard = environment.BaseUrl + "/dashboard/current";

  // dashboard/new
  public static NewDashboard = environment.BaseUrl + "/dashboard/new";

  // dashboard/tabs
  public static DashboardTabs = environment.BaseUrl + "/dashboard/tabs";

  // dashboard/tabs/{tabId:min(1)}
  public static DashboardTab = environment.BaseUrl + "/dashboard/tabs/{0}";

  // dashboard/tabs/widgets
  public static AllTabWidgets = environment.BaseUrl + "/dashboard/tabs/widgets";

  // dashboard/tabs/{tabId:min(1)}/widgets
  public static TabWidgets = environment.BaseUrl + "/dashboard/tabs/{0}/widgets";

  // dashboard/tabs/{tabId:min(1)}/widgets/{widgetId:min(1)}
  public static TabWidget = environment.BaseUrl + "/dashboard/tabs/{0}/widgets/{1}";

  // dashboard/widgets
  public static Widgets = environment.BaseUrl + "/dashboard/widgets";

  // dashboard/widgets/all
  public static AllWidgets = environment.BaseUrl + "/dashboard/widgets/all";

  // dashboard/widgets/{widgetId:min(1)}
  public static Widget = environment.BaseUrl + "/dashboard/widgets/{0}";

  // dashboard/widgets/{widgetId:min(1)}/usage
  public static WidgetUsage = environment.BaseUrl + "/dashboard/widgets/{0}/usage";

  // dashboard/widgets/{widgetId:min(1)}/data
  public static WidgetData = environment.BaseUrl + "/dashboard/widgets/{0}/data";

  // dashboard/functions/{functionId:min(1)}/params
  public static FunctionParameters = environment.BaseUrl + "/dashboard/functions/{0}/params";

  // dashboard/lookup/functions
  public static WidgetFunctionsLookup =
    environment.BaseUrl + "/dashboard/lookup/functions";

  // dashboard/lookup/wtypes
  public static WidgetTypesLookup =
    environment.BaseUrl + "/dashboard/lookup/wtypes";

  // dashboard/lookup/widgets
  public static WidgetsLookup =
    environment.BaseUrl + "/dashboard/lookup/widgets";

  // dashboard/license
  public static LicenseInfo = environment.BaseUrl + "/dashboard/license";

  //
  public static WidgetRoles = environment.BaseUrl + "/dashboard/widgets/{0}/roles";
}
