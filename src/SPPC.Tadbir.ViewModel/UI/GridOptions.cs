using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.UI
{
    /// <summary>
    /// گزینه های مختلف برای کنترل سطرهای اطلاعاتی نمایش داده شده در یک نمای جدولی را نگهداری می کند
    /// </summary>
    public class GridOptions
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس می سازد
        /// </summary>
        public GridOptions()
        {
            Filters = new List<GridFilter>();
        }

        /// <summary>
        /// شماره اولین سطر اطلاعاتی در نمای جدولی بر اساس صفحه بندی جاری
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// تعداد سطرهای اطلاعاتی مورد نیاز بر اساس صفحه بندی جاری
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// مجموعه ای از فیلترهای فعال در نمای جدولی اطلاعات
        /// </summary>
        public IList<GridFilter> Filters { get; private set; }

        /// <summary>
        /// اطلاعات مربوط به نحوه مرتب سازی سطرهای داده ای در نمای جدولی
        /// </summary>
        public string Order { get; set; }
    }
}
