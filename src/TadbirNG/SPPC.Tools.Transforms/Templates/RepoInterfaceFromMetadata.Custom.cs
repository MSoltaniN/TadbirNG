using System;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class RepoInterfaceFromMetadata
    {
        public RepoInterfaceFromMetadata(EntityInfoModel model)
        {
            _model = model;
        }

        private readonly EntityInfoModel _model;
    }
}
