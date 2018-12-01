using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Api
{
    public sealed class ReportApi
    {
        private ReportApi()
        {
        }

        public const string EnvironmentVoucherSummaryByDate = "reports/voucher/sum-by-date";

        public const string EnvironmentVoucherSummaryByDateUrl = "reports/voucher/sum-by-date";

        public const string VoucherStandardForm = "reports/voucher/std-form/{0}";

        public const string VoucherStandardFormUrl = "reports/voucher/std-form/{voucherId:min(1)}";
    }
}
