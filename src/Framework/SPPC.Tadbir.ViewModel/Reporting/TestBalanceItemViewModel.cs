using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از سطرهای گزارش تراز آزمایشی را نگهداری می کند
    /// </summary>
    public class TestBalanceItemViewModel : IAccountView
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public TestBalanceItemViewModel()
        {
        }

        /// <summary>
        /// شماره ردیف سطر در گزارش
        /// </summary>
        public int RowNo { get; set; }

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
        /// شماره سطح که عمق حساب به کار رفته در آرتیکل مالی را در ساختار درختی مشخص می کند
        /// </summary>
        public short AccountLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public int DetailAccountId { get; set; }

        /// <summary>
        /// نام تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        /// کد کامل تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        /// شماره سطح که عمق تفصیلی شناور به کار رفته در آرتیکل مالی را در ساختار درختی مشخص می کند
        /// </summary>
        public short DetailAccountLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public int CostCenterId { get; set; }

        /// <summary>
        /// نام مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// کد کامل مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        /// شماره سطح که عمق مرکز هزینه به کار رفته در آرتیکل مالی را در ساختار درختی مشخص می کند
        /// </summary>
        public short CostCenterLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// نام پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// کد کامل پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        /// شماره سطح که عمق پروژه به کار رفته در آرتیکل مالی را در ساختار درختی مشخص می کند
        /// </summary>
        public short ProjectLevel { get; set; }

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

        /// <summary>
        /// رفرنس سند مالی
        /// </summary>
        public string VoucherReference { get; set; }
    }
}
