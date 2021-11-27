using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات گزارش دفتر عملیات ارزی را نگهداری میکند
    /// </summary>
    public class CurrencyBookViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CurrencyBookViewModel()
        {
            Items = new List<CurrencyBookItemViewModel>();
        }

        /// <summary>
        /// جمع مبالغ بدهکار ارزی در تمام سطرهای گزارش
        /// </summary>
        public decimal DebitSum { get; set; }

        /// <summary>
        /// جمع مبالغ بستانکار ارزی در تمام سطرهای گزارش
        /// </summary>
        public decimal CreditSum { get; set; }

        /// <summary>
        /// مانده نهایی ارزی در انتهای دوره گزارشگیری
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// جمع مبالغ بدهکار در تمام سطرهای گزارش
        /// </summary>
        public decimal BaseCurrencyDebitSum { get; set; }

        /// <summary>
        /// جمع مبالغ بستانکار در تمام سطرهای گزارش
        /// </summary>
        public decimal BaseCurrencyCreditSum { get; set; }

        /// <summary>
        /// مانده نهایی ارز پایه در انتهای دوره گزارشگیری
        /// </summary>
        public decimal BaseCurrencyBalance { get; set; }

        /// <summary>
        /// تعداد کل سطرهای اطلاعاتی گزارش
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// مجموعه سطرهای اطلاعاتی گزارش
        /// </summary>
        public List<CurrencyBookItemViewModel> Items { get; }

        /// <summary>
        /// سطرهای اطلاعاتی گزارش را با مجوعه سطرهای اطلاعاتی داده شده جایگزین می کند
        /// </summary>
        /// <param name="items">سطرهای اطلاعاتی مورد نظر برای جایگزینی در مدل نمایشی</param>
        public void SetItems(IEnumerable<CurrencyBookItemViewModel> items)
        {
            Items.Clear();
            Items.AddRange(items);
        }
    }
}
