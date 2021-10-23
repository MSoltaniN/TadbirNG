using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public enum GeneratorOutputType
    {
        None,
        Poco,
        ViewModel,
        FluentMapping,
        MappingTest,
        XmlMapping,
        CreateTable,
        Repository,
        DummyFactory
    }

    public enum RepositoryCommandType
    {
        New,
        Open,
        Save,
        SaveAs
    }
}
