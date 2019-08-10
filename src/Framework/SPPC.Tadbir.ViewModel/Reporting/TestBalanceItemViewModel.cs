using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از سطرهای گزارش تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class TestBalanceItemViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public TestBalanceItemViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی حساب مورد استفاده در سطر گزارش
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// نام حساب مورد استفاده در سطر گزارش
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// کد کامل حساب مورد استفاده در سطر گزارش
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// مانده ابتدای دوره - بدهکار
        /// </summary>
        public decimal StartBalanceDebit { get; set; }

        /// <summary>
        /// مانده ابتدای دوره - بستانکار
        /// </summary>
        public decimal StartBalanceCredit { get; set; }

        /// <summary>
        /// گردش طی دوره - بدهکار
        /// </summary>
        public decimal TurnoverDebit { get; set; }

        /// <summary>
        /// گردش طی دوره - بستانکار
        /// </summary>
        public decimal TurnoverCredit { get; set; }

        /// <summary>
        /// جمع عملیات - بدهکار
        /// </summary>
        public decimal OperationSumDebit { get; set; }

        /// <summary>
        /// جمع عملیات - بستانکار
        /// </summary>
        public decimal OperationSumCredit { get; set; }

        /// <summary>
        /// اصلاحات - بدهکار
        /// </summary>
        public decimal CorrectionsDebit { get; set; }

        /// <summary>
        /// اصلاحات - بستانکار
        /// </summary>
        public decimal CorrectionsCredit { get; set; }

        /// <summary>
        /// مانده انتهای دوره - بدهکار
        /// </summary>
        public decimal EndBalanceDebit { get; set; }

        /// <summary>
        /// مانده انتهای دوره - بستانکار
        /// </summary>
        public decimal EndBalanceCredit { get; set; }

        /// <summary>
        /// نام شعبه ایجادکننده آرتیکل
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ایجادکننده آرتیکل
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
    }
}
