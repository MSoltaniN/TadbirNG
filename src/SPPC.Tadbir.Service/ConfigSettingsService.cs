using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.ViewModel.Settings;

namespace SPPC.Tadbir.Service
{
    public class ConfigSettingsService : ISettingsService
    {
        public ConfigSettingsService(IDomainMapper mapper)
        {
            _mapper = mapper;
        }

        public WorkflowSettingsViewModel GetWorkflowSettings()
        {
            var sectionHandler = new TadbirConfigurationSectionHandler();
            var settings = _mapper.Map<WorkflowSettingsViewModel>(sectionHandler.Section.WorkflowSettings);
            return settings;
        }

        public void SaveWorkflowSettings(WorkflowSettingsViewModel settings)
        {
            var sectionHandler = new TadbirConfigurationSectionHandler();
            foreach (WorkflowElement workflow in sectionHandler.Section.WorkflowSettings.Workflows)
            {
                var workflowSetting = settings.Workflows
                    .Where(wf => wf.Name == workflow.Name)
                    .Single();
                foreach (WorkflowEditionElement edition in workflow.Editions)
                {
                    edition.IsDefault = (edition.Name == workflowSetting.DefaultEdition);
                }
            }
        }

        private IDomainMapper _mapper;
    }
}
