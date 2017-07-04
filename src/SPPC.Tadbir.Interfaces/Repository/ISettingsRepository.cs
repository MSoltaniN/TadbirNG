using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Repository
{
    /// <summary>
    /// عملیات مربوط به ذخیره و بازیابی تنظیمات برنامه را تعریف می کند.
    /// </summary>
    public interface ISettingsRepository
    {
        /// <summary>
        /// تنظیمات جاری مربوط به گردش های کاری را بازیابی کرده و برمی گرداند
        /// </summary>
        /// <returns>مدل نمایشی مربوط به تنظیمات گردش های کاری</returns>
        WorkflowSettingsViewModel GetWorkflowSettings();

        /// <summary>
        /// ویرایش پیش فرض گردش کاری با نام مشخص شده را بازیابی کرده و برمی گرداند 
        /// </summary>
        /// <param name="workflowName">نام یکی از گردش های کاری موجود</param>
        /// <returns>مدل نمایشی مربوط به ویرایش پیش فرض از گردش کاری مشخص شده</returns>
        WorkflowEditionViewModel GetDefaultWorkflowEdition(string workflowName);

        /// <summary>
        /// تغییرات ایجادشده در تنظیمات گردش های کاری را ذخیره می کند
        /// </summary>
        /// <param name="settings">مدل نمایشی مربوط به تنظیمات گردش های کاری با آخرین تغییرات</param>
        void SaveWorkflowSettings(WorkflowSettingsViewModel settings);
    }
}
