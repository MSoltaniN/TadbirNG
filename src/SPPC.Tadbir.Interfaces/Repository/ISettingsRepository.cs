using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Repository
{
    public interface ISettingsRepository
    {
        WorkflowSettingsViewModel GetWorkflowSettings();

        WorkflowEditionViewModel GetDefaultWorkflowEdition(string workflowName);

        void SaveWorkflowSettings(WorkflowSettingsViewModel settings);
    }
}
