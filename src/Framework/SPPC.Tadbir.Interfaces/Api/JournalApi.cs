using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Api
{
    public sealed class JournalApi
    {
        private JournalApi()
        {
        }

        /// <summary>
        /// API client URL for Journal-By Date-By Row report
        /// </summary>
        public const string JournalByDateByRow = "reports/journal/by-date/by-row";

        /// <summary>
        /// API server route URL for Journal-By Date-By Row report
        /// </summary>
        public const string JournalByDateByRowUrl = "reports/journal/by-date/by-row";
    }
}
