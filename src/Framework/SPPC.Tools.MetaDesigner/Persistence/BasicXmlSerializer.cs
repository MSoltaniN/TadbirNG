using System;
using System.Collections;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Persistence
{
    /// <summary>
    /// Provides a simple implementation for XML serialize/deserialize operations.
    /// </summary>
    /// <remarks>
    /// Current serialization logic in this class can be summarized as below :
    /// 1. No specific attribute is required by the input object.
    /// 2. All public properties in a given object will be serialized, regardless of any exclusion criteria
    /// that may be specified on a property.
    /// 3. A property having built-in types (i.e. all common types in System namespace, as well as all enum types)
    /// will be serialized as an XML attribute having property name in camel-case form.
    /// 4. A generic collection property will be serialized as a container XML element having original
    /// property name. Each element inside the collection will be serialized as a custom object (i.e. not built-in)
    /// To be able to later deserialize a collection element, an additional property containing the element's full
    /// type name (called clrType) will also be added to the resulting XML element.
    /// 5. A property having a custom non-enumeration type will be serialized as an XML child element recursively.
    /// </remarks>
    public class BasicXmlSerializer : IXmlSerializer
    {
        /// <summary>
        /// Converts given object into a well-formed XML representation and returns the serialized form
        /// as an XElement object.
        /// </summary>
        /// <param name="item">Object that needs to be serialized</param>
        /// <returns>Serialized form of the input object as an XElement instance</returns>
        public XElement Serialize(object item)
        {
            return Serialize(item, false);
        }

        /// <summary>
        /// Converts given object into a well-formed XML representation and saves the serialized form
        /// in a standard XML file (with UTF-8 encoding) in the specified path.
        /// </summary>
        /// <param name="path">Absolute or related path to the file where the serialized object must be saved</param>
        /// <param name="item">Object that needs to be serialized</param>
        public void Serialize(string path, object item)
        {
            Verify.ArgumentNotNullOrWhitespace(path, "path");
            Verify.ArgumentNotNull(item, "item");

            var xItem = Serialize(item);
            xItem.Save(path, SaveOptions.None);
        }
        public object Deserialize(XElement xItem, Type itemType)
        {
            Verify.ArgumentNotNull(xItem, "xItem");
            Verify.ArgumentNotNull(itemType, "itemType");

            object item = Reflector.Instantiate(itemType);
            var propertyNames = Reflector.GetPropertyNames(item);
            foreach (var propertyName in propertyNames)
            {
                if (!IsSerialized(itemType, propertyName))
                    continue;

                XAttribute xAttribute = null;
                XElement xElement = null;
                var type = Reflector.GetPropertyType(item, propertyName);
                if (Reflector.IsBuiltin(type))
                {
                    xAttribute = xItem.Attribute(propertyName.CamelCase());
                    if (xAttribute != null)
                    {
                        if (type.IsEnum)
                            Reflector.SetProperty(item, propertyName, Enum.Parse(type, xAttribute.Value));
                        else
                            Reflector.SetProperty(item, propertyName, Convert.ChangeType(xAttribute.Value, type));
                    }
                }
                else if (Reflector.DerivesFromInterface(type, typeof(IEnumerable)))
                {
                    var collection = Reflector.GetProperty(item, propertyName);
                    var xCollection = xItem.Element(propertyName);
                    if (xCollection != null)
                    {
                        foreach (var x in xCollection.Elements())
                        {
                            var elmType = Type.GetType(x.Attribute("clrType").Value);
                            Reflector.Invoke(collection, "Add", Deserialize(x, elmType));
                        }
                    }
                }
                else
                {
                    var propertyType = Reflector.GetPropertyType(item, propertyName);
                    xElement = xItem.Element(propertyType.Name);
                    if (xElement != null)
                        Reflector.SetProperty(item, propertyName, Deserialize(xElement, type));
                }
            }

            return item;
        }
        public object Deserialize(string path, Type itemType)
        {
            Verify.ArgumentNotNullOrWhitespace(path, "path");
            Verify.ArgumentNotNull(itemType, "itemType");

            var xRoot = XElement.Load(path);
            return Deserialize(xRoot, itemType);
        }

        private XElement Serialize(object item, bool withType = false)
        {
            Verify.ArgumentNotNull(item, "item");

            var itemType = item.GetType();
            var xItem = new XElement(itemType.Name);
            var propertyNames = Reflector.GetPropertyNames(item)
                .Where(prop => !Reflector.IsReadOnly(item, prop) || Reflector.IsGenericCollection(item, prop));
            foreach (var propertyName in propertyNames)
            {
                if (!IsSerialized(itemType, propertyName))
                    continue;

                var property = Reflector.GetProperty(item, propertyName);
                var propertyType = Reflector.GetPropertyType(item, propertyName);
                if (Reflector.IsBuiltin(propertyType))
                {
                    var value = (property != null) ? property.ToString() : String.Empty;
                    xItem.Add(new XAttribute(propertyName.CamelCase(), value));
                }
                else if (Reflector.IsGenericCollection(item, propertyName))
                {
                    var collection = property as IEnumerable;
                    var xItems = new XElement(propertyName);
                    foreach (var element in collection)
                    {
                        xItems.Add(Serialize(element, true));
                    }

                    xItem.Add(xItems);
                }
                else
                {
                    if (property == null)
                        xItem.Add(new XElement(propertyType.Name));
                    else
                        xItem.Add(Serialize(property));
                }
            }

            if (withType)
            {
                xItem.Add(new XAttribute("clrType", item.GetType().AssemblyQualifiedName));
            }

            return xItem;
        }
        private bool IsSerialized(Type itemType, string propertyName)
        {
            return (Reflector.GetPropertyAttribute(itemType, propertyName, typeof(XmlIgnoreAttribute)) == null);
        }
    }
}
