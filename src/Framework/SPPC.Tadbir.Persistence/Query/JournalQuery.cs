using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    public static class JournalQuery
    {
        public const string MainByDateByRow = @"
SELECT COUNT(*) AS TotalCount, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
WHERE v.Date >= '{0}' AND v.Date <= '{1}'{{0}}";

        public const string ByDateByRow = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.BranchId ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
facc.FullCode AS [DetailFullCode], facc.Name AS [DetailName], cc.FullCode AS [CostFullCode], cc.Name AS [CostName],
prj.FullCode AS [ProjectFullCode], prj.Name AS [ProjectName], vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID
    INNER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID
    INNER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{0}' AND v.Date <= '{1}'{{0}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        public const string MainByNoByRow = @"
SELECT COUNT(*) AS TotalCount, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
WHERE v.No >= '{0}' AND v.No <= '{1}'{{0}}";

        public const string ByNoByRow = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.BranchId ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
facc.FullCode AS [DetailFullCode], facc.Name AS [DetailName], cc.FullCode AS [CostFullCode], cc.Name AS [CostName],
prj.FullCode AS [ProjectFullCode], prj.Name AS [ProjectName], vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID
    INNER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID
    INNER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{0}' AND v.No <= '{1}'{{0}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";
    }
}
