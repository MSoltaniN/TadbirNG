using System;
using SPPC.Framework.Presentation;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// اطلاعات پارامترهای مورد نیاز در گزارش مانده به تفکیک حساب را نگهداری می کند
    /// </summary>
    public class BalanceByAccountParameters
    {
        /// <summary>
        /// شناسه مولفه حساب مورد نظر
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// مشخص میکند که آیا گزارش بر اساس تاریخ است یا خیر؟
        /// اگر گزارش بر اساس تاریخ نبود یعنی بر اساس شماره سند است
        /// </summary>
        public bool IsByDate { get; set; }

        /// <summary>
        /// تاریخ شروع دوره گزارش گیری در حالت گزارش بر اساس تاریخ
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// تاریخ پایان دوره گزارش گیری در حالت گزارش بر اساس تاریخ
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// اولین سند در حالت گزارش بر اساس سند
        /// </summary>
        public int? FromNo { get; set; }

        /// <summary>
        /// آخرین سند در حالت گزارش بر اساس سند
        /// </summary>
        public int? ToNo { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش گیری باید به تفکیک شعبه انجام شود یا نه؟
        /// </summary>
        public bool IsByBranch { get; set; }

        /// <summary>
        /// در صورتی که گزارش بر اساس حساب نباشد تفکیک بر اساس حساب انجام میشود
        /// </summary>
        public bool IsSelectedAccount { get; set; }

        /// <summary>
        /// سطح حساب انتخاب شده
        /// </summary>
        public int? AccountLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی حساب انتخاب شده
        /// </summary>
        public int? AccountId { get; set; }

        /// <summary>
        /// در صورتی که گزارش بر اساس تفصیلی شناور نباشد تفکیک بر اساس تفصیلی شناور انجام میشود
        /// </summary>
        public bool IsSelectedDetailAccount { get; set; }

        /// <summary>
        /// سطح تفصیلی شناور انتخاب شده
        /// </summary>
        public int? DetailAccountLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی تفصیلی انتخاب شده
        /// </summary>
        public int? DetailAccountId { get; set; }

        /// <summary>
        /// در صورتی که گزارش بر اساس مرکز هزینه نباشد تفکیک بر اساس مرکز هزینه انجام میشود
        /// </summary>
        public bool IsSelectedCostCenter { get; set; }

        /// <summary>
        /// سطح مرکز هزینه انتخاب شده
        /// </summary>
        public int? CostCenterLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مرکز هزینه انتخاب شده
        /// </summary>
        public int? CostCenterId { get; set; }

        /// <summary>
        /// در صورتی که گزارش بر اساس پروژه نباشد تفکیک بر اساس پروژه انجام میشود
        /// </summary>
        public bool IsSelectedProject { get; set; }

        /// <summary>
        /// سطح پروژه انتخاب شده
        /// </summary>
        public int? ProjectLevel { get; set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه انتخاب شده
        /// </summary>
        public int? ProjectId { get; set; }

        /// <summary>
        /// عدد مربوط به انتخاب گزینه های عملیاتی مورد نیاز در گزارش
        /// </summary>
        public int? Options { get; set; }

        /// <summary>
        /// گزینه های مختلف برای کنترل نمایش سطرهای اطلاعاتی در یک نمای جدولی را نگهداری می کند
        /// </summary>
        public GridOptions GridOptions { get; set; }
    }
}
