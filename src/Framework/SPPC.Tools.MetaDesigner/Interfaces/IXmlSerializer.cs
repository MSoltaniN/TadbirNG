using System;
using System.Xml.Linq;

namespace SPPC.Tools.MetaDesigner.Common
{
    public interface IXmlSerializer
    {
        XElement Serialize(object item);
        void Serialize(string path, object item);
        object Deserialize(XElement xItem, Type itemType);
        object Deserialize(string path, Type itemType);
    }
}
