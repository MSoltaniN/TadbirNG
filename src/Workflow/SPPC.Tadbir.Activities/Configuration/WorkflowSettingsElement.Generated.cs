// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-07-04 12:19:53 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// Provides configuration for workflows in Tadbir application
    /// </summary>
    public class WorkflowSettingsElement : ConfigurationElement
    {
        /// <summary>
        /// Initializes a new instance of WorkflowSettingsElement class.
        /// </summary>
        public WorkflowSettingsElement()
        {
        }

        /// <summary>
        /// Gets configuration of all workflows in Tadbir application
        /// </summary>
        [ConfigurationProperty("workflows", IsDefaultCollection = false)]
        public WorkflowElementCollection Workflows
        {
            get
            {
                return (WorkflowElementCollection)base["workflows"];
            }
        }

        /// <summary>
        /// Reads XML from the configuration file.
        /// </summary>
        /// <param name="reader">The XmlReader that reads from the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
        {
            base.DeserializeElement(reader, serializeCollectionKey);
            // Enter your custom processing code here.
        }

        /// <summary>
        /// Writes the contents of this configuration element to the configuration file
        /// when implemented in a derived class.
        /// </summary>
        /// <param name="writer">The XmlWriter that writes to the configuration file.</param>
        /// <param name="serializeCollectionKey">true to serialize only the collection key properties; otherwise, false.</param>
        /// <returns>true if any data was actually serialized; otherwise, false.</returns>
        protected override bool SerializeElement(XmlWriter writer, bool serializeCollectionKey)
        {
            bool ret = base.SerializeElement(writer, serializeCollectionKey);
            // Enter your custom processing code here. 
            return ret;

        }

        /// <summary>
        /// Indicates whether this configuration element has been modified since it was
        /// last saved or loaded, when implemented in a derived class.
        /// </summary>
        /// <returns>true if the element has been modified; otherwise, false.</returns>
        protected override bool IsModified()
        {
            bool ret = base.IsModified();
            // Enter your custom processing code here. 
            return ret;
        }
    }
}
