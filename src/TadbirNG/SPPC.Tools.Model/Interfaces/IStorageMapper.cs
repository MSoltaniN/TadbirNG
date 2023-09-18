using SPPC.Framework.Common;

namespace SPPC.Tools.Transforms
{
    public interface IStorageMapper
    {
        string MapPropertyType(BuiltinType type, int length);
    }
}
