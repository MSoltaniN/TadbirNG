using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal class BookQuery
    {
        internal const string TotalByRow = @"
SELECT COUNT(*) AS TotalCount, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND {{0}}";

        internal const string BookByRow = @"
SELECT v.Date, v.No, vl.Description, vl.Debit, vl.Credit, vl.Mark
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND {{0}}
ORDER BY v.Date, v.No, vl.RowNo";
    }
}
