using BabakSoft.Platform.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class SysVeiwModel
    {
        public SysVeiwModel()
        {
            SysModelViewItemIndex = -1;
        }   
        public int SysModelViewItemIndex { get; set; }
        public DataTable SysModelViewItems { get; set; }
     
    }
}
