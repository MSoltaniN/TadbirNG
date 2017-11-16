using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت تنظیمات برنامه را تعریف می کند.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// تنظیمات مربوط به گردش های کاری برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مدل نمایشی مربوط به تنظیمات گردش های کاری</returns>
        WorkflowSettingsViewModel GetWorkflowSettings();

        /// <summary>
        /// تغییرات انجام شده روی تنظیمات گردش های کاری را ذخیره می کند
        /// </summary>
        /// <param name="settings"></param>
        void SaveWorkflowSettings(WorkflowSettingsViewModel settings);
    }
}
