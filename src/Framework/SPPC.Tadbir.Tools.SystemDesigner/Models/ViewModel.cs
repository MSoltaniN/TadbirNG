using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ViewModelEntityModel
    {
        public ViewModelEntityModel()
        {
            Columns = new List<ColumnViewModel>();
            ActiveColumns = new List<bool>();
        }
      
        public string Name { get; set; }
        public string FetchUrl { get; set; }
        public bool IsHierarchy { get; set; }
        public bool IsCartableIntegrated { get; set; }
        public string EntityType { get; set; }
        public string SearchUrl { get; set; }
        public List<ColumnViewModel> Columns { get; set; }
        public List<bool> ActiveColumns { get; set; }
    }
}
