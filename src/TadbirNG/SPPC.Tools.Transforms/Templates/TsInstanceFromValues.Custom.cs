using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class TsInstanceFromValues : ITextTemplate
    {
        public TsInstanceFromValues(IBuildSettings settings)
        {
            _settings = settings;
        }

        private readonly IBuildSettings _settings;
    }
}
