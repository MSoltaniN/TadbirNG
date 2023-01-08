using System;
using System.Text;
using SPPC.Tadbir.ViewModel.Auth;

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
                "INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName], [Description])");
            scriptBuilder.AppendLine(
                $"    VALUES ({group.Id}, N'{group.Name}', N'{group.EntityName}', " +
                $"{GetNullableValue(group.Description)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF");
            }

            return scriptBuilder.ToString();
        }

        private static string GetNullableValue(string nullable)
        {
            return String.IsNullOrEmpty(nullable)
                ? "NULL"
                : String.Format("N'{0}'", nullable);
        }
    }
}
