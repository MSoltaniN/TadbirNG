using System;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class DockerDeploy : ITextTemplate
    {
        public DockerDeploy(string licenseKey)
        {
            _imageGuid = licenseKey
                .ToLower()
                .Substring(0, 8);
        }

        private readonly string _imageGuid;
    }
}
