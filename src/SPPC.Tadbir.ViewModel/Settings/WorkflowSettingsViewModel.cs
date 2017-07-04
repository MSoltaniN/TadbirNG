using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Settings
{
    /// <summary>
    /// این مدل نمایشی تنظیمات مربوط به گردش های کاری برنامه را نمایش می دهد.
    /// </summary>
    public class WorkflowSettingsViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public WorkflowSettingsViewModel()
        {
            Workflows = new List<WorkflowViewModel>();
        }

        /// <summary>
        /// مجموعه ای از تنظیمات گردش های کاری موجود
        /// </summary>
        public IList<WorkflowViewModel> Workflows { get; private set; }
    }
}
