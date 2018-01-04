using System.Activities.Tracking;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Activities.Tracking.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Web.Configuration;

namespace SPPC.Framework.Workflow.Tracking
{
    /// <summary>
    /// A WCF service behavior that enables tracking workflow states in a SQL Server database
    /// </summary>
    public class SqlTrackingBehavior : IServiceBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlTrackingBehavior"/> class
        /// </summary>
        /// <param name="profileName">Name of the tracking profile to use</param>
        public SqlTrackingBehavior(string profileName)
        {
            _profileName = profileName;
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom extension objects such as
        /// error handlers, message or parameter interceptors, security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description</param>
        /// <param name="serviceHostBase">The host that is currently being built</param>
        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            WorkflowServiceHost workflowServiceHost = serviceHostBase as WorkflowServiceHost;
            if (workflowServiceHost != null)
            {
                string workflowDisplayName = workflowServiceHost.Activity.DisplayName;
                TrackingProfile trackingProfile = GetProfile(_profileName, workflowDisplayName);

                ////workflowServiceHost.WorkflowExtensions.Add(() =>
                ////        {
                ////            var participant = UnityConfig.GetConfiguredContainer()
                ////                .Get<SqlTrackingParticipant>();
                ////            participant.TrackingProfile = trackingProfile;
                ////            return participant;
                ////        });
            }
        }

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the contract implementation
        /// </summary>
        /// <param name="serviceDescription">The service description of the service</param>
        /// <param name="serviceHostBase">The host of the service</param>
        /// <param name="endpoints">The service endpoints</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access</param>
        public virtual void AddBindingParameters(
            ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description to confirm
        /// that the service can run successfully
        /// </summary>
        /// <param name="serviceDescription">The service description</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed</param>
        public virtual void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }

        private static TrackingProfile GetProfile(string profileName, string displayName)
        {
            TrackingProfile trackingProfile = null;
            TrackingSection trackingSection = (TrackingSection)WebConfigurationManager.GetSection("system.serviceModel/tracking");
            if (trackingSection == null) 
            {
                return null;
            }

            if (profileName == null) 
            {
                profileName = string.Empty;
            }

            // Find the profile with the specified profile name in the list of profile found in config
            var match = from p in new List<TrackingProfile>(trackingSection.TrackingProfiles)
                        where (p.Name == profileName) && ((p.ActivityDefinitionId == displayName) || (p.ActivityDefinitionId == "*"))
                        select p;

            if (match.Count() == 0)
            {
                // Return an empty profile
                trackingProfile = new TrackingProfile()
                {
                    ActivityDefinitionId = displayName
                };
            }
            else
            {
                trackingProfile = match.First();
            }

            return trackingProfile;
        }        

        private string _profileName;
    }
}
