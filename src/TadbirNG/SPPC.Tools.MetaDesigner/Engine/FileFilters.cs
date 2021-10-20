using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class FileFilters
    {
        public static string All
        {
            get { return "All Files (*.*)|*.*"; }
        }

        public static string XmlRepository
        {
            get { return "XML Repository Files (*.xml)|*.xml"; }
        }
    }
}
