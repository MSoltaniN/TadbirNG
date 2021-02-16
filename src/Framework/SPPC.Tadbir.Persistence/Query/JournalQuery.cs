using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class JournalQuery
    {
        internal const string MainByDateByRow = @"
SELECT COUNT(*) AS TotalCount, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
WHERE v.Date >= '{0}' AND v.Date <= '{1}'{{0}} {{1}}";

        internal const string ByDateByRow = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.RowNo ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
vl.Description, vl.Debit, vl.Credit, vl.Mark
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.Date >= '{0}' AND v.Date <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string ByDateByRowByBranch = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.BranchId ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{0}' AND v.Date <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string ByDateByRowDetails = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.RowNo ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
facc.FullCode AS [DetailFullCode], facc.Name AS [DetailName], cc.FullCode AS [CostFullCode], cc.Name AS [CostName],
prj.FullCode AS [ProjectFullCode], prj.Name AS [ProjectName], vl.Description, vl.Debit, vl.Credit, vl.Mark
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    LEFT OUTER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID
    LEFT OUTER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID
    LEFT OUTER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID
WHERE v.Date >= '{0}' AND v.Date <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string ByDateByRowDetailsByBranch = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.BranchId ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
facc.FullCode AS [DetailFullCode], facc.Name AS [DetailName], cc.FullCode AS [CostFullCode], cc.Name AS [CostName],
prj.FullCode AS [ProjectFullCode], prj.Name AS [ProjectName], vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
    LEFT OUTER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID
    LEFT OUTER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID
    LEFT OUTER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID
WHERE v.Date >= '{0}' AND v.Date <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string MainByNoByRow = @"
SELECT COUNT(*) AS TotalCount, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
WHERE v.No >= '{0}' AND v.No <= '{1}'{{0}} {{1}}";

        internal const string ByNoByRow = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.RowNo ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
vl.Description, vl.Debit, vl.Credit, vl.Mark
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.No >= '{0}' AND v.No <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string ByNoByRowByBranch = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.BranchId ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{0}' AND v.No <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string ByNoByRowDetail = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.RowNo ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
facc.FullCode AS [DetailFullCode], facc.Name AS [DetailName], cc.FullCode AS [CostFullCode], cc.Name AS [CostName],
prj.FullCode AS [ProjectFullCode], prj.Name AS [ProjectName], vl.Description, vl.Debit, vl.Credit, vl.Mark
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    LEFT OUTER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID
    LEFT OUTER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID
    LEFT OUTER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID
WHERE v.No >= '{0}' AND v.No <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string ByNoByRowDetailByBranch = @"
SELECT *
FROM ( SELECT ROW_NUMBER() OVER ( ORDER BY v.Date, v.No, vl.BranchId ) AS RowNum, v.Date, v.No, acc.FullCode, acc.Name,
facc.FullCode AS [DetailFullCode], facc.Name AS [DetailName], cc.FullCode AS [CostFullCode], cc.Name AS [CostName],
prj.FullCode AS [ProjectFullCode], prj.Name AS [ProjectName], vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
    LEFT OUTER JOIN [Finance].[DetailAccount] facc ON vl.DetailID = facc.DetailAccountID
    LEFT OUTER JOIN [Finance].[CostCenter] cc ON vl.CostCenterID = cc.CostCenterID
    LEFT OUTER JOIN [Finance].[Project] prj ON vl.ProjectID = prj.ProjectID
WHERE v.No >= '{0}' AND v.No <= '{1}'{{0}} {{1}} ) AS PagedResult
WHERE RowNum >= {2}
    AND RowNum <= {3}
ORDER BY RowNum";

        internal const string ByDateByLevel = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY CONVERT(date, v.Date), v.No, SUBSTRING(acc.FullCode, 1, {0})
ORDER BY CONVERT(date, v.Date), v.No, SUBSTRING(acc.FullCode, 1, {0})";

        internal const string ByNoByLevel = @"
SELECT v.Date, v.No, SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.No >= '{1}' AND v.No <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY v.No, v.Date, SUBSTRING(acc.FullCode, 1, {0})
ORDER BY v.No, SUBSTRING(acc.FullCode, 1, {0})";

        internal const string ByDateByLevelByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY CONVERT(date, v.Date), v.No, SUBSTRING(acc.FullCode, 1, {0}), br.Name
ORDER BY CONVERT(date, v.Date), v.No, SUBSTRING(acc.FullCode, 1, {0}), br.Name";

        internal const string ByNoByLevelByBranch = @"
SELECT v.Date, v.No, SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{1}' AND v.No <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY v.No, v.Date, SUBSTRING(acc.FullCode, 1, {0}), br.Name
ORDER BY v.No, SUBSTRING(acc.FullCode, 1, {0}), br.Name";

        internal const string ByDateLedgerSummary = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string ByNoLedgerSummary = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.No >= '{1}' AND v.No <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string ByDateLedgerSummaryByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.Name";

        internal const string ByNoLedgerSummaryByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{1}' AND v.No <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.Name";

        internal const string LedgerSummaryByDate = @"
SELECT CONVERT(date, v.Date) AS Date, SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY CONVERT(date, v.Date), SUBSTRING(acc.FullCode, 1, {0})
ORDER BY CONVERT(date, v.Date), SUBSTRING(acc.FullCode, 1, {0})";

        internal const string LedgerSummaryByDateByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY CONVERT(date, v.Date), SUBSTRING(acc.FullCode, 1, {0}), br.Name
ORDER BY CONVERT(date, v.Date), SUBSTRING(acc.FullCode, 1, {0}), br.Name";

        internal const string MonthlyLedgerSummary = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string MonthlyLedgerSummaryByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS [BranchName]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND vl.Debit > 0{{0}} {{1}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.Name";
    }
}
