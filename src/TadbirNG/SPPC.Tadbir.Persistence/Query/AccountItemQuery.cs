using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class AccountItemQuery
    {
        internal const string ItemLookup = @"
SELECT DISTINCT(SUBSTRING(acc.FullCode, 1, {0})) AS FullCode,
    (SELECT {1}ID FROM [Finance].[{1}] WHERE FullCode = SUBSTRING(acc.FullCode, 1, {0})) AS Id,
    (SELECT Name FROM [Finance].[{1}] WHERE FullCode = SUBSTRING(acc.FullCode, 1, {0})) AS Name
FROM [Finance].[{1}] acc
WHERE acc.FiscalPeriodID <= {2}";

        internal const string ItemLookupExact = @"
SELECT DISTINCT(SUBSTRING(acc.FullCode, 1, {0})) AS FullCode,
    (SELECT {1}ID FROM [Finance].[{1}] WHERE FullCode = SUBSTRING(acc.FullCode, 1, {0})) AS Id,
    (SELECT Name FROM [Finance].[{1}] WHERE FullCode = SUBSTRING(acc.FullCode, 1, {0})) AS Name,
    (SELECT Description FROM [Finance].[{1}] WHERE FullCode = SUBSTRING(acc.FullCode, 1, {0})) AS Description
FROM [Finance].[{1}] acc
WHERE acc.FiscalPeriodID <= {2} AND acc.FullCode IN({3})";

        internal const string PreviousBalanceByCode = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
WHERE v.Date <= '{2}' AND ({3}) AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string SpecialVoucherBalanceByCode = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{1}] acc ON vl.{1}ID = acc.{1}ID
WHERE v.OriginId = {2} AND ({3}) AND {{0}}
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";

        internal const string BalanceByDate = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date < '{1}' AND acc.FullCode LIKE '{2}%' AND {{0}}";

        internal const string OpeningVoucherBalanceByNo = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.OriginId = 2 AND acc.FullCode LIKE '{2}%' AND {{0}}";

        internal const string CollectionAccounts = @"
SELECT acc.AccountID AS Id, acc.Name, acc.FullCode
FROM [Finance].[AccountCollectionAccount] aca
    INNER JOIN [Finance].[Account] acc ON acc.AccountID = aca.AccountID
WHERE aca.FiscalPeriodID <= {0} AND aca.BranchID = {1} AND aca.CollectionID = {2}";

        internal const string EnvironmentAccounts = @"
SELECT acc.AccountID AS Id, acc.Name, acc.FullCode
FROM [Finance].[Account] acc
WHERE acc.FiscalPeriodID <= {0} AND
    (acc.BranchScope = 0
        OR (acc.BranchScope = 1 AND acc.BranchID IN({1}))
        OR (acc.BranchScope = 2 AND acc.BranchID = {2}))";

        internal const string LeafAccountFilter = @"
NOT EXISTS(SELECT AccountID FROM [Finance].[Account] WHERE ParentID = acc.AccountID)";
    }
}
