using System;
using System.Collections.Generic;
using System.Linq;
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

        private string GetRelationArea(Relation relation)
        {
            string area = String.Empty;
            var entity = _entity.Repository.Entities
                .Where(ent => ent.Name == relation.EntityName)
                .SingleOrDefault();
            if (entity != null)
            {
                area = entity.Area;
            }

            return area;
        }

        private Entity _entity;
        private string _version;
    }
}
