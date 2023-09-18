using System;

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

        public static string JsonRepository
        {
            get { return "JSON Repository Files (*.json)|*.json"; }
        }
    }
}
