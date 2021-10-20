namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// اطلاعات پارامترهای مورد نیاز در گزارش مانده به تفکیک حساب را نگهداری می کند
    /// </summary>
    public class BalanceByAccountParameters : ReportParameters
    {
        /// <summary>
        /// شناسه مولفه حساب مورد نظر
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// مشخص می کند که تفکیک بر اساس سرفصل های حسابداری مورد نیاز است یا نه
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
        /// مشخص می کند که تفکیک بر اساس تفصیلی های شناور مورد نیاز است یا نه
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
        /// مشخص می کند که تفکیک بر اساس مراکز هزینه مورد نیاز است یا نه
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
        /// مشخص می کند که تفکیک بر اساس پروژه ها مورد نیاز است یا نه
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
    }
}
