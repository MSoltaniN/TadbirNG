using System;
using System.Linq;
using BabakSoft.Platform.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Templates
{
    public partial class SqlCreateTableFromMetadata
    {
        public SqlCreateTableFromMetadata(Entity[] entities)
        {
            _entities = entities;
        }

        private void SetupTokenPadding(Entity entity)
        {
            var lengths = entity.Properties.Select(prop => prop.Storage.Name.Length);
            if (entity.Relations.Count > 0)
            {
                lengths = lengths.Concat(entity.Relations.Select(rel => rel.EndpointName.Length));
            }

            _maxFieldPadding = lengths.Max() + 3;
            _maxTypePadding = entity.Properties.Select(prop => prop.Storage.Type.Length).Max() + 1;
        }

        private string PadFieldName(string field)
        {
            var padding = new string(' ', _maxFieldPadding - field.Length);
            return String.Format("[{0}]{1}", field, padding);
        }

        private string PadTypeName(string type)
        {
            var padding = new string(' ', _maxTypePadding - type.Length);
            return String.Format("{0}{1}", type, padding);
        }

        private string GetRelationArea(Entity entity, Relation relation)
        {
            string area = String.Empty;
            var relatedEntity = entity.Repository.Entities
                .Where(ent => ent.Name == relation.EntityName)
                .SingleOrDefault();
            if (relatedEntity != null)
            {
                area = relatedEntity.Area;
            }

            return area;
        }

        private Entity[] _entities;
        private int _maxFieldPadding;
        private int _maxTypePadding;
    }
}
