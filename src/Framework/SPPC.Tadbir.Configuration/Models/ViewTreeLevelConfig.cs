using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات قابل تعریف برای یکی از سطوح در ساختار اطلاعاتی درختی را نگهداری می کند
    /// </summary>
    public class ViewTreeLevelConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ViewTreeLevelConfig()
        {
            CodeLength = ConfigConstants.DefaultCodeLength;
        }

        /// <summary>
        /// شماره سطح
        /// </summary>
        public int No { get; set; }

        /// <summary>
        /// نام تعریف شده برای سطح
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// طول کد تعریف شده برای سطح
        /// </summary>
        public short CodeLength { get; set; }

        /// <summary>
        /// مشخص می کند که سطح مورد نظر در ساختار درختی فعال شده یا نه
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
