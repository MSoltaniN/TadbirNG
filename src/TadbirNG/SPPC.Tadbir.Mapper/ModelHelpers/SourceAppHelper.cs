using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Resources;

namespace SPPC.Tadbir.Mapper.ModelHelpers
{
    internal static class SourceAppHelper
    {
        internal static string GetTypeName(SourceApp sourceApp)
        {
            var type = sourceApp.Type == (short)SourceAppType.Source
                ? AppStrings.Source
                : AppStrings.Application;
            return type;
        }
      
    }
}
