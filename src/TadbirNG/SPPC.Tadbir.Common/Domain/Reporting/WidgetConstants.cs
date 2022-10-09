using System;

namespace SPPC.Tadbir.Domain.Reporting
{
    /// <summary>
    /// مقادیر ثابت مورد استفاده در ویجت ها را تعریف می کند
    /// </summary>
    public class WidgetConstants
    {
        private WidgetConstants()
        {
        }

        /// <summary>
        /// کمترین مقدار پیش فرض برای نمایش در گیج
        /// </summary>
        public const decimal GaugeMinValue = 0.0M;

        /// <summary>
        /// بیشترین مقدار پیش فرض برای نمایش در گیج
        /// </summary>
        public const decimal GaugeMaxValue = 10.0M;
    }
}
