using System;
using System.Collections.Generic;
using System.Data;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ViewWizardModel
    {
        public ViewWizardModel()
        {
            View = new ViewViewModel();
            Columns = new List<ColumnViewModel>();
            SelectedViewModelOnTreeView = "View Models";
        }

        public ViewViewModel View { get; set; }

        public List<ColumnViewModel> Columns { get; private set; }

        public DataTable ViewItems { get; set; }

        public string SelectedViewModelOnTreeView { get; set; }
    }
}
