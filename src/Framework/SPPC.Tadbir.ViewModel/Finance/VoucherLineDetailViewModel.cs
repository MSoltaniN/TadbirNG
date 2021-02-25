using System;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات مفصل مورد نیاز برای یک آرتیکل مالی را نگهداری می کند
    /// </summary>
    public class VoucherLineDetailViewModel : ViewModelBase, IFiscalEntity
    {
        /// <summary>
        /// شناسه دیتابیسی آرتیکل مالی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// تاریخ سندی که آرتیکل مالی در آن ایجاد شده است
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// شماره سندی که آرتیکل مالی در آن ایجاد شده است
        /// </summary>
        public int VoucherNo { get; set; }

        /// <summary>
        /// رفرنس سندی که آرتیکل مالی در آن ایجاد شده است
        /// </summary>
        public string VoucherReference { get; set; }

        /// <summary>
        /// شناسه دیتابیسی وضعیت ثبتی سندی که آرتیکل مالی در آن ایجاد شده است
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
        /// مبلغ بدهکار آرتیکل مالی
        /// </summary>
        public decimal Debit { get; set; }

        /// <summary>
        /// مبلغ بستانکار آرتیکل مالی
        /// </summary>
        public decimal Credit { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Mark { get; set; }

        /// <summary>
        /// شرح آرتیکل مالی
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// شناسه دیتابیسی سرفصل حسابداری مورد استفاده در آرتیکل مالی
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// کد کامل سرفصل حسابداری مورد استفاده در آرتیکل مالی
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// نام سرفصل حسابداری مورد استفاده در آرتیکل مالی
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public short AccountLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تفصیلی شناور مورد استفاده در آرتیکل مالی
        /// </summary>
        public int DetailAccountId { get; set; }

        /// <summary>
        /// کد کامل تفصیلی شناور مورد استفاده در آرتیکل مالی
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        /// نام تفصیلی شناور مورد استفاده در آرتیکل مالی
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public short DetailAccountLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه مورد استفاده در آرتیکل مالی
        /// </summary>
        public int CostCenterId { get; set; }

        /// <summary>
        /// کد کامل مرکز هزینه مورد استفاده در آرتیکل مالی
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        /// نام مرکز هزینه مورد استفاده در آرتیکل مالی
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public short CostCenterLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه مورد استفاده در آرتیکل مالی
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// کد کامل پروژه مورد استفاده در آرتیکل مالی
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        /// نام پروژه مورد استفاده در آرتیکل مالی
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        ///
        /// </summary>
        public short ProjectLevel { get; set; }

        /// <summary>
        /// نام ارز مورد استفاده در آرتیکل مالی
        /// </summary>
        public string CurrencyName { get; set; }

        /// <summary>
        /// مبلغ ارزی مورد استفاده در آرتیکل مالی
        /// </summary>
        public decimal CurrencyValue { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ایجادکننده آرتیکل سند
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی که آرتیکل سند در آن ایجاد شده است
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public DateTime ModifiedDate { get; set; }
    }
}
