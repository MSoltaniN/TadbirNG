using System;
using System.Linq;
using System.Reflection;
using SPPC.Framework.Common;
using SPPC.Framework.Utility;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class CsFluentMappingFromMetadata : ITextTemplate
    {
        public CsFluentMappingFromMetadata(Repository repository, Entity entity)
        {
            Verify.ArgumentNotNull(repository, nameof(repository));
            Verify.ArgumentNotNull(entity, nameof(entity));

            _repository = repository;
            _entity = entity;
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private string GetRelationArea(Relation relation)
        {
            string area = String.Empty;
            var entity = _repository.Entities
                .Where(ent => ent.Name == relation.EntityName)
                .SingleOrDefault();
            if (entity != null)
            {
                area = entity.Area;
            }

            return area;
        }

        private readonly Repository _repository;
        private readonly Entity _entity;
        private readonly string _version;
    }
}
