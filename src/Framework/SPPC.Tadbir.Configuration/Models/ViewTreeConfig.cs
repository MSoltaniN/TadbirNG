﻿using System;
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

        /// <summary>
        /// یک نمونه جدید را با تنظیمات سیستمی پیش فرض مقداردهی می کند
        /// </summary>
        public void InitDefaultLevels()
        {
            Array.ForEach(
                Enumerable.Range(1, ConfigConstants.MaxTreeDepth).ToArray(),
                no => Levels.Add(null));
            Array.ForEach(
                Enumerable.Range(1, ConfigConstants.DefaultTreeDepth).ToArray(),
                no => Levels[no - 1] = new ViewTreeLevelConfig() { No = no });
        }
    }
}
