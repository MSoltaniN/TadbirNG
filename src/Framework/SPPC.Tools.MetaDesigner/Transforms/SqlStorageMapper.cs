using System;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Transforms
{
    public class SqlStorageMapper : IStorageMapper
    {
        public string MapPropertyType(BuiltinType type, int length = 0)
        {
            var storageType = String.Empty;
            switch (type)
            {
                case BuiltinType.String:
                    storageType = String.Format("nvarchar({0})", length);
                    break;
                case BuiltinType.Char:
                    storageType = "nchar(1)";
                    break;
                case BuiltinType.Guid:
                    storageType = "uniqueidentifier";
                    break;
                case BuiltinType.Decimal:
                case BuiltinType.Double:
                case BuiltinType.Single:
                    storageType = "float";
                    break;
                case BuiltinType.Boolean:
                    storageType = "bit";
                    break;
                case BuiltinType.DateTime:
                    storageType = "datetime";
                    break;
                case BuiltinType.TimeSpan:
                    storageType = "time(7)";
                    break;
                case BuiltinType.Byte:
                case BuiltinType.SByte:
                    storageType = "tinyint";
                    break;
                case BuiltinType.Int16:
                case BuiltinType.UInt16:
                    storageType = "smallint";
                    break;
                case BuiltinType.Int32:
                case BuiltinType.UInt32:
                    storageType = "int";
                    break;
                case BuiltinType.Int64:
                case BuiltinType.UInt64:
                    storageType = "bigint";
                    break;
                default:
                    break;
            }

            return storageType;
        }
    }
}
