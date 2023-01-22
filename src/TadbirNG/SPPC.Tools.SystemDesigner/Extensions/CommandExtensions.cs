using System.Text;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class CommandExtensions
    {
        public static string ToScript(
            this CommandViewModel command, bool withIdentityOn = true, bool withIdentityOff = true)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[Command] ON");
            }

            scriptBuilder.Append(
                "INSERT INTO [Metadata].[Command] ([CommandID], [ParentID], [PermissionID], [TitleKey], ");
            scriptBuilder.AppendLine("[RouteUrl], [IconName], [HotKey])");
            scriptBuilder.AppendLine(
                $"    VALUES ({command.Id}, {ScriptUtility.GetNullableValue(command.ParentId)}, " +
                $"{ScriptUtility.GetNullableValue(command.PermissionId)}, N'{command.Title}', " +
                $"{ScriptUtility.GetNullableValue(command.RouteUrl)}, " +
                $"{ScriptUtility.GetNullableValue(command.IconName, false)}, " +
                $"{ScriptUtility.GetNullableValue(command.HotKey, false)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[Command] OFF");
            }

            return scriptBuilder.ToString();
        }

        public static string ToUpdateScript(this CommandViewModel command)
        {
            return 
$@"UPDATE [Metadata].[Command]
SET [PermissionID] = {ScriptUtility.GetNullableValue(command.PermissionId)}, [TitleKey] = N'{command.Title}', " +
$@"[RouteUrl] = {ScriptUtility.GetNullableValue(command.RouteUrl)}, " +
$@"[IconName] = {ScriptUtility.GetNullableValue(command.IconName, false)}, " +
$@"[HotKey] = {ScriptUtility.GetNullableValue(command.HotKey, false)}";
        }
    }
}
