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
    }
}
