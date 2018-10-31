using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات مورد نیاز برای نمایش یک نمودار ستونی در داشبورد را نگداری می کند
    /// </summary>
    public class DashboardChartSeriesViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public DashboardChartSeriesViewModel()
        {
            Points = new List<DashboardChartPointViewModel>();
        }

        /// <summary>
        /// عنوان اصلی نمودار ستونی
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// عنوان مورد استفاده در راهنمای نمودار ستونی
        /// </summary>
        public string Legend { get; set; }

        /// <summary>
        /// مجموعه ای از نقاط قابل نمایش در نمودار ستونی
        /// </summary>
        public IList<DashboardChartPointViewModel> Points { get; }
    }
}
