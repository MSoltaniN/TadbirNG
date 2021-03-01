using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class BalanceSheetQuery
    {
        public const string CollectionBalanceSelect = @"
SELECT SUBSTRING(acc.FullCode, 1, {0}) AS FullCode, SUM(vl.Debit - vl.Credit) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID";

        public const string CollectionBalanceWhere = @"
WHERE v.Date >= '{0}' AND v.Date <= '{1}' AND acc.AccountID IN({2}) AND {{0}}";

        public const string CollectionBalanceEnd = @"
GROUP BY SUBSTRING(acc.FullCode, 1, {0})
ORDER BY SUBSTRING(acc.FullCode, 1, {0})";
    }
}
