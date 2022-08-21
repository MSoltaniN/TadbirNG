using SPPC.Framework.Utility;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class DockerCompose : ITextTemplate
    {
        public DockerCompose(string editionTag)
        {
            _editionTag = editionTag;
        }

        private readonly string _editionTag;
    }
}
