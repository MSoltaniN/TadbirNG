using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class BalanceQuery
    {
        internal const string TwoColumnByDate = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
WHERE v.Date >= '{3}' AND v.Date <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string FourColumnByDate = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
WHERE v.Date >= '{3}' AND v.Date <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string TwoColumnByDateByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{3}' AND v.Date <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        internal const string FourColumnByDateByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{3}' AND v.Date <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        internal const string TwoColumnByNo = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
WHERE v.No >= '{3}' AND v.No <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string FourColumnByNo = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
WHERE v.No >= '{3}' AND v.No <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string TwoColumnByNoByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{3}' AND v.No <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        internal const string FourColumnByNoByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{3}' AND v.No <= '{4}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        internal const string InitBalanceByDate = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
WHERE v.Date < '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string InitBalanceByDateByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date < '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        internal const string InitBalanceByNo = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
WHERE v.No < '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string InitBalanceByNoByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{2}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No < '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";
    }
}
