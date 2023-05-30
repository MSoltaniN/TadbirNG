using SPPC.Tadbir.ViewModel.Finance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    public partial class PayReceiveAccountViewModel
    {
        /// <summary>
        /// بردار حساب مورد استفاده برای طرف حساب
        /// </summary>
        public FullAccountViewModel FullAccount { get; set; }

        /// <summary>
        /// شناسه فرم دریافت/پرداخت اصلی
        /// </summary>
        public int PayReceiveId { get; set; }
    }
}
