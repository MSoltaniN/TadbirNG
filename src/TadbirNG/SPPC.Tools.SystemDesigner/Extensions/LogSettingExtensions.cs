using System.Text;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class LogSettingExtensions
    {
        public static string ToScript(this LogSettingViewModel setting,
            bool withIdentityOn = false, bool withIdentityOff = false)
        {
            int isEnabled = setting.IsEnabled ? 1 : 0;
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Config].[LogSetting] ON");
            }

            scriptBuilder.Append(
                "INSERT INTO [Config].[LogSetting] ([LogSettingID], [SubsystemID], [SourceTypeID], ");
            scriptBuilder.AppendLine(
                "[SourceID], [EntityTypeID], [OperationID], [IsEnabled])");
            scriptBuilder.AppendLine(
                $"    VALUES ({setting.Id}, {setting.SubsystemId}, {setting.SourceTypeId}, " +
                $"{ScriptUtility.GetNullableValue(setting.SourceId)}, " +
                $"{ScriptUtility.GetNullableValue(setting.EntityTypeId)}, " +
                $"{setting.OperationId}, {isEnabled})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Config].[LogSetting] OFF");
            }

            return scriptBuilder.ToString();
        }

        public static string ToSysScript(this LogSettingViewModel setting,
            bool withIdentityOn = false, bool withIdentityOff = false)
        {
            int isEnabled = setting.IsEnabled ? 1 : 0;
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Config].[SysLogSetting] ON");
            }

            scriptBuilder.Append(
                "INSERT INTO [Config].[SysLogSetting] ([SysLogSettingID], [SourceID], [EntityTypeID], ");
            scriptBuilder.AppendLine(
                "[OperationID], [IsEnabled])");
            scriptBuilder.AppendLine(
                $"    VALUES ({setting.Id}, {ScriptUtility.GetNullableValue(setting.SourceId)}, " +
                $"{ScriptUtility.GetNullableValue(setting.EntityTypeId)}, {setting.OperationId}, " +
                $"{isEnabled})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Config].[SysLogSetting] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
