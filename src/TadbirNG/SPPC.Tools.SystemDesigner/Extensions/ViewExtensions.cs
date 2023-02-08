using System.Text;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class ViewExtensions
    {
        public static string ToScript(
            this ViewViewModel view, bool withIdentityOn = true, bool withIdentityOff = true)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[View] ON");
            }

            int isHierarchy = view.IsHierarchy ? 1 : 0;
            int enableCartable = view.IsCartableIntegrated ? 1 : 0;
            scriptBuilder.AppendLine(
                "INSERT INTO [Metadata].[View] ([ViewID], [Name], [EntityName], [EntityType], " +
                "[FetchUrl], [SearchUrl], [IsHierarchy], [IsCartableIntegrated])");
            scriptBuilder.AppendLine(
                $"    VALUES ({view.Id}, '{view.Name}', N'{view.EntityName}', " +
                $"{ScriptUtility.GetNullableValue(view.Entitytype)}, " +
                $"{ScriptUtility.GetNullableValue(view.FetchUrl)}, " +
                $"{ScriptUtility.GetNullableValue(view.SearchUrl)}, " +
                $"{isHierarchy}, {enableCartable})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[View] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
