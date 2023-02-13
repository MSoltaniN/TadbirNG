using System.Text;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class OperationSourceExtensions
    {
        public static string ToScript(this OperationSourceViewModel source,
            bool withIdentityOn = false, bool withIdentityOff = false)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[OperationSource] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name], [Description])");
            scriptBuilder.AppendLine(
                $"    VALUES ({source.Id}, N'{source.Name}', {ScriptUtility.GetNullableValue(source.Description)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[OperationSource] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
