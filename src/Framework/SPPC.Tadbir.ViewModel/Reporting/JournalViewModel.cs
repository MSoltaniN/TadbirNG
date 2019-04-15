using System;
using System.Collections.Generic;
using System.Text;

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
        /// سطرهای اطلاعاتی گزارش را با مجوعه سطرهای اطلاعاتی داده شده جایگزین می کند
        /// </summary>
        /// <param name="items">سطرهای اطلاعاتی مورد نظر برای جایگزینی در مدل نمایشی</param>
        public void SetItems(IEnumerable<JournalItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
