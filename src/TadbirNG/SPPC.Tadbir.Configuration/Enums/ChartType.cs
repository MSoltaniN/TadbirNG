using System;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// انواع نمودارهای مورد استفاده در داشبورد را تعریف می کند
    /// </summary>
    public sealed class ChartType
    {
        private ChartType()
        {
        }

        /// <summary>
        /// نمودار ستونی
        /// </summary>
        public const string ColumnChart = "ColumnChart";

        /// <summary>
        /// نمودار میله ای
        /// </summary>
        public const string BarChart = "BarChart";

        /// <summary>
        /// نمودار خطی
        /// </summary>
        public const string LineChart = "LineChart";

        /// <summary>
        /// نمودار کیکی
        /// </summary>
        public const string PieChart = "PieChart";
    }
}
