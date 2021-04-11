﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class VoucherQuery
    {
        internal const string EnvironmentVouchers = @"
SELECT v.VoucherID, v.No, v.Date, v.Description, st.Name AS StatusName, v.Reference, v.Association, v.DailyNo, v.IsBalanced,
    v.ConfirmerName, v.ApproverName, v.ConfirmedByID, v.ApprovedByID, br.Name AS BranchName, v.IssuerName, vo.Name AS OriginName,
    v.SubjectType
FROM [Finance].[Voucher] v
    INNER JOIN [Corporate].[Branch] br ON v.BranchID = br.BranchID
    INNER JOIN [Core].[DocumentStatus] st ON v.StatusID = st.StatusID
    INNER JOIN [Finance].[VoucherOrigin] vo ON v.OriginID = vo.OriginID
WHERE {0}
ORDER BY v.Date, v.No";

        internal const string BalanceByItemsByCurrency = @"
SELECT vl.DetailID, vl.CostCenterID, vl.ProjectID, vl.CurrencyID, SUM(COALESCE(vl.CurrencyValue, 0)) AS CurrencyValueSum, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[Voucher] v
    INNER JOIN [Finance].[VoucherLine] vl ON v.VoucherID = vl.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    LEFT OUTER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID
    LEFT OUTER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID
    LEFT OUTER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID
    LEFT OUTER JOIN [Finance].[Currency] curr ON vl.CurrencyID = curr.CurrencyID
WHERE v.FiscalPeriodID = {0} AND vl.BranchID = {1} AND vl.AccountID = {2} AND v.SubjectType = 0
GROUP BY vl.DetailID, vl.CostCenterID, vl.ProjectID, vl.CurrencyID
ORDER BY vl.DetailID, vl.CostCenterID, vl.ProjectID, vl.CurrencyID";
    }
}
