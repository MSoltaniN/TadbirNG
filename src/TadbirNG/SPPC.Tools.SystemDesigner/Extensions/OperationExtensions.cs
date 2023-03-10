using System.Text;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class OperationExtensions
    {
        public static string ToScript(this OperationViewModel operation,
            bool withIdentityOn = false, bool withIdentityOff = false)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[Operation] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Metadata].[Operation] ([OperationID], [Name], [Description])");
            scriptBuilder.AppendLine(
                $"    VALUES ({operation.Id}, N'{operation.Name}', {ScriptUtility.GetNullableValue(operation.Description)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[Operation] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
