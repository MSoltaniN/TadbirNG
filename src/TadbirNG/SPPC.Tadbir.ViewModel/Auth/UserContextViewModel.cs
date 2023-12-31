﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// اطلاعات کامل محیطی را به همراه مجوزهای امنیتی برای یک کاربر نگهداری می کند
    /// </summary>
    public class UserContextViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public UserContextViewModel()
        {
            Roles = new List<int>();
            Permissions = new List<PermissionBriefViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی این کاربر
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام کاربری مورد استفاده برای این کاربر
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// نام و نام خانوادگی این کاربر
        /// </summary>
        public string PersonFullName { get; set; }

        /// <summary>
        /// اطلاعات مورد نیاز برای اتصال به دیتابیس شرکت جاری
        /// </summary>
        public string Connection { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شرکتی که کاربر به آن وارد شده
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای از شرکت جاری که کاربر به آن وارد شده
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی از شرکت جاری که کاربر به آن وارد شده
        /// </summary>
        public int FiscalPeriodId { get; set; }

        /// <summary>
        /// نام شرکتی که کاربر به آن وارد شده
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// نام شعبه ای از شرکت جاری که کاربر به آن وارد شده
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// نام دوره مالی از شرکت جاری که کاربر به آن وارد شده
        /// </summary>
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// نوع سیستم ثبت موجودی در دوره مالی جاری
        /// </summary>
        public int InventoryMode { get; set; }

        /// <summary>
        /// تاریخ و ساعت آخرین ورود کاربر به برنامه
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// زبان جاری برنامه که موقع ورود به برنامه انتخاب شده است
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// مجموعه شناسه های دیتابیسی نقش های تخصیص یافته به این کاربر
        /// </summary>
        public IList<int> Roles { get; }

        /// <summary>
        /// مجموعه مجوزهای امنیتی اعطا شده به این کاربر
        /// </summary>
        public IList<PermissionBriefViewModel> Permissions { get; private set; }
    }
}
