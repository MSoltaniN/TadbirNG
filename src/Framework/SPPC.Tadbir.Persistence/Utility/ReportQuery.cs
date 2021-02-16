using System;
using System.Text;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.Persistence
{
    public class ReportQuery
    {
        public ReportQuery(string query)
        {
            Query = query;
        }

        public string Query { get; private set; }

        public void ApplyDefaultFilters(string environmentFilter, string quickFilter)
        {
            Verify.ArgumentNotNullOrEmptyString(environmentFilter, nameof(environmentFilter));
            string filter1 = String.Format(" AND {0}", environmentFilter);
            string filter2 = !String.IsNullOrEmpty(quickFilter)
                ? String.Format(" AND {0}", quickFilter)
                : String.Empty;
            filter1 = TranslateQuery(filter1);
            filter2 = TranslateQuery(filter2);
            Query = String.Format(Query, filter1, filter2);
        }

        public void AddFilter(string filter)
        {
            int index = Query.IndexOf("{0}");
            if (index != -1)
            {
                var builder = new StringBuilder(Query);
                Query = builder
                    .Insert(index, filter)
                    .ToString();
            }
        }

        private string TranslateQuery(string query)
        {
            var builder = new StringBuilder(query);
            if (!String.IsNullOrEmpty(query))
            {
                builder = builder
                    .Replace("Voucher", "v.")
                    .Replace("== null", " IS NULL")
                    .Replace("!= null", " IS NOT NULL")
                    .Replace("==", " =")
                    .Replace("!=", " <>")
                    .Replace(">=", " >=")
                    .Replace("<=", " <=")
                    .Replace("FiscalPeriodId", "v.FiscalPeriodId")
                    .Replace("BranchId", "vl.BranchId")
                    .Replace("Mark", "vl.Mark")
                    .Replace("&&", "AND")
                    .Replace("||", "OR");
            }

            return builder.ToString();
        }
    }
}
