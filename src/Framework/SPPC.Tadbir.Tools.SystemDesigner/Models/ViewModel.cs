using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ViewModelClass
    {
        public string Name { get; set; }
        public string FetchUrl { get; set; }
        public bool IsHierarchy { get; set; }
        public bool IsCartableIntegrated { get; set; }
        public string EntityType { get; set; }
        public string SearchUrl { get; set; }

        public string SelectedViewModelOnTreeView { get; set; }

    }
}
