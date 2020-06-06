using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ViewModelWizard
    {
        public ViewModelWizard()
        {
            ViewModel = new ViewModelEntityModel();
            SysModelViewItemIndex = -1;
            SelectedViewModelOnTreeView = "View Models";
        }
        public ViewModelEntityModel ViewModel { get; set; }
       //SysViewModel control
        public int SysModelViewItemIndex { get; set; }
        public DataTable SysModelViewItems { get; set; }
        //ViewModel control
        public string SelectedViewModelOnTreeView { get; set; }
        //ColumnViewModel control        
    }
}
