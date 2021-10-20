using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات نمایشی گزارش مالی دفتر روزنامه را نگهداری می کند
    /// </summary>
    public class JournalViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public JournalViewModel()
        {
            Items = new List<JournalItemViewModel>();
        }

        /// <summary>
        /// مجموعه سطرهای اطلاعاتی گزارش
        /// </summary>
        public List<JournalItemViewModel> Items { get; }

        /// <summary>
        /// جمع مبالغ بدهکار در تمام سطرهای گزارش
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مبالغ بستانکار در تمام سطرهای گزارش
        /// </summary>
        public decimal CreditSum { get; set; }

        /// <summary>
        /// تعداد کل سطرهای گزارش پس از اعمال فیلترهای سریع و فیلترهای ستونی
        /// </summary>
        public int TotalCount { get; set; }
    }
}
