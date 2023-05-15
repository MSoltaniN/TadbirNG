using SPPC.Tadbir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    public partial class PayReceiveViewModel : IFiscalEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که فرم دریافت یا پرداخت در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که فرم دریافت یا پرداخت در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه ارز
        /// </summary>
        public int? CurrencyId { get; set; }

        /// <summary>
        /// مشخص می کند که آیا فرم دریافت/پرداخت مورد نظر تایید شده است یا نه؟
        /// </summary>
        public bool IsConfirmed { get; set; }

        /// <summary>
        /// مشخص می کند که آیا فرم دریافت/پرداخت مورد نظر تصویب شده است یا نه؟
        /// </summary>
        public bool IsApproved { get; set; }
    }
}
