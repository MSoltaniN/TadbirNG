using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class CsPocoFromXmlMetadata : ITextTemplate
    {
        public CsPocoFromXmlMetadata(Entity entity)
        {
            Verify.ArgumentNotNull(entity);
            _entity = entity;
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private IList<Property> GetNonInheritedProperties()
        {
            var noninherited = new List<Property>();
            var typeFullName = String.Format("{0}.{1}, {0}", _modelAssembly, _entity.Type);
            var type = Type.GetType(typeFullName);
            if (type != null)
            {
                var propNames = Reflector.GetPropertyNames(type);
                noninherited = _entity.Properties
                    .Where(prop => !propNames.Contains(prop.Name))
                    .ToList();
            }

            return noninherited;
        }

        private IList<Relation> GetNonInheritedRelations()
        {
            var noninherited = new List<Relation>();
            var typeFullName = String.Format("{0}.{1}, {0}", _modelAssembly, _entity.Type);
            var type = Type.GetType(typeFullName);
            if (type != null)
            {
                var relNames = Reflector.GetPropertyNames(type);
                noninherited = _entity.Relations
                    .Where(rel => !relNames.Contains(rel.Name))
                    .ToList();
            }

            return noninherited;
        }

        private IList<string> GetRequiredNamespaces()
        {
            var namespaces = new List<string>();
            var relations = GetNonInheritedRelations();
            foreach (var relation in relations)
            {
                var entity = _entity.Repository.Entities
                    .Where(ent => ent.Name == relation.EntityName)
                    .SingleOrDefault();
                if (entity != null && entity.Area != _entity.Area)
                {
                    namespaces.Add(String.Format("{0}.{1}", _modelAssembly, entity.Area));
                }
            }

            return namespaces
                .Distinct()
                .OrderBy(item => item)
                .ToList();
        }

        private string GetTypeAlias(Property property)
        {
            string builtinTypeName = property.Type.ToString();
            var alias = String.Empty;
            switch (builtinTypeName)
            {
                case "String":
                case "Char":
                case "Byte":
                case "SByte":
                case "Decimal":
                case "Double":
                    alias = builtinTypeName.ToLower();
                    break;
                case "Int16":
                    alias = "short";
                    break;
                case "UInt16":
                    alias = "ushort";
                    break;
                case "Int32":
                    alias = "int";
                    break;
                case "UInt32":
                    alias = "uint";
                    break;
                case "Int64":
                    alias = "long";
                    break;
                case "UInt64":
                    alias = "ulong";
                    break;
                case "Single":
                    alias = "float";
                    break;
                case "Boolean":
                    alias = "bool";
                    break;
                default:
                    alias = builtinTypeName;
                    break;
            }

            if (property.Type.ToString() != "String" && property.Storage.Nullable)
            {
                alias += "?";
            }

            return alias;
        }

        private const string _modelAssembly = "SPPC.Tadbir.Model";
        private Entity _entity;
        private string _version;
    }
}
