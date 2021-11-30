using System;

namespace SPPC.Tadbir.Persistence
{
    internal class CurrencyBookQuery
    {
        internal const string ByRowSelect = @"
SELECT v.Date, v.No, vl.Description, v.Reference,
vl.CurrencyValue AS [Debit], vl.CurrencyValue AS [Credit],
vl.Debit AS [BaseCurrencyDebit], vl.Credit AS [BaseCurrencyCredit], vl.Mark, vl.VoucherLineID";

        internal const string ByRowByBranchSelect = @"
SELECT v.Date, v.No, vl.Description, v.Reference,
vl.CurrencyValue AS [Debit], vl.CurrencyValue AS [Credit],
vl.Debit AS [BaseCurrencyDebit], vl.Credit AS [BaseCurrencyCredit], vl.Mark, vl.VoucherLineID,
br.Name AS [BranchName]";

        internal const string VoucherSumSelect = @"
SELECT CAST(v.Date AS date), v.No, v.Reference, SUM(vl.Debit) AS [BaseCurrencyDebit],
SUM(vl.Credit) AS [BaseCurrencyCredit], SUM(vl.CurrencyValue) AS [Debit],
SUM(vl.CurrencyValue) AS [Credit]";

        internal const string VoucherSumGroupBy = @"GROUP BY CAST(v.Date AS date), v.No, v.Reference
ORDER BY CAST(v.Date AS date), v.No";

        internal const string VoucherSumByBranchSelect = @"
SELECT CAST(v.Date AS date), v.No, v.Reference, SUM(vl.Debit) AS [BaseCurrencyDebit],
SUM(vl.Credit) AS [BaseCurrencyCredit], SUM(vl.CurrencyValue) AS [Debit],
SUM(vl.CurrencyValue) AS [Credit], br.Name AS [BranchName]";

        internal const string VoucherSumByBranchGroupBy = @"GROUP BY CAST(v.Date AS date), v.No, br.Name, v.Reference
ORDER BY CAST(v.Date AS date), v.No, br.Name";

        internal const string DailySumSelect = @"
SELECT CAST(v.Date AS date), COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS [BaseCurrencyDebit],
SUM(vl.Credit) AS [BaseCurrencyCredit], SUM(vl.CurrencyValue) AS [Debit],
SUM(vl.CurrencyValue) AS [Credit]";

        internal const string DailySumGroupBy = @"GROUP BY CAST(v.Date AS date)
ORDER BY CAST(v.Date AS date)";

        internal const string DailySumByBranchSelect = @"
SELECT CAST(v.Date AS date), COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS [BaseCurrencyDebit],
SUM(vl.Credit) AS [BaseCurrencyCredit], SUM(vl.CurrencyValue) AS [Debit],
SUM(vl.CurrencyValue) AS [Credit], br.Name AS [BranchName]";

        internal const string DailySumByBranchGroupBy = @"GROUP BY CAST(v.Date AS date), br.Name
ORDER BY CAST(v.Date AS date), br.Name";

        internal const string MonthlySumSelect = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS [BaseCurrencyDebit],
SUM(vl.Credit) AS [BaseCurrencyCredit], SUM(vl.CurrencyValue) AS [Debit],
SUM(vl.CurrencyValue) AS [Credit]";

        internal const string MonthlySumByBranchSelect = @"
SELECT COUNT(vl.RowNo) AS LineCount, SUM(vl.Debit) AS [BaseCurrencyDebit],
SUM(vl.Credit) AS [BaseCurrencyCredit], SUM(vl.CurrencyValue) AS [Debit],
SUM(vl.CurrencyValue) AS [Credit], br.Name AS BranchName";

        internal const string MonthlySumByBranchGroupBy = @"GROUP BY br.Name
ORDER BY br.Name";
    }
}
