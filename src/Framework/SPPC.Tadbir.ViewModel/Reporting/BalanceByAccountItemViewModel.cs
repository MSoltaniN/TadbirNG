using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از سطرهای گزارش مانده به تفکیک حساب را نگهداری می کند
    /// </summary>
    public class BalanceByAccountItemViewModel : IAccountView
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public BalanceByAccountItemViewModel()
        {
        }

        /// <summary>
        /// شماره ردیف سطر در گزارش
        /// </summary>
        public int RowNo { get; set; }

        /// <summary>
        /// شماره سند آرتیکل
        /// </summary>
        public int VoucherNo { get; set; }

        /// <summary>
        /// تاریخ ایجاد آرتیکل
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// شناسه دیتابیسی حساب مورد استفاده در سطر گزارش
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// کد کامل حساب مورد استفاده در سطر گزارش
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// نام حساب مورد استفاده در سطر گزارش
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// شماره سطح که عمق حساب به کار رفته در آرتیکل مالی را در ساختار درختی مشخص می کند
        /// </summary>
        public short AccountLevel { get; set; }

        /// <summary>
        /// کد کامل تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        /// نام تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        /// کد کامل مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        /// نام مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// کد کامل پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        /// نام پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// شرح سند 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// مانده ابتدای دوره گزارشگیری
        /// </summary>
        public decimal StartBalance { get; set; }

        /// <summary>
        /// بدهکاری
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// بستانکاری
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        /// مانده انتهای دوره گزارش گیری
        /// </summary>
        public decimal EndBalance { get; set; }

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
