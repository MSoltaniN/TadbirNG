using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعاتی نمایشی یک آرتیکل مالی را جهت استفاده در گزارش دفتر حساب نگهداری می کند
    /// لازم به یادآوری است که مفهوم حساب در اینجا عمومی تر از سرفصل حسابداری بوده
    /// و شامل همه مولفه های بردار حساب می شود.
    /// </summary>
    public class AccountBookItemViewModel : ViewModelBase
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
        /// مبلغ بدهکار در آرتیکل مالی
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار در آرتیکل مالی
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// مانده حساب مورد نظر پس از اعمال مبلغ بدهکار یا بستانکار آرتیکل مالی
        /// </summary>
        public decimal Balance { get; set; }

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
        ///
        /// </summary>
        /// <returns></returns>
        public AccountBookItemViewModel GetCopy()
        {
            return (AccountBookItemViewModel)MemberwiseClone();
        }
    }
}
