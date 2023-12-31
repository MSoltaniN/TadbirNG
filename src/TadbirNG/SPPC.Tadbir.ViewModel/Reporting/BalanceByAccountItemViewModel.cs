﻿using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یکی از سطرهای گزارش مانده به تفکیک حساب را نگهداری می کند
    /// </summary>
    public class BalanceByAccountItemViewModel : ViewModelBase, IAccountView
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public BalanceByAccountItemViewModel()
        {
        }

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
        /// شناسه دیتابیسی تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public int DetailAccountId { get; set; }

        /// <summary>
        /// کد کامل تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public string DetailAccountFullCode { get; set; }

        /// <summary>
        /// نام تفصیلی شناور مورد استفاده در سطر گزارش
        /// </summary>
        public string DetailAccountName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public int CostCenterId { get; set; }

        /// <summary>
        /// کد کامل مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public string CostCenterFullCode { get; set; }

        /// <summary>
        /// نام مرکز هزینه مورد استفاده در سطر گزارش
        /// </summary>
        public string CostCenterName { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// کد کامل پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public string ProjectFullCode { get; set; }

        /// <summary>
        /// نام پروژه مورد استفاده در سطر گزارش
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// شرح حساب
        /// </summary>
        public string AccountDescription { get; set; }

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
    }
}
