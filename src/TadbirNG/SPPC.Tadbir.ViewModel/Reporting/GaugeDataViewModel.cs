using System;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات یک ویجت از نوع گیج را نگهداری می کند
    /// </summary>
    public class GaugeDataViewModel
    {
        /// <summary>
        /// مقدار جاری برای نمایش در گیج
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// کمترین مقدار برای نمایش در گیج
        /// </summary>
        public decimal MinValue { get; set; }

        /// <summary>
        /// بیشترین مقدار برای نمایش در گیج
        /// </summary>
        public decimal MaxValue { get; set; }
    }
}
