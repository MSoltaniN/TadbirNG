using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Service
{
    public interface ISettingsService
    {
        WorkflowSettingsViewModel GetWorkflowSettings();

        void SaveWorkflowSettings(WorkflowSettingsViewModel settings);
    }
}
