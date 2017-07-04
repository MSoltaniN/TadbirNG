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
using System.Web;
using System.Web.Configuration;
using Sys = System.Configuration;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// Provides a convenient wrapper for accessing top-level configuration items in TadbirConfigurationSection class.
    /// </summary>
    public class TadbirConfigurationSectionHandler
    {
        /// <summary>
        /// Gets the custom configuration section populated with data from 'sppc.tadbir'
        /// element in application configuration file.
        /// </summary>
        public TadbirConfigurationSection Section
        {
            get
            {
                if (_configSection == null)
                    _configSection = LoadSection();

                return _configSection;
            }
        }

        public void Save()
        {
            var config = Section.CurrentConfiguration;
            config.Save();
        }

        private static TadbirConfigurationSection LoadSection()
        {
            var config = GetRootConfig();
            var configSection = config.GetSection("sppc.tadbir") as TadbirConfigurationSection;
            if (configSection == null)
            {
                throw new Sys::ConfigurationErrorsException(
                    "Could not find configuration section 'sppc.tadbir' in the main configuration file.");
            }

            return configSection;
        }

        private static Sys.Configuration GetRootConfig()
        {
            return (HttpContext.Current != null)
                ? WebConfigurationManager.OpenWebConfiguration("~")
                : Sys.ConfigurationManager.OpenExeConfiguration(Sys.ConfigurationUserLevel.None);
        }

        private TadbirConfigurationSection _configSection;
    }
}
