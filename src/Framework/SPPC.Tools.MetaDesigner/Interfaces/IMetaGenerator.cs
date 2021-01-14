using System;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Transforms
{
    public interface IMetaGenerator
    {
        Repository GenerateFileRepository(string name, string path);
        Entity GenerateEntity(string name, Repository repository);
        Property GenerateProperty(string name, BuiltinType type, int length);
        object GenerateDefaultItem(Type itemType);
    }
}
