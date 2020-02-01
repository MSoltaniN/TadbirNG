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
        public DateTime VoucherDate { get; set; }

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
        /// مانده حساب مورد نظر پس از اعمال مبلغ بدهکار یا بستانکار ارزی آرتیکل مالی
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// شناسه دیتابیسی ارز در آرتیکل مالی
        /// </summary>
        public int? CurrencyId { get; set; }

        /// <summary>
        /// مبلغ بدهکار ارزی در آرتیکل مالی
        /// </summary>
        public decimal BaseCurrencyDebit { get; set; }

        /// <summary>
        /// مبلغ بستانکار ارز پایه در آرتیکل مالی
        /// </summary>
        public decimal BaseCurrencyCredit { get; set; }

        /// <summary>
        /// مانده حساب مورد نظر پس از اعمال مبلغ بدهکار یا بستانکار ارز پایه آرتیکل مالی
        /// </summary>
        public decimal BaseCurrencyBalance { get; set; }

        /// <summary>
        /// نام ارز انتخاب شده در آرتیکل مالی
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// نرخ ارز انتخاب شده در آرتیکل مالی
        /// </summary>
        public string CurrencyRate { get; set; }

        /// <summary>
        /// علامتگذاری کاربر روی آرتیکل
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// نام شعبه ایجادکننده آرتیکل
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای که آرتیکل برای آن ایجاد شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی سند مالی
        /// </summary>
        public int VoucherStatusId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر تاییدکننده سند مالی
        /// </summary>
        public int? VoucherConfirmedById { get; set; }

        /// <summary>
        /// شناسه دیتابیسی کاربر تصویب کننده سند مالی
        /// </summary>
        public int? VoucherApprovedById { get; set; }

        /// <summary>
        /// هنگامیکه کلیه ارزها انتخاب شده باشد این فیلد مقدار درست میگیرد
        /// </summary>
        public bool HasChild { get; set; } = false;

        /// <summary>
        /// رفرنس سند عملیاتی در سند مالی
        /// </summary>
        public string VoucherReference { get; set; }
    }
}
