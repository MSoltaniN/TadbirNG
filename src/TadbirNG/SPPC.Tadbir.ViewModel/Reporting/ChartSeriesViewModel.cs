using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات مورد نیاز برای نمایش چند سری اطلاعات عددی را در یک نمودار نگهداری می کند
    /// </summary>
    public class ChartSeriesViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ChartSeriesViewModel()
        {
            Labels = new List<string>();
            Datasets = new List<ChartSerieViewModel>();
        }

        /// <summary>
        /// مجموعه برچسب ها - یا عناوین - مورد استفاده در محور اصلی یک نمودار دوبعدی
        /// </summary>
        public List<string> Labels { get; }

        /// <summary>
        /// مجموعه سری های عددی که مقادیر محور تابع نمودار را مشخص می کنند
        /// </summary>
        public List<ChartSerieViewModel> Datasets { get; }
    }
}
