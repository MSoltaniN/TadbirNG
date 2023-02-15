using System.Text;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tools.Extensions
{
    public static class LocalReportExtensions
    {
        public static string ToScript(this LocalReportViewModel report,
            bool withIdentityOn = true, bool withIdentityOff = true)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Reporting].[LocalReport] ([LocalReportID], [LocaleID], [ReportID], [Caption])");
            scriptBuilder.AppendLine(
                $"    VALUES ({report.Id}, {report.LocaleId}, {report.ReportId}, N'{report.Caption}')");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
