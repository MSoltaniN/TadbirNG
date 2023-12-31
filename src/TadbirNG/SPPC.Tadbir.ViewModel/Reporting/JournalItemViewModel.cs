﻿using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعاتی نمایشی یک آرتیکل مالی را جهت استفاده در گزارش دفتر روزنامه نگهداری می کند
    /// </summary>
    public class JournalItemViewModel : ViewModelBase, IAccountView
    {
        /// <summary>
        /// تاریخ سند مالی
        /// </summary>
        public DateTime VoucherDate { get; set; }

        /// <summary>
        /// شماره سند مالی، ستون شماره در نمای لیستی
        /// </summary>
        public int VoucherNo { get; set; }

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
        /// کد کامل سرفصل حسابداری مورد استفاده در ردیف سند، ستون شماره حساب در نمای لیستی
        /// </summary>
        public string AccountFullCode { get; set; }

        /// <summary>
        /// نام سرفصل حسابداری مورد استفاده در ردیف سند، ستون عنوان حساب در نمای لیستی
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// شماره سطح سرفصل حسابداری در ساختار درختی
        /// </summary>
        public short AccountLevel { get; set; }

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
    }
}
