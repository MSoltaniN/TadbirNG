﻿using System;
using System.Linq;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Transforms
{
    public class BasicMetaGenerator : IMetaGenerator
    {
        public Repository GenerateFileRepository(string name, string path)
        {
            var repoName = !String.IsNullOrWhiteSpace(name)
                ? name
                : "Repository";
            return new Repository()
            {
                Name = repoName,
                Store = StorageFactory.CreateFile(path)
            };
        }

        public Entity GenerateEntity(string name, Repository repository)
        {
            var entity = new Entity() { Name = name, Identifier = "ID", Repository = repository };
            entity.Properties.Add(GetDefaultIdProperty(name));
            return entity;
        }

        public Entity GenerateAsIEntity(string name, Repository repository)
        {
            var entity = new Entity() { Name = name, Identifier = "Id", Repository = repository };

            // Generate and add Id property...
            var property = GenerateProperty("Id", BuiltinType.Int32);
            property.Storage.Name = String.Format("{0}ID", name);
            property.IsValidated = false;
            entity.Properties.Add(property);

            // Generate and add RowGuid property...
            property = GenerateProperty("RowGuid", BuiltinType.Guid);
            property.Storage.Name = "rowguid";
            property.ValidationRule.Required = true;
            property.IsValidated = false;
            entity.Properties.Add(property);

            // Generate and add ModifiedDate property...
            property = GenerateProperty("ModifiedDate", BuiltinType.DateTime);
            property.Storage.Name = "ModifiedDate";
            property.ValidationRule.Required = true;
            property.IsValidated = false;
            entity.Properties.Add(property);

            return entity;
        }

        public Property GenerateProperty(string name, BuiltinType type, int length = 0)
        {
            var property = new Property()
                {
                    Name = name,
                    Type = type,
                    Column = GetDefaultColumn(name),
                    Storage = GetDefaultStorage(name, type, length),
                    View = GetDefaultView(name, type)
                };
            property.ValidationRule = ValidationRuleFactory.CreateDefault(property.Type);
            property.ValidationRule.Name = String.Format("{0}_Validation", property.Name);
            return property;
        }

        public TItem GenerateDefaultItem<TItem>()
            where TItem : class, new()
        {
            return GenerateDefaultItem(typeof(TItem)) as TItem;
        }

        public object GenerateDefaultItem(Type itemType)
        {
            object item = Reflector.Instantiate(itemType);
            if (Reflector.GetPropertyNames(item, typeof(string)).Contains("Name"))
            {
                Reflector.SetProperty(item, "Name", String.Format("New{0}", itemType.Name));
            }

            return item;
        }

        private ColumnView GetDefaultColumn(string name)
        {
            return new ColumnView()
            {
                Name = name,
                Visible = true,
                Width = 120
            };
        }

        private PropertyStorage GetDefaultStorage(string name, BuiltinType type, int length)
        {
            var mapper = new SqlStorageMapper();
            return new PropertyStorage()
            {
                Name = name,
                Type = mapper.MapPropertyType(type, length),
                Nullable = false
            };
        }

        private PropertyView GetDefaultView(string name, BuiltinType type)
        {
            var mapper = new BasicPropertyViewMapper();
            var viewType = mapper.MapPropertyType(type);
            return new PropertyView()
            {
                Name = mapper.GetDefaultName(name, viewType),
                Type = viewType,
                BindingMember = mapper.GetDefaultBindingMember(viewType),
                Format = (type == BuiltinType.DateTime) ? EntityConstants.DateFormat : String.Empty
            };
        }

        private Property GetDefaultIdProperty(string entityName)
        {
            var idProperty = GenerateProperty("ID", BuiltinType.Int32);
            idProperty.Column.Visible = false;
            idProperty.Storage.Name = String.Format("{0}ID", entityName);
            idProperty.View = GetDefaultView(String.Format("{0}ID", entityName), BuiltinType.String);
            idProperty.ValidationRule = new ValidationRule()
            {
                Name = "ID_Validation",
                Type = ValidationRuleType.Value,
                Required = true
            };

            return idProperty;
        }
    }
}
