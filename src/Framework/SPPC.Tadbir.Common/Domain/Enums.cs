using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مقادیر تعریف شده برای محدوده دسترسی به موجودیت ها در سطح شعبه های سازمانی را تعریف می کند
    /// </summary>
    public enum BranchScope
    {
        /// <summary>
        /// دسترسی در سطح کلیه شعب
        /// </summary>
        AllBranches = 0,

        /// <summary>
        /// دسترسی در سطح شعبه جاری و تمام شعب زیرمجموعه
        /// </summary>
        CurrentBranchAndChildren = 1,

        /// <summary>
        /// دسترسی در سطح شعبه جاری
        /// </summary>
        CurrentBranch = 2
    }

    /// <summary>
    /// نوع انتخاب حساب برای اضافه کردن در مجموعه حساب
    /// </summary>
    public enum TypeLevel
    {
        /// <summary>
        /// تمام حساب ها
        /// </summary>
        AllAccounts = 0,

        /// <summary>
        /// حساب های در سطح آخر
        /// </summary>
        LeafAccounts = 1,

        /// <summary>
        /// حساب های غیر از سطح آخر
        /// </summary>
        NonLeafAccounts = 2
    }

    /// <summary>
    /// انواع سیستمی اسناد را تعریف می کند
    /// </summary>
    public enum VoucherType
    {
        /// <summary>
        /// نوع سیستمی سند عادی
        /// </summary>
        NormalVoucher = 0,
    }

    /// <summary>
    /// انواع مفهومی سند مالی را تعریف می کند
    /// </summary>
    public enum SubjectType
    {
        /// <summary>
        /// نوع مفهومی سند حسابداری
        /// </summary>
        Accounting = 0,

        /// <summary>
        /// نوع مفهومی سند پیش نویس
        /// </summary>
        Draft = 1,

        /// <summary>
        /// نوع مفهومی سند بودجه
        /// </summary>
        Budgeting = 2
    }

    /// <summary>
    /// داده شمارشی برای انواع آرتیکل های سند مالی
    /// </summary>
    public enum VoucherLineType
    {
        /// <summary>
        /// آرتیکل عادی
        /// </summary>
        NormalLine = 0,

        /// <summary>
        /// آرتیکل مالیات و عوارض
        /// </summary>
        TaxAndToll = 1,

        /// <summary>
        /// آرتیکل اصلاحی
        /// </summary>
        Revised = 2
    }

    /// <summary>
    /// حالات ممکن برای محدودیت ثبت مالی را در سرفصل های حسابداری تعریف می کند
    /// </summary>
    public enum TurnoverMode : short
    {
        /// <summary>
        /// نداشتن محدودیت برای ثبت مالی
        /// </summary>
        Unlimited = -1,

        /// <summary>
        /// محدودیت بدهکار در طول دوره
        /// </summary>
        DebtorDuringPeriod = 0,

        /// <summary>
        /// محدودیت بستانکار در طول دوره
        /// </summary>
        CreditorDuringPeriod = 1,

        /// <summary>
        /// محدودیت بدهکار در انتهای دوره
        /// </summary>
        DebtorEndPeriod = 2,

        /// <summary>
        /// محدودیت بستانکار در انتهای دوره
        /// </summary>
        CreditorEndPeriod = 3
    }
}
