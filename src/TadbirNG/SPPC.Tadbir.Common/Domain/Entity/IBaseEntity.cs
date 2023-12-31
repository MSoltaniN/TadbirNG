﻿using System;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مشخصات مورد نیاز برای موجودیت های پایه ای برنامه را تعریف می کند
    /// </summary>
    public interface IBaseEntity : IFiscalEntity
    {
        /// <summary>
        /// محدوده دسترسی به حساب را در سطح شعبه های موجود در سازمان مشخص می کند. مقادیر مجاز شامل
        /// "کلیه شعبه ها" (مقدار 0)، "شعبه جاری و زیرمجموعه ها" (مقدار 1) و "شعبه جاری" (مقدار 2) می شود.
        /// </summary>
        short BranchScope { get; set; }

        /// <summary>
        ///شناسه کاربر ایجاد کننده
        /// </summary>
        int CreatedById { get; set; }

        /// <summary>
        ///نام و نام خانوادگی کاربر ایجاد کننده
        /// </summary>
        string CreatedByName { get; set; }

        /// <summary>
        ///تاریخ ایجاد
        /// </summary>
        DateTime CreatedDate { get; set; }

        /// <summary>
        ///شناسه آخرین کاربر تغییر دهنده
        /// </summary>
        int ModifiedById { get; set; }

        /// <summary>
        ///نام و نام خانوادگی آخرین کاربر تغییر دهنده
        /// </summary>
        string ModifiedByName { get; set; }
    }
}
