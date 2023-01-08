using System;
using System.Text;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tools.Extensions
{
    public static class PermissionExtensions
    {
        public static string ToScript(
            this PermissionViewModel permission, bool withIdentityOn = true, bool withIdentityOff = true)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Auth].[Permission] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag], [Description])");
            scriptBuilder.AppendLine(
                $"    VALUES ({permission.Id}, {permission.GroupId}, N'{permission.Name}', {permission.Flag}, " +
                $"{GetNullableValue(permission.Description)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Auth].[Permission] OFF");
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
