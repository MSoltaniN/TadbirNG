﻿using System;
using System.Text;
using SPPC.Framework.Common;

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
        public string Query { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="environmentFilter"></param>
        /// <param name="quickFilter"></param>
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="filter"></param>
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

        private static string TranslateQuery(string query)
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
                    .Replace("Level", "acc.Level")
                    .Replace("FullCode", "acc.FullCode")
                    .Replace("Mark", "vl.Mark")
                    .Replace("&&", "AND")
                    .Replace("||", "OR");
            }

            return builder.ToString();
        }
    }
}
