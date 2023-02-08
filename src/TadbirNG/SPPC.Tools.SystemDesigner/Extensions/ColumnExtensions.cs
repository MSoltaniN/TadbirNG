using System.Text;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class ColumnExtensions
    {
        public static string ToScript(
            this ColumnViewModel column, bool withIdentityOn = true, bool withIdentityOff = true)
        {
            var visibility = column.Visibility != "Visible"
                ? column.Visibility
                : null;
            var type = column.Type != "(not set)"
                ? column.Type
                : null;
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[Column] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], " +
                "[StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], " +
                "[IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])");
            scriptBuilder.AppendLine(
                $"    VALUES ({column.Id}, {column.ViewId}, '{column.Name}', " +
                $"{ScriptUtility.GetNullableValue(column.GroupName)}, " +
                $"{ScriptUtility.GetNullableValue(type, false)}, '{column.DotNetType}', " +
                $"'{column.StorageType}', '{column.ScriptType}', {column.Length}, {column.MinLength}, " +
                $"{ScriptUtility.GetDbBoolean(column.IsDynamic)}, " +
                $"{ScriptUtility.GetDbBoolean(column.IsFixedLength)}, " +
                $"{ScriptUtility.GetDbBoolean(column.IsNullable)}, " +
                $"{ScriptUtility.GetDbBoolean(column.AllowSorting)}, " +
                $"{ScriptUtility.GetDbBoolean(column.AllowFiltering)}, " +
                $"{ScriptUtility.GetNullableValue(visibility)}, {column.DisplayIndex}, " +
                $"{ScriptUtility.GetNullableValue(column.Expression, false)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[Column] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
