using System;
using System.Collections.Generic;
using SPPC.Framework.Service;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Service
{
    public class ConfigSettingsService : ISettingsService
    {
        public ConfigSettingsService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public WorkflowSettingsViewModel GetWorkflowSettings()
        {
            var settings = _apiClient.Get<WorkflowSettingsViewModel>(SettingsApi.WorkflowSettings);
            return settings;
        }

        public void SaveWorkflowSettings(WorkflowSettingsViewModel settings)
        {
            _apiClient.Update(settings, SettingsApi.WorkflowSettings);
        }

        private IApiClient _apiClient;
    }
}
