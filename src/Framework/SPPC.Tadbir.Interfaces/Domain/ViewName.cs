using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    public sealed class ViewName
    {
        private ViewName()
        {
        }

        public const int Account = 1;
        public const int Voucher = 2;
        public const int VoucherLine = 3;
        public const int User = 4;
        public const int Role = 5;
        public const int DetailAccount = 6;
        public const int CostCenter = 7;
        public const int Project = 8;
        public const int FiscalPeriod = 9;
        public const int Branch = 10;
        public const int Company = 11;
    }
}
