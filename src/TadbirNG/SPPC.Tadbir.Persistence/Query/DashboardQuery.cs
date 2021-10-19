using System;
using System.Collections.Generic;

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
    }
}
