using SPPC.Framework.Utility;

namespace SPPC.Tadbir.Utility.Templates
{
    public partial class DockerCompose : ITextTemplate
    {
        public DockerCompose(string editionTag, string dbServer)
        {
            _editionTag = editionTag;
            _dbServer = dbServer;
        }

        private readonly string _editionTag;
        private readonly string _dbServer;
    }
}
