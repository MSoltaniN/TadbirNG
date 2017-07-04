using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.ViewModel.Settings;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Service
{
    public class ConfigSettingsRepository : ISettingsRepository
    {
        public ConfigSettingsRepository(IDomainMapper mapper)
        {
            _sectionHandler = new TadbirConfigurationSectionHandler();
            _mapper = mapper;
        }

        public WorkflowSettingsViewModel GetWorkflowSettings()
        {
            var settings = _mapper.Map<WorkflowSettingsViewModel>(_sectionHandler.Section.WorkflowSettings);
            return settings;
        }

        public WorkflowEditionViewModel GetDefaultWorkflowEdition(string workflowName)
        {
            var workflow = _sectionHandler.Section.WorkflowSettings.Workflows[workflowName];
            var edition = workflow.Editions[workflow.Editions.DefaultEdition];
            return _mapper.Map<WorkflowEditionViewModel>(edition);
        }

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
