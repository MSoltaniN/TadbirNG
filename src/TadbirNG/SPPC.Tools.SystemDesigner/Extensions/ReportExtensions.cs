using System.Text;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class ReportExtensions
    {
        public static string ToScript(this ReportViewModel report,
            bool withIdentityOn = true, bool withIdentityOff = true)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Reporting].[Report] ([ReportID], [ParentID], [CreatedByID], [ViewID], " +
                $"[SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic], " +
                $"[ResourceKeys])");
            scriptBuilder.AppendLine(
                $"    VALUES ({report.Id}, {ScriptUtility.GetNullableValue(report.ParentId)}, " +
                $"{report.CreatedById}, {ScriptUtility.GetNullableValue(report.ViewId)}, " +
                $"{report.SubsystemId}, N'{report.Code}', {ScriptUtility.GetNullableValue(report.ServiceUrl)}, " +
                $"{ScriptUtility.GetDbBoolean(report.IsGroup)}, {ScriptUtility.GetDbBoolean(report.IsSystem)}, " +
                $"{ScriptUtility.GetDbBoolean(report.IsDefault)}, {ScriptUtility.GetDbBoolean(report.IsDynamic)}, " +
                $"{ScriptUtility.GetNullableValue(report.ResourceKeys)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
