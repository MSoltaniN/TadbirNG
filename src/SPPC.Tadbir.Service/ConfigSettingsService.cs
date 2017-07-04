using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت تنظیمات برنامه را با استفاده از فایل پیکربندی انجام می دهد.
    /// </summary>
    public class ConfigSettingsService : ISettingsService
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="apiClient">پیاده سازی اینترفیس مربوط به کار با سرویس</param>
        public ConfigSettingsService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// تنظیمات مربوط به گردش های کاری برنامه را از فایل پیکربندی خوانده و برمی گرداند
        /// </summary>
        /// <returns>مدل نمایشی مربوط به تنظیمات گردش های کاری</returns>
        public WorkflowSettingsViewModel GetWorkflowSettings()
        {
            var settings = _apiClient.Get<WorkflowSettingsViewModel>(SettingsApi.WorkflowSettings);
            return settings;
        }

        /// <summary>
        /// تغییرات انجام شده روی تنظیمات گردش های کاری را در فایل پیکربندی ذخیره می کند
        /// </summary>
        /// <param name="settings"></param>
        public void SaveWorkflowSettings(WorkflowSettingsViewModel settings)
        {
            _apiClient.Update(settings, SettingsApi.WorkflowSettings);
        }

        private IApiClient _apiClient;
    }
}
