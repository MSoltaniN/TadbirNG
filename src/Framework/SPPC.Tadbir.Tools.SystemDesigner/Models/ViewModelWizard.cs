using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ViewModelWizard
    {
        public ViewModelWizard()
        {
            SysViewModel = new SysVeiwModel();
            ViewModel = new ViewModelClass();
            ColumnViewModel = new List<ColumnViewModel>();
            ActiveColumns = new List<bool>();
        }

        public SysVeiwModel SysViewModel { get; set; }

        public ViewModelClass ViewModel { get; set; }
        public List<ColumnViewModel> ColumnViewModel { get; set; }
        public List<bool> ActiveColumns { get; set; }
    }
}
