using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    public static class VoucherStatus
    {
        public const string NotBalanced = "NotBalanced";
        public const string Balanced = "Balanced";
        public const string Checked = "Checked";
        public const string Confirmed = "Confirmed";
        public const string Approved = "Approved";
        public const string Finalized = "Finalized";
    }
}
