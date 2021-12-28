using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
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
