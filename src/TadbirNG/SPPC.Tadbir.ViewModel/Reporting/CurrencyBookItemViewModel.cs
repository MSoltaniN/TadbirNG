using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعاتی نمایشی یک آرتیکل مالی را جهت استفاده در گزارش دفتر عملیات ارزی نگهداری می کند
    /// </summary>
    public class CurrencyBookItemViewModel : ViewModelBase
    {
        /// <summary>
        /// شناسه دیتابیسی آرتیکل مالی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// تاریخ سند مالی
        /// </summary>
        public DateTime? VoucherDate { get; set; }

        /// <summary>
        /// شماره سند مالی، ستون شماره در نمای لیستی
        /// </summary>
        public int VoucherNo { get; set; }

        /// <summary>
        /// تعداد آرتیکل ها در گزارش های تجمیع شده روی چند سند
        /// </summary>
        public int LineCount { get; set; }

        /// <summary>
        /// شرح آرتیکل مالی
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// مبلغ بدهکار ارزی در آرتیکل مالی
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار ارزی در آرتیکل مالی
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// مانده ارزی در آرتیکل مالی
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// شناسه دیتابیسی ارز در آرتیکل مالی
        /// </summary>
        public int? CurrencyId { get; set; }

        /// <summary>
        /// مبلغ بدهکار ارز پایه در آرتیکل مالی
        /// </summary>
        public decimal BaseCurrencyDebit { get; set; }

        /// <summary>
        /// مبلغ بستانکار ارز پایه در آرتیکل مالی
        /// </summary>
        public decimal BaseCurrencyCredit { get; set; }

        /// <summary>
        /// مانده ارز پایه در آرتیکل مالی
        /// </summary>
        public decimal BaseCurrencyBalance { get; set; }

        /// <summary>
        /// نام ارز انتخاب شده در آرتیکل مالی
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// نرخ ارز انتخاب شده در آرتیکل مالی
        /// </summary>
        public decimal CurrencyRate { get; set; }

        /// <summary>
        /// علامتگذاری کاربر روی آرتیکل
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// نام شعبه ایجادکننده آرتیکل
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// هنگامیکه کلیه ارزها انتخاب شده باشد این فیلد مقدار درست میگیرد
        /// </summary>
        public bool HasChild { get; set; } = false;

        /// <summary>
        /// رفرنس سند مالی
        /// </summary>
        public string VoucherReference { get; set; }
    }
}
