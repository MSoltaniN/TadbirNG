using SPPC.Framework.Utility;

namespace SPPC.Tadbir.Utility.Templates
{
    public partial class WebApiSettings : ITextTemplate
    {
        public WebApiSettings(IBuildSettings settings)
        {
            _settings = settings;
        }

        private readonly IBuildSettings _settings;
    }
}
