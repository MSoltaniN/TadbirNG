// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2015-01-06 10:53:52 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace SPPC.Tools.MetaDesigner.Configuration
{
    /// <summary>
    /// Defines configuration settings for Metadata Designer tool.
    /// </summary>
    public class MetaDesignerSection : ConfigurationSection
    {
        /// <summary>
        /// Initializes a new instance of MetaDesignerSection class.
        /// </summary>
        public MetaDesignerSection()
        {
        }

        /// <summary>
        /// A collection of file generator configurations in this section
        /// </summary>
        [ConfigurationProperty("generators", IsDefaultCollection = false)]
        public FileGeneratorElementCollection Generators
        {
            get
            {
                return (FileGeneratorElementCollection)base["generators"];
            }
        }


        /// <summary>
        /// A collection of custom command configurations in this section.
        /// </summary>
        [ConfigurationProperty("commands", IsDefaultCollection = false)]
        public CommandElementCollection Commands
        {
            get
            {
                return (CommandElementCollection)base["commands"];
            }
        }


        /// <summary>
        /// A collection of default command configurations in this section.
        /// </summary>
        [ConfigurationProperty("defaultCommands", IsDefaultCollection = false)]
        public CommandElementCollection DefaultCommands
        {
            get
            {
                return (CommandElementCollection)base["defaultCommands"];
            }
        }


        /// <summary>
        /// A collection of metadata editor configurations in this section.
        /// </summary>
        [ConfigurationProperty("editors", IsDefaultCollection = false)]
        public MetadataEditorElementCollection MetadataEditors
        {
            get
            {
                return (MetadataEditorElementCollection)base["editors"];
            }
        }

        /// <summary>
        /// Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The XmlReader object, which reads from the configuration file.</param>
        protected override void DeserializeSection(XmlReader reader)
        {
            base.DeserializeSection(reader);
            // Add custom processing code here.
        }

        /// <summary>
        /// Creates an XML string containing an unmerged view of the ConfigurationSection object
        /// as a single section to write to a file.
        /// </summary>
        /// <param name="parentElement">The ConfigurationElement instance to use as the parent
        /// when performing the un-merge.</param>
        /// <param name="name">The name of the section to create.</param>
        /// <param name="saveMode">The ConfigurationSaveMode instance to use when writing to a string.
        /// </param>
        /// <returns>An XML string containing an unmerged view of the ConfigurationSection object.</returns>
        protected override string SerializeSection(
            ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
        {
            string s = base.SerializeSection(parentElement, name, saveMode);
            // Add custom processing code here. 
            return s;
        }
    }
}