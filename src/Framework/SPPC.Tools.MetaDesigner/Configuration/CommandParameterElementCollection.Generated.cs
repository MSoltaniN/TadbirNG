// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2015-01-01 4:35:34 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;

namespace SPPC.Tools.MetaDesigner.Configuration
{
    /// <summary>
    /// Defines configuration settings for a collection of command parameters.
    /// </summary>
    public class CommandParameterElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// Gets the type of the ConfigurationElementCollection.
        /// </summary>
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        /// <summary>
        /// Gets or sets the configuration element with the specified index.
        /// </summary>
        /// <param name="index">The index of the element to return.</param>
        /// <returns>The CommandParameterElement with the specified index; otherwise, null.</returns>
        public CommandParameterElement this[int index]
        {
            get
            {
                return (CommandParameterElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }

        /// <summary>
        /// Returns the configuration element with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to return.</param>
        /// <returns>The CommandParameterElement with the specified key; otherwise, null.</returns>
        public new CommandParameterElement this[string key]
        {
            get
            {
                return (CommandParameterElement)BaseGet(key);
            }
        }

        /// <summary>
        /// Creates a new ConfigurationElement.
        /// </summary>
        /// <returns>A new ConfigurationElement.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new CommandParameterElement();
        }

        /// <summary>
        /// Indicates the index of the specified FileGeneratorElement.
        /// </summary>
        /// <param name="item">The CommandParameterElement for the specified index location.</param>
        /// <returns>The index of the specified CommandParameterElement; otherwise, -1.</returns>
        public int IndexOf(CommandParameterElement item)
        {
            return BaseIndexOf(item);
        }

        /// <summary>
        /// Adds a collection-specific configuration element to the ConfigurationElementCollection.
        /// </summary>
        /// <param name="item">The CommandParameterElement to add.</param>
        public void Add(CommandParameterElement item)
        {
            BaseAdd(item);
            // Add custom code here.
        }

        /// <summary>
        /// Adds a configuration element to the ConfigurationElementCollection.
        /// </summary>
        /// <param name="element">The ConfigurationElement to add.</param>
        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
            // Add custom code here.
        }

        /// <summary>
        /// Gets the element key for a specified configuration element when overridden in a derived class.
        /// </summary>
        /// <param name="element">The ConfigurationElement to return the key for.</param>
        /// <returns>An Object that acts as the key for the specified ConfigurationElement.</returns>
        protected override Object GetElementKey(ConfigurationElement element)
        {
            return ((CommandParameterElement)element).Name;
        }

        /// <summary>
        /// Removes a CommandParameterElement item from the collection.
        /// </summary>
        /// <param name="item">The CommandParameterElement instance to remove from the collection</param>
        public void Remove(CommandParameterElement item)
        {
            if (BaseIndexOf(item) >= 0)
                BaseRemove(item.Name);
        }

        /// <summary>
        /// Removes a ConfigurationElement from the collection.
        /// </summary>
        /// <param name="key">The key of the ConfigurationElement to remove.</param>
        public void Remove(string key)
        {
            BaseRemove(key);
        }

        /// <summary>
        /// Removes the ConfigurationElement at the specified index location.
        /// </summary>
        /// <param name="index">The index location of the ConfigurationElement to remove.</param>
        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        /// <summary>
        /// Removes all configuration element objects from the collection.
        /// </summary>
        public void Clear()
        {
            BaseClear();
            // Add custom code here.
        }
    }
}
