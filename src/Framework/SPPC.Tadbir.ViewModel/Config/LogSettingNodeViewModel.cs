using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Config
{
    /// <summary>
    /// اطلاعات یکی از شاخه های نمای درختی را در فرم تنظیمات لاگ نگهداری می کند
    /// </summary>
    public class LogSettingNodeViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public LogSettingNodeViewModel()
        {
            Items = new List<LogSettingItemViewModel>();
        }

        /// <summary>
        /// شناسه یکتای تولید شده برای شاخه نمای درختی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// شناسه شاخه والد برای شاخه جاری در نمای درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// نام محلی شده که در شاخه نمای درختی نمایش داده می شود
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// مجموعه تنظیمات جاری برای عملیات یکی از فرم ها یا موجودیت ها
        /// </summary>
        public List<LogSettingItemViewModel> Items { get; }
    }
}
