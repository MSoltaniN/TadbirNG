using System.Text;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Utility;

namespace SPPC.Tools.Extensions
{
    public static class EntityTypeExtensions
    {
        public static string ToScript(this EntityTypeViewModel entity,
            bool withIdentityOn = false, bool withIdentityOff = false)
        {
            var scriptBuilder = new StringBuilder();
            if (withIdentityOn)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[EntityType] ON");
            }

            scriptBuilder.AppendLine(
                "INSERT INTO [Metadata].[EntityType] ([EntityTypeID], [Name], [Description])");
            scriptBuilder.AppendLine(
                $"    VALUES ({entity.Id}, N'{entity.Name}', {ScriptUtility.GetNullableValue(entity.Description)})");

            if (withIdentityOff)
            {
                scriptBuilder.AppendLine("SET IDENTITY_INSERT [Metadata].[EntityType] OFF");
            }

            return scriptBuilder.ToString();
        }
    }
}
