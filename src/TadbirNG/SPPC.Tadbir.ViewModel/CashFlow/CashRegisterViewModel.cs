using SPPC.Tadbir.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.CashFlow
{
    public partial class CashRegisterViewModel : IFiscalEntityView
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که صندوق در آن تعریف شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که صندوق در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه ای که صندوق در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// نام دوره مالی که صندوق در آن تعریف شده است
        /// </summary>
        public string FiscalPeriodName { get; set; }
    }
}
