using System;
using System.Collections.Generic;
using System.Linq;

namespace SPPC.Tadbir.Configuration.Models
{
    /// <summary>
    /// تنظیمات مربوط به سطوح مختلف یک ساختار اطلاعاتی درختی (سلسله مراتبی) را نگهداری می کند
    /// </summary>
    public class ViewTreeConfig
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public ViewTreeConfig()
        {
            MaxDepth = ConfigConstants.DefaultTreeDepth;
            Levels = new List<ViewTreeLevelConfig>();
        }

        /// <summary>
        /// شناسه دیتابیسی مدل نمایشی مورد استفاده در ساختار درختی
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// حداکثر عمق تنظیم شده برای سطوح ساختار درختی
        /// </summary>
        public short MaxDepth { get; set; }

        /// <summary>
        /// مجموعه ای از تنظیمات سطوح قابل تعریف در ساختار درختی
        /// </summary>
        public IList<ViewTreeLevelConfig> Levels { get; protected set; }
    }
}
