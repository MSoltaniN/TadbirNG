using System;
using System.Collections.Generic;
using System.Reflection;
using BabakSoft.Platform.Metadata;
using SPPC.Framework.Common;

namespace SPPC.Framework.Tools.ProjectCLI.Templates
{
    public partial class CsFluentMappingFromMetadata
    {
        public CsFluentMappingFromMetadata(Entity entity)
        {
            Verify.ArgumentNotNull(entity);
            _entity = entity;
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private Entity _entity;
        private string _version;
    }
}
