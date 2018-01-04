using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// عملیات مربوط به ذخیره و بازیابی تنظیمات برنامه را با استفاده از فایل پیکربندی انجام می دهد.
    /// </summary>
    public class ConfigSettingsRepository : ISettingsRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="mapper">پیاده سازی اینترفیس مورد نیاز برای نگاشت مدل های داده ای</param>
        public ConfigSettingsRepository(IDomainMapper mapper)
        {
            _sectionHandler = new TadbirConfigurationSectionHandler();
            _mapper = mapper;
        }

        /// <summary>
        /// تنظیمات جاری مربوط به گردش های کاری را از فایل پیکربندی بازیابی کرده و برمی گرداند
        /// </summary>
        /// <returns>مدل نمایشی مربوط به تنظیمات گردش های کاری</returns>
        public WorkflowSettingsViewModel GetWorkflowSettings()
        {
            var settings = _mapper.Map<WorkflowSettingsViewModel>(_sectionHandler.Section.WorkflowSettings);
            return settings;
        }

        /// <summary>
        /// ویرایش پیش فرض گردش کاری با نام مشخص شده را از فایل پیکربندی بازیابی کرده و برمی گرداند 
        /// </summary>
        /// <param name="workflowName">نام یکی از گردش های کاری موجود</param>
        /// <returns>مدل نمایشی مربوط به ویرایش پیش فرض از گردش کاری مشخص شده</returns>
        public WorkflowEditionViewModel GetDefaultWorkflowEdition(string workflowName)
        {
            var workflow = _sectionHandler.Section.WorkflowSettings.Workflows[workflowName];
            var edition = workflow.Editions[workflow.Editions.DefaultEdition];
            return _mapper.Map<WorkflowEditionViewModel>(edition);
        }

        /// <summary>
        /// تغییرات ایجادشده در تنظیمات گردش های کاری را در فایل پیکربندی ذخیره می کند
        /// </summary>
        /// <param name="settings">مدل نمایشی مربوط به تنظیمات گردش های کاری با آخرین تغییرات</param>
        public void SaveWorkflowSettings(WorkflowSettingsViewModel settings)
        {
            Verify.ArgumentNotNull(settings, "settings");
            foreach (WorkflowElement workflow in _sectionHandler.Section.WorkflowSettings.Workflows)
            {
                var workflowSetting = settings.Workflows
                    .Where(wf => wf.Name == workflow.Name)
                    .Single();
                foreach (WorkflowEditionElement edition in workflow.Editions)
                {
                    edition.IsDefault = (edition.Name == workflowSetting.DefaultEdition);
                }
            }

            _sectionHandler.Save();
        }

        private TadbirConfigurationSectionHandler _sectionHandler;
        private IDomainMapper _mapper;
    }
}
