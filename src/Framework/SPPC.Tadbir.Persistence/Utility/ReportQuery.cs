using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Persistence
{
    public class ReportQuery
    {
        public ReportQuery(string query = null)
        {
            Query = query;
        }

        public string Query { get; private set; }

        public void ApplyOptions(GridOptions options)
        {
            string filter = options.QuickFilter != null
                ? String.Format(" AND {0}", options.QuickFilter.ToString())
                : String.Empty;
            if (!String.IsNullOrEmpty(filter))
            {
                filter = filter.Replace("Voucher", "v.");
                filter = filter.Replace("== null", " IS NULL");
                filter = filter.Replace("!= null", "IS NOT NULL");
                filter = filter.Replace("==", " =");
                filter = filter.Replace("!=", " <>");
                filter = filter.Replace(">=", " >=");
                filter = filter.Replace("<=", " <=");
                filter = filter.Replace("BranchId", "vl.BranchId");
                filter = filter.Replace("Mark", "vl.Mark");
                filter = filter.Replace("&&", "AND");
                filter = filter.Replace("||", "OR");
                Query = String.Format(Query, filter);
            }
        }
    }
}
