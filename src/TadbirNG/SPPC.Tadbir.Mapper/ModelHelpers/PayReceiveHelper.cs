using SPPC.Framework.Common;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Mapper.ModelHelpers
{
    internal class PayReceiveHelper
    {
        internal static decimal GetAccountAmountsSum(PayReceive payReceive)
        {
            Verify.ArgumentNotNull(payReceive, nameof(payReceive));
            return payReceive.Accounts
                .Sum(acc => acc.Amount);
        }

        internal static decimal GetCashAmountsSum(PayReceive payReceive)
        {
            Verify.ArgumentNotNull(payReceive, nameof(payReceive));
            return payReceive.CashAccounts
                .Sum(cashAcc => cashAcc.Amount);
        }
    }
}
