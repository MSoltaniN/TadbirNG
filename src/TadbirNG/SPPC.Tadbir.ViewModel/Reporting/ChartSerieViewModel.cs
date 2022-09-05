using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// اطلاعات مورد نیاز برای نمایش یک سری اطلاعات عددی را در یک نمودار نگهداری می کند
    /// </summary>
    public class ChartSerieViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند
        /// </summary>
        public ChartSerieViewModel()
        {
            Data = new List<decimal>();
        }

        /// <summary>
        /// برچسب یا عنوان مورد نظر برای نمایش این سری از اطلاعات در نمودار
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// اطلاعات عددی موجود در این سری
        /// </summary>
        public List<decimal> Data { get; }
    }
}
