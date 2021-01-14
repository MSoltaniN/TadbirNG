using System;
using System.Collections;
using System.Linq;
using System.Xml.Serialization;
using SPPC.Framework.Common;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// Builds a Tree data structure from a source object using its property information.
    /// </summary>
    public class ObjectTreeBuilder
    {
        /// <summary>
        /// Reflects on type information of the source object and creates a Tree object containing
        /// the full object hierarchy contained in source object. 
        /// </summary>
        /// <param name="source">The object to build a tree hierarchy for.</param>
        /// <param name="metadata">If not null, serves as custom metadata in the root node
        /// of the resulting Tree object.</param>
        /// <returns>A Tree object built from type information in source object.</returns>
        public Tree BuildTree(object source, object metadata = null)
        {
            Verify.ArgumentNotNull(source, "source");
            var root = new Tree()
            {
                Data = source,
                Metadata = metadata ?? GetDefaultMetadata(source)
            };

            foreach (var propertyName in Reflector.GetPropertyNames(source))
            {
                if (!IsSerialized(source.GetType(), propertyName))
                    continue;

                var propertyType = Reflector.GetPropertyType(source, propertyName);
                object property = Reflector.GetProperty(source, propertyName);
                if (Reflector.IsBuiltin(propertyType))
                {
                    var leaf = new Leaf() { Name = propertyName, Data = property };
                    root.AddLeaf(leaf);
                }
                else if ((property is IEnumerable) && propertyType.IsGenericType)
                {
                    var child = new Tree()
                    {
                        Data = property,
                        Metadata = GetDefaultCollectionMetadata(property, propertyName)
                    };

                    var collection = property as IEnumerable;
                    foreach (var item in collection)
                    {
                        child.AddChild(BuildTree(item));
                    }

                    root.AddChild(child);
                }
                else
                {
                    root.AddChild(BuildTree(property, GetDefaultChildMetadata(source, propertyName)));
                }
            }

            return root;
        }

        private bool IsSerialized(Type itemType, string propertyName)
        {
            return (Reflector.GetPropertyAttribute(itemType, propertyName, typeof(XmlIgnoreAttribute)) == null);
        }
        private ObjectMetadata GetDefaultMetadata(object data)
        {
            var name = ObjectNameProvider.GetObjectName(data);
            if (String.IsNullOrEmpty(name))
                name = ObjectNameProvider.GetTypeName(data);
            return new ObjectMetadata() { Name = name };
        }
        private ObjectMetadata GetDefaultCollectionMetadata(object collection, string name)
        {
            var genericType = collection.GetType().GenericTypeArguments[0];
            return new ObjectMetadata()
            {
                Name = name,
                ItemType = genericType.AssemblyQualifiedName
            };
        }
        private ObjectMetadata GetDefaultChildMetadata(object data, string property)
        {
            return new ObjectMetadata() { Name = property };
        }
    }
}
