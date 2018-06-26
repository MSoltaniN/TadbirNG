using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// گزینه های موجود برای نحوه خواندن و اعمال تنظیمات جداکننده را تعریف می کند
    /// </summary>
    public sealed class SeparatorModeOptions
    {
        private SeparatorModeOptions()
        {
        }

        /// <summary>
        /// استفاده از تنظیمات پیش فرض تنظیم شده در سیستم عامل
        /// </summary>
        public const string UseDefault = "UseDefault";

        /// <summary>
        /// استفاده از تنظیمات خاص برنامه
        /// </summary>
        public const string UseCustom = "UseCustom";
    }
}
