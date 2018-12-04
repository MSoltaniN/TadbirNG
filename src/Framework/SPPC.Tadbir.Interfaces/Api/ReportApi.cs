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

        public const string VoucherStandardFormWithDetail = "reports/voucher/std-form-detail/{0}";

        public const string VoucherStandardFormWithDetailUrl = "reports/voucher/std-form-detail/{voucherId:min(1)}";
    }
}
