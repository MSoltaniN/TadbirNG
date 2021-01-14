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
                case BuiltinType.Text:
                    storageType = String.Format("nvarchar({0})", length);
                    break;
                case BuiltinType.Character:
                    storageType = "nchar(1)";
                    break;
                case BuiltinType.SystemGuid:
                    storageType = "uniqueidentifier";
                    break;
                case BuiltinType.DecimalNumber:
                case BuiltinType.DoublePrecision:
                case BuiltinType.SinglePrecision:
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
                case BuiltinType.TinyNumber:
                case BuiltinType.SignedTinyNumber:
                    storageType = "tinyint";
                    break;
                case BuiltinType.SmallNumber:
                case BuiltinType.UnsignedSmallNumber:
                    storageType = "smallint";
                    break;
                case BuiltinType.Number:
                case BuiltinType.UnsignedNumber:
                    storageType = "int";
                    break;
                case BuiltinType.BigNumber:
                case BuiltinType.UnsignedBigNumber:
                    storageType = "bigint";
                    break;
                default:
                    break;
            }

            return storageType;
        }
    }
}
