using System;

namespace SPPC.Tadbir.Persistence
{
    internal class BookQuery
    {
        #region Book By Date Queries

        internal const string TotalByDateByRow = @"
SELECT COUNT(*) AS TotalCount, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND {{0}}";

        internal const string ByDateByRow = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, vl.Description, vl.Debit, vl.Credit, vl.Mark, vl.VoucherLineID AS [Id]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND {{0}}
ORDER BY CONVERT(date, v.Date), v.No, vl.RowNo";

        internal const string ByDateByRowByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS BranchName, vl.VoucherLineID AS [Id]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND {{0}}
ORDER BY CONVERT(date, v.Date), v.No, vl.BranchID";

        internal const string VoucherSumByDate = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), v.No
ORDER BY CONVERT(date, v.Date), v.No";

        internal const string VoucherSumByDateByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), v.No, br.BranchID, br.Name
ORDER BY CONVERT(date, v.Date), v.No, br.BranchID";

        internal const string DailySumByDate = @"
SELECT CONVERT(date, v.Date) AS Date, COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date)
ORDER BY CONVERT(date, v.Date)";

        internal const string DailySumByDateByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), br.BranchID, br.Name
ORDER BY CONVERT(date, v.Date), br.BranchID";

        internal const string MonthlySumByDate = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND v.OriginID = 1 AND vl.Debit > 0 AND {{0}}";

        internal const string MonthlySumByDateByBranch = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND acc.FullCode LIKE '{3}%' AND v.OriginID = 1 AND vl.Debit > 0 AND {{0}}
GROUP BY br.BranchID, br.Name
ORDER BY br.BranchID";

        internal const string SpecialVoucherByDate = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND v.OriginID = {3} AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}";

        internal const string SpecialVoucherByDateByBranch = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.Date >= '{1}' AND v.Date <= '{2}' AND v.OriginID = {3} AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}
GROUP BY br.BranchID, br.Name
ORDER BY br.BranchID";

        #endregion

        #region Book By No Queries

        internal const string TotalByNoByRow = @"
SELECT COUNT(*) AS TotalCount, SUM(vl.Debit) AS DebitSum, SUM(vl.Credit) AS CreditSum
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND {{0}}";

        internal const string ByNoByRow = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, vl.Description, vl.Debit, vl.Credit, vl.Mark, vl.VoucherLineID AS [Id]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND {{0}}
ORDER BY v.No, vl.RowNo";

        internal const string ByNoByRowByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, vl.Description, vl.Debit, vl.Credit, vl.Mark, br.Name AS BranchName, vl.VoucherLineID AS [Id]
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND {{0}}
ORDER BY v.No, vl.BranchID";

        internal const string VoucherSumByNo = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), v.No
ORDER BY v.No";

        internal const string VoucherSumByNoByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, v.No, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), v.No, br.BranchID, br.Name
ORDER BY v.No, br.BranchID";

        internal const string DailySumByNo = @"
SELECT CONVERT(date, v.Date) AS Date, COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date)
ORDER BY CONVERT(date, v.Date)";

        internal const string DailySumByNoByBranch = @"
SELECT CONVERT(date, v.Date) AS Date, COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND vl.Debit > 0 AND {{0}}
GROUP BY CONVERT(date, v.Date), br.BranchID, br.Name
ORDER BY CONVERT(date, v.Date), br.BranchID";

        internal const string MonthlySumByNo = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND v.OriginID = 1 AND vl.Debit > 0 AND {{0}}";

        internal const string MonthlySumByNoByBranch = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= {1} AND v.No <= {2} AND acc.FullCode LIKE '{3}%' AND v.OriginID = 1 AND vl.Debit > 0 AND {{0}}
GROUP BY br.BranchID, br.Name
ORDER BY br.BranchID";

        internal const string SpecialVoucherByNo = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
WHERE v.No >= {1} AND v.No <= {2} AND v.OriginID = {3} AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}";

        internal const string SpecialVoucherByNoByBranch = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS Debit, 0 AS Credit1, br.Name AS BranchName
FROM [Finance].[VoucherLine] vl
    INNER JOIN [Finance].[Voucher] v ON vl.VoucherID = v.VoucherID
    INNER JOIN [Finance].[{0}] acc ON vl.{0}ID = acc.{0}ID
    INNER JOIN [Corporate].[Branch] br ON vl.BranchID = br.BranchID
WHERE v.No >= {1} AND v.No <= {2} AND v.OriginID = {3} AND acc.FullCode LIKE '{4}%' AND vl.Debit > 0 AND {{0}}
GROUP BY br.BranchID, br.Name
ORDER BY br.BranchID";

        #endregion
    }
}
