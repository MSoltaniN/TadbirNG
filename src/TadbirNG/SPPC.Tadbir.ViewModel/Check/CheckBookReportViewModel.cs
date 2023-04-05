using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.Check
{
    /// <summary>
    /// اطلاعات گزارش دسته چک را نگهداری می کند
    /// </summary>
    public class CheckBookReportViewModel:CheckBookViewModel
    {
        /// <summary>
        /// بانک - کد حساب
        /// </summary>
        public string AccountCode { get; set; }

        /// <summary>
        /// بانک - نام حساب
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// بانک - نام تفصیلی شناور
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        /// بانک - نام مرکز هزینه
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// بانک - نام پروژه
        /// </summary>
        public string ProjectName { get; set; }
    }
}
