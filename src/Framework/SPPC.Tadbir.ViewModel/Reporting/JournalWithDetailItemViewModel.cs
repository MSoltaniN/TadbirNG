using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعاتی نمایشی یک آرتیکل مالی را جهت استفاده در گزارش دفتر روزنامه نگهداری می کند
    /// </summary>
    public class JournalWithDetailItemViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی آرتیکل مالی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شماره ردیف سطر اطلاعاتی در گزارش با مرتب سازی پیش فرض گزارش
        /// </summary>
        public int RowNo { get; set; }

        /// <summary>
        /// تاریخ سند مالی
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// شماره سند مالی، ستون شماره در نمای لیستی
        /// </summary>
        public string VoucherNo { get; set; }

        /// <summary>
        /// کد کامل سرفصل حسابداری مورد استفاده در ردیف سند، ستون شماره حساب در نمای لیستی
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// نام سرفصل حسابداری مورد استفاده در ردیف سند، ستون عنوان حساب در نمای لیستی
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// کد کامل تفصیلی شناور مورد استفاده در ردیف سند
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        /// نام تفصیلی شناور مورد استفاده در ردیف سند
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        /// کد کامل مرکز هزینه مورد استفاده در ردیف سند
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        /// نام مرکز هزینه مورد استفاده در ردیف سند
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// کد کامل پروژه مورد استفاده در ردیف سند
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        /// نام پروژه مورد استفاده در ردیف سند
        /// </summary>
        public string ProjectName { get; set; }

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
    }
}
