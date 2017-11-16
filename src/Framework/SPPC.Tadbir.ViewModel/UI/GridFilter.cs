using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.UI
{
    /// <summary>
    /// اطلاعات مورد نیاز برای اعمال فیلتر روی یک فیلد اطلاعاتی را نگهداری می کند
    /// </summary>
    public class GridFilter
    {
        /// <summary>
        /// نام فیلد اطلاعاتی مورد استفاده در فیلتر
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// مقدار مورد استفاده برای فیلتر کردن مقادیر فیلد اطلاعاتی
        /// </summary>
        public string Value { get; set; }
    }
}
