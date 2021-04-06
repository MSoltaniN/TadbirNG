using System;
using System.Text;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class ReportQuery
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        public ReportQuery(string query)
        {
            Query = query;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static string TranslateQuery(string query)
        {
            var builder = new StringBuilder(query);
            if (!String.IsNullOrEmpty(query))
            {
                builder = builder
                    .Replace("Voucher", "v.")
                    .Replace('"', '\'')
                    .Replace("== null", " IS NULL")
                    .Replace("!= null", " IS NOT NULL")
                    .Replace("==", " =")
                    .Replace("!=", " <>")
                    .Replace(">=", " >=")
                    .Replace("<=", " <=")
                    .Replace("BranchId", "vl.BranchID")
                    .Replace("&&", "AND")
                    .Replace("||", "OR");
            }

            return builder.ToString();
        }

        /// <summary>
        ///
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
        public void SetFilter(string filter)
        {
            int index = Query.IndexOf("{0}");
            if (index != -1)
            {
                string translated = TranslateQuery(filter);
                Query = String.Format(Query, translated);
            }
        }
    }
}
