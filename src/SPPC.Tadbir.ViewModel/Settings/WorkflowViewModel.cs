using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Settings
{
    public class WorkflowViewModel
    {
        public WorkflowViewModel()
        {
            Editions = new List<WorkflowEditionViewModel>();
        }

        public string Name { get; set; }

        public string LocalName { get; set; }

        public string DefaultEdition { get; set; }

        public IList<WorkflowEditionViewModel> Editions { get; private set; }
    }
}
