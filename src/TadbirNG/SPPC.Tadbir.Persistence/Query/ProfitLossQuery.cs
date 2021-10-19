using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class ProfitLossQuery
    {
        internal const string BalanceTotalSelect = @"
SELECT SUM({0}) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID";

        internal const string BalanceTotalEnd = @"
WHERE v.Date >= '{0}' AND v.Date <= '{1}' AND ({2}) AND {{0}}";

        internal const string InitBalanceTotalEnd = @"
WHERE v.Date < '{0}' AND ({1}) AND {{0}}";

        internal const string InitBalanceWithOptionTotalEnd = @"
WHERE v.Date <= '{0}' AND ({1}) AND {{0}}";

        internal const string BalanceByAccountSelect = @"
SELECT acc.FullCode, acc.Name, SUM({0}) AS Balance
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[Account] acc ON vl.AccountID = acc.AccountID";

        internal const string BalanceByAccountEnd = @"
WHERE v.Date >= '{0}' AND v.Date <= '{1}' AND ({2}) AND {{0}}
GROUP BY acc.FullCode, acc.Name
ORDER BY acc.FullCode, acc.Name";

        internal const string InitBalanceByAccountEnd = @"
WHERE v.Date < '{0}' AND ({1}) AND {{0}}
GROUP BY acc.FullCode, acc.Name
ORDER BY acc.FullCode, acc.Name";

        internal const string InitBalanceWithOptionByAccountEnd = @"
WHERE v.Date <= '{0}' AND ({1}) AND {{0}}
GROUP BY acc.FullCode, acc.Name
ORDER BY acc.FullCode, acc.Name";
    }
}
