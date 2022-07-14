using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class NgEnvironment : ITextTemplate
    {
        public NgEnvironment(IBuildSettings settings)
        {
            _settings = settings;
        }

        private readonly IBuildSettings _settings;
    }
}
