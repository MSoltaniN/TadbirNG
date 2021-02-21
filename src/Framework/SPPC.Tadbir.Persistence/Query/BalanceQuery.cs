using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    public static class BalanceQuery
    {
        public const string TwoColumnByDate = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        public const string FourColumnByDate = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        public const string TwoColumnByDateByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        public const string FourColumnByDateByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        public const string TwoColumnByNo = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
WHERE v.No >= '{2}' AND v.No <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        public const string FourColumnByNo = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
WHERE v.No >= '{2}' AND v.No <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        public const string TwoColumnByNoByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{2}' AND v.No <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        public const string FourColumnByNoByBranch = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, br.Name AS BranchName, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum,
    SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= '{2}' AND v.No <= '{3}' AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID, br.Name
ORDER BY SUBSTRING(acc.FullCode, 1, {0}), br.BranchID";

        public const string BalanceByDate = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date < '{1}' AND acc.FullCode LIKE '{2}%' AND {{0}}";

        public const string BalanceByNo = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No < '{1}' AND acc.FullCode LIKE '{2}%' AND {{0}}";

        public const string OpeningVoucherBalanceByDate = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date >= {1} AND v.VoucherOriginId = 2 AND acc.FullCode LIKE '{2}%' AND {{0}}";

        public const string OpeningVoucherBalanceByNo = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.VoucherOriginId = 2 AND acc.FullCode LIKE '{2}%' AND {{0}}";
    }
}
