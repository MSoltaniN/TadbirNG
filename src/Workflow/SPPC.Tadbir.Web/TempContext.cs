using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPPC.Tadbir.Web
{
    public sealed class TempContext
    {
        private TempContext()
        {
        }

        public const int CurrentBranchId = 1;
        public const int CurrentFiscalPeriodId = 1;
    }
}
