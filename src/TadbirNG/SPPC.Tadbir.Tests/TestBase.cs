using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using SPPC.Tadbir.Resources;

namespace SPPC.Tadbir.Tests
{
    public abstract class TestBase
    {
        protected static IStringLocalizer<AppStrings> GetStringLocalizer()
        {
            var options = new LocalizationOptions()
            {
                ResourcesPath = @"..\..\..\src\TadbirNG\SPPC.Tadbir.Resources"
            };
            var factory = new ResourceManagerStringLocalizerFactory(
                new OptionsWrapper<LocalizationOptions>(options), new NullLoggerFactory());
            return new StringLocalizer<AppStrings>(factory);
        }

        protected const string _configPath = @"..\..\..\src\TadbirNG\SPPC.Tools.LicenseManager\edition-config.json";
    }
}
