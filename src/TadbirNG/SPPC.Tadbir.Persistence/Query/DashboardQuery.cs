﻿using System;

namespace SPPC.Tadbir.Persistence
{
    internal static class DashboardQuery
    {
        internal const string CollectionBalance = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
WHERE {0}";

        internal const string CollectionBalanceByDate = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
WHERE v.Date >= '{0}' AND v.Date <= '{1}' AND {2}";

        internal const string CurrentDashboardWidgets = @"
SELECT [wgt].[WidgetID], [tab].[DashboardTabID] AS [TabID], [dbd].[DashboardID], [tab].[Index], [tab].[Title] AS [TabTitle],
  [twgt].[Settings], [wgt].[Title], [wgt].[Description], [wgt].[DefaultSettings]
FROM [Reporting].[Dashboard] AS [dbd]
  INNER JOIN [Reporting].[DashboardTab] AS [tab] ON [dbd].[DashboardID] = [tab].[DashboardID]
  INNER JOIN [Reporting].[TabWidget] AS [twgt] ON [tab].[DashboardTabID] = [twgt].[TabID]
  INNER JOIN [Reporting].[Widget] AS [wgt] ON [twgt].[WidgetID] = [wgt].[WidgetID]
WHERE [dbd].[UserID] = {0}";

        internal const string WidgetDetails = @"
SELECT [wgt].[WidgetID], [wgt].[FunctionID], [func].[Name] AS [FunctionName], [wgt].[TypeID], [type].[Name] AS [TypeName]
FROM [Reporting].[Widget] AS [wgt]
  INNER JOIN [Reporting].[WidgetFunction] AS [func] ON [wgt].[FunctionID] = [func].[WidgetFunctionID]
  INNER JOIN [Reporting].[WidgetType] AS [type] ON [wgt].[TypeID] = [type].[WidgetTypeID]
WHERE [wgt].[WidgetID] IN({0})";

        internal const string WidgetsAccounts = @"
SELECT [wacc].[WidgetID], [wacc].[AccountID], [acc].[Name] AS [AccountName], [acc].[FullCode] AS [AccountFullCode],
  [wacc].[DetailAccountID], [facc].[Name] AS [DetailAccountName], [facc].[FullCode] AS [DetailAccountFullCode],
  [wacc].[CostCenterID], [cc].[Name] AS [CostCenterName], [cc].[FullCode] AS [CostCenterFullCode],
  [wacc].[ProjectID], [prj].[Name] AS [ProjectName], [prj].[FullCode] AS [ProjectFullCode]
FROM [Reporting].[WidgetAccount] AS [wacc]
  LEFT OUTER JOIN [Finance].[Account] AS [acc] ON [wacc].[AccountID] = [acc].[AccountID]
  LEFT OUTER JOIN [Finance].[DetailAccount] AS [facc] ON [wacc].[DetailAccountID] = [facc].[DetailAccountID]
  LEFT OUTER JOIN [Finance].[CostCenter] AS [cc] ON [wacc].[CostCenterID] = [cc].[CostCenterID]
  LEFT OUTER JOIN [Finance].[Project] AS [prj] ON [wacc].[ProjectID] = [prj].[ProjectID]
WHERE [wacc].[WidgetID] IN ({0})";

        internal const string WidgetsParameters = @"
SELECT [uwp].[WidgetID], [wp].[Name], [wp].[Alias], [wp].[Type], [wp].[DefaultValue], [wp].[Description]
FROM [Reporting].[UsedWidgetParameter] AS [uwp]
  INNER JOIN [Reporting].[WidgetParameter] AS [wp] ON [uwp].[ParameterID] = [wp].[WidgetParameterID]
WHERE [uwp].[WidgetID] IN ({0})";

