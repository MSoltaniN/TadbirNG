using System;
using System.Configuration;
using System.Globalization;
using System.ServiceModel.Configuration;
using System.Web.Configuration;

namespace SPPC.Framework.Workflow.Tracking
{
    public class SqlTrackingExtensionElement : BehaviorExtensionElement
    {
        [ConfigurationProperty("connectionStringName", DefaultValue = "", IsKey = false, IsRequired = true)]
        public string ConnectionStringName
        {
            get { return (string)this["connectionStringName"]; }
            set { this["connectionStringName"] = value; }
        }

        [ConfigurationProperty("profileName", DefaultValue = "", IsKey = false, IsRequired = false)]
        public string ProfileName
        {
            get { return (string)this["profileName"]; }
            set { this["profileName"] = value; }
        }

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

        public override Type BehaviorType
        {
            get { return typeof(SqlTrackingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new SqlTrackingBehavior(ProfileName);
        }
    }
}
