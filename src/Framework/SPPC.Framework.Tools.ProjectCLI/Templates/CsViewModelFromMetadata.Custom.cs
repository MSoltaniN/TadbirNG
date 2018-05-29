using System;
using System.Reflection;
using BabakSoft.Platform.Metadata;
using SPPC.Framework.Common;

namespace SPPC.Framework.Tools.ProjectCLI.Templates
{
    public partial class CsViewModelFromMetadata
    {
        public CsViewModelFromMetadata(Entity entity)
        {
            Verify.ArgumentNotNull(entity);
            _entity = entity;
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private string GetTypeAlias(Property property)
        {
            string builtinTypeName = property.Type.ToString();
            var alias = String.Empty;
            switch (builtinTypeName)
            {
                case "String":
                case "Char":
                case "Byte":
                case "SByte":
                case "Decimal":
                case "Double":
                    alias = builtinTypeName.ToLower();
                    break;
                case "Int16":
                    alias = "short";
                    break;
                case "UInt16":
                    alias = "ushort";
                    break;
                case "Int32":
                    alias = "int";
                    break;
                case "UInt32":
                    alias = "uint";
                    break;
                case "Int64":
                    alias = "long";
                    break;
                case "UInt64":
                    alias = "ulong";
                    break;
                case "Single":
                    alias = "float";
                    break;
                case "Boolean":
                    alias = "bool";
                    break;
                default:
                    alias = builtinTypeName;
                    break;
            }

            // NOTE: Nullable is especially used for DateTime, because the default value (0001-01-01) is unacceptable
            // for SQL Server. For all other common value types, the default value (0) is perfectly valid to use.
            if (builtinTypeName == "DateTime" && property.Storage.Nullable)
            {
                alias = "DateTime?";
            }

            return alias;
        }

        private bool HasMinLengthRestrictionOnly(Property property)
        {
            var rule = property.ValidationRule;
            return (rule != null
                && rule.Type == ValidationRuleType.Length
                && String.IsNullOrWhiteSpace(rule.Maximum)
                && !String.IsNullOrWhiteSpace(rule.Minimum));
        }

        private bool HasMaxLengthRestrictionOnly(Property property)
        {
            var rule = property.ValidationRule;
            return (rule != null
                && rule.Type == ValidationRuleType.Length
                && !String.IsNullOrWhiteSpace(rule.Maximum)
                && String.IsNullOrWhiteSpace(rule.Minimum));
        }

        private bool HasLengthRangeRestriction(Property property)
        {
            var rule = property.ValidationRule;
            return (rule != null
                && rule.Type == ValidationRuleType.Length
                && !String.IsNullOrWhiteSpace(rule.Maximum)
                && !String.IsNullOrWhiteSpace(rule.Minimum));
        }

        private bool HasRangeRestriction(Property property)
        {
            var rule = property.ValidationRule;
            return (rule != null
                && rule.Type == ValidationRuleType.Value
                && !String.IsNullOrWhiteSpace(rule.Maximum)
                && !String.IsNullOrWhiteSpace(rule.Minimum));
        }

        private Entity _entity;
        private string _version;
    }
}