        internal const string DebitTurnover = @"
SELECT {0}, SUM([vl].[Debit]) AS [Debit]
FROM [Finance].[Voucher] AS [v]
  INNER JOIN [Finance].[VoucherLine] AS [vl] ON [v].[VoucherID] = [vl].[VoucherID]
  INNER JOIN [Finance].[Account] AS [acc] ON [vl].[AccountID] = [acc].[AccountID]
  LEFT OUTER JOIN [Finance].[DetailAccount] AS [facc] ON [vl].[DetailID] = [facc].[DetailAccountID]
  LEFT OUTER JOIN [Finance].[CostCenter] AS [cc] ON [vl].[CostCenterID] = [cc].[CostCenterID]
  LEFT OUTER JOIN [Finance].[Project] AS [prj] ON [vl].[ProjectID] = [prj].[ProjectID]
WHERE [v].[Date] >= '{1}' AND [v].[Date] <= '{2}' AND [v].[FiscalPeriodID] = {3} AND [v].[SubjectType] = 0
GROUP BY {4}";

        internal const string CreditTurnover = @"
SELECT {0}, SUM([vl].[Credit]) AS [Credit]
FROM [Finance].[Voucher] AS [v]
  INNER JOIN [Finance].[VoucherLine] AS [vl] ON [v].[VoucherID] = [vl].[VoucherID]
  INNER JOIN [Finance].[Account] AS [acc] ON [vl].[AccountID] = [acc].[AccountID]
  LEFT OUTER JOIN [Finance].[DetailAccount] AS [facc] ON [vl].[DetailID] = [facc].[DetailAccountID]
  LEFT OUTER JOIN [Finance].[CostCenter] AS [cc] ON [vl].[CostCenterID] = [cc].[CostCenterID]
  LEFT OUTER JOIN [Finance].[Project] AS [prj] ON [vl].[ProjectID] = [prj].[ProjectID]
WHERE [v].[Date] >= '{1}' AND [v].[Date] <= '{2}' AND [v].[FiscalPeriodID] = {3} AND [v].[SubjectType] = 0
GROUP BY {4}";

        internal const string NetTurnover = @"
SELECT {0}, ABS(SUM([vl].[Debit]) - SUM([vl].[Credit])) AS [Net]
FROM [Finance].[Voucher] AS [v]
  INNER JOIN [Finance].[VoucherLine] AS [vl] ON [v].[VoucherID] = [vl].[VoucherID]
  INNER JOIN [Finance].[Account] AS [acc] ON [vl].[AccountID] = [acc].[AccountID]
  LEFT OUTER JOIN [Finance].[DetailAccount] AS [facc] ON [vl].[DetailID] = [facc].[DetailAccountID]
  LEFT OUTER JOIN [Finance].[CostCenter] AS [cc] ON [vl].[CostCenterID] = [cc].[CostCenterID]
  LEFT OUTER JOIN [Finance].[Project] AS [prj] ON [vl].[ProjectID] = [prj].[ProjectID]
WHERE [v].[Date] >= '{1}' AND [v].[Date] <= '{2}' AND [v].[FiscalPeriodID] = {3} AND [v].[SubjectType] = 0
GROUP BY {4}";

        internal const string Balance = @"
SELECT {0}, SUM([vl].[Debit] - [vl].[Credit]) AS [Balance]
FROM [Finance].[Voucher] AS [v]
  INNER JOIN [Finance].[VoucherLine] AS [vl] ON [v].[VoucherID] = [vl].[VoucherID]
  INNER JOIN [Finance].[Account] AS [acc] ON [vl].[AccountID] = [acc].[AccountID]
  LEFT OUTER JOIN [Finance].[DetailAccount] AS [facc] ON [vl].[DetailID] = [facc].[DetailAccountID]
  LEFT OUTER JOIN [Finance].[CostCenter] AS [cc] ON [vl].[CostCenterID] = [cc].[CostCenterID]
  LEFT OUTER JOIN [Finance].[Project] AS [prj] ON [vl].[ProjectID] = [prj].[ProjectID]
WHERE [v].[Date] <= '{1}' AND [v].[FiscalPeriodID] = {2} AND [v].[SubjectType] = 0
GROUP BY {3}";
    }
}
