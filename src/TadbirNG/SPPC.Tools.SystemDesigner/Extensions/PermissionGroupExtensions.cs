using System.Text;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class PermissionGroupExtensions
    {
        public static string ToScript(
            this PermissionGroupViewModel group, bool withIdentityOn = true, bool withIdentityOff = true)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Auth].[PermissionGroup] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [SubsystemID], [SourceTypeID], " +
                "[Name], [EntityName], [Description])");
            scriptBuilder.AppendLine(
                $"    VALUES ({group.Id}, 2, 1, N'{group.Name}', N'{group.EntityName}', " +
                $"{ScriptUtility.GetNullableValue(group.Description)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
