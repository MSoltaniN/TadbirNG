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

        internal const string ByRow = @"
SELECT v.Date, v.No, vl.Description, vl.Debit, vl.Credit, vl.Mark
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND {{0}}
ORDER BY v.Date, v.No, vl.RowNo";

        internal const string ByRowByBranch = @"
SELECT v.Date, v.No, vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND {{0}}
ORDER BY v.Date, v.No, vl.BranchID";

        internal const string VoucherSum = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), v.No
ORDER BY CONVERT(date, v.Date), v.No";

        internal const string VoucherSumByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), v.No, br.BranchID, br.Name
ORDER BY CONVERT(date, v.Date), v.No, br.BranchID";

        internal const string DailySum = @"
SELECT CONVERT(date, v.Date) AS Date, COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date)
ORDER BY CONVERT(date, v.Date)";

        internal const string DailySumByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), br.BranchID, br.Name
ORDER BY CONVERT(date, v.Date), br.BranchID";

        internal const string MonthlySum = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND v.OriginID = 1 AND vl.Debit > 0 AND {{0}}";

        internal const string MonthlySumByBranch = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND acc.FullCode LIKE '{4}%' AND v.OriginID = 1 AND vl.Debit > 0 AND {{0}}
GROUP BY br.BranchID, br.Name
ORDER BY br.BranchID";

        internal const string SpecialVoucher = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND v.OriginID = {4} AND acc.FullCode LIKE '{5}%' AND vl.Debit > 0 AND {{0}}";

        internal const string SpecialVoucherByBranch = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{1}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{2}' AND v.Date <= '{3}' AND v.OriginID = {4} AND acc.FullCode LIKE '{5}%' AND vl.Debit > 0 AND {{0}}
GROUP BY br.BranchID, br.Name
ORDER BY br.BranchID";
    }
}
