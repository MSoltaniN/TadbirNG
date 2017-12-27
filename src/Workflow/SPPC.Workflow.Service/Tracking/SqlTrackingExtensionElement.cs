using System;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Configuration;
using System.Web.Configuration;

namespace SPPC.Framework.Workflow.Tracking
{
    /// <summary>
    /// Represents a configuration element that can be used to configure parameters required for tracking
    /// workflow state in a SQL Server database
    /// </summary>
    public class SqlTrackingExtensionElement : BehaviorExtensionElement
    {
        /// <summary>
        /// Gets or sets the name of connection string to use for connecting to the workflow tracking database
        /// </summary>
        [ConfigurationProperty("connectionStringName", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string)this["connectionStringName"]; }
            set { this["connectionStringName"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of tracking profile to use
        /// </summary>
        [ConfigurationProperty("profileName", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string ProfileName
        {
            get { return (string)this["profileName"]; }
            set { this["profileName"] = value; }
        }

        /// <summary>
        /// Gets the connection string to use for connecting to the workflow tracking database
        /// </summary>
        public string ConnectionString
        {
            get
            {
                ConnectionStringSettingsCollection connectionStrings = WebConfigurationManager.ConnectionStrings;
                if (connectionStrings == null)
                {
                    return null;
                }

                string connectionString = null;
                if (connectionStrings[ConnectionStringName] != null)
                {
                    connectionString = connectionStrings[ConnectionStringName].ConnectionString;
                }

                if (connectionString == null)
                {
                    throw new ConfigurationErrorsException(
                        String.Format(CultureInfo.InvariantCulture, "Connection string is required"));
                }

                return connectionString;
            }
        }

        /// <summary>
        /// Gets the type of behavior
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(SqlTrackingBehavior); }
        }

        /// <summary>
        /// Creates a behavior extension based on the current configuration settings
        /// </summary>
        /// <returns>The behavior extension</returns>
        protected override object CreateBehavior()
        {
            return new SqlTrackingBehavior(ProfileName);
        }
    }
}
