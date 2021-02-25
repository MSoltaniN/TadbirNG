using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class AccountItemQuery
    {
        internal const string ItemLookup = @"
SELECT DISTINCT(SUBSTRING(acc.FullCode, 1, {0})) AS FullCode,
    (SELECT Name FROM [Finance].[{1}] WHERE FullCode = SUBSTRING(acc.FullCode, 1, {0})) AS Name
FROM [Finance].[{1}] acc
WHERE acc.FiscalPeriodID = {2}";

        internal const string BalanceByDate = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date < '{2}' AND acc.FullCode LIKE '{3}%' AND {{0}}";

        internal const string OpeningVoucherBalanceByDate = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= {2} AND v.VoucherOriginId = 2 AND acc.FullCode LIKE '{3}%' AND {{0}}";

        internal const string BalanceByNo = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.No < '{2}' AND acc.FullCode LIKE '{3}%' AND {{0}}";

        internal const string OpeningVoucherBalanceByNo = @"
SELECT SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.No >= {2} AND v.VoucherOriginId = 2 AND acc.FullCode LIKE '{3}%' AND {{0}}";
    }
}
