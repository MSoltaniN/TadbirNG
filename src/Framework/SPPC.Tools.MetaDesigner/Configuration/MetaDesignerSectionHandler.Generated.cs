// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2015-01-01 6:14:33 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Configuration;

namespace SPPC.Tools.MetaDesigner.Configuration
{
    /// <summary>
    /// Provides a convenient wrapper for accessing top-level configuration items in MetaDesignerSection class.
    /// </summary>
    public class MetaDesignerSectionHandler
    {
        /// <summary>
        /// Gets the custom configuration section populated with data from 'swforall.platform.metaDesigner'
        /// element in application configuration file.
        /// </summary>
        public MetaDesignerSection Section
        {
            get
            {
                if (_configSection == null)
                    _configSection = LoadSection();

                return _configSection;
            }
        }

        private MetaDesignerSection LoadSection()
        {
            var configSection = ConfigurationManager.GetSection("sppc.tools.metaDesigner") as MetaDesignerSection;
            //var configSection = config.GetSection("sppc.tools.metaDesigner") as MetaDesignerSection;
            if (configSection == null)
            {
                throw new ConfigurationErrorsException(
                    "Could not find configuration section 'sppc.tools.metaDesigner' in the main configuration file.");
            }

            return configSection;
        }

        private MetaDesignerSection _configSection;
    }
}
