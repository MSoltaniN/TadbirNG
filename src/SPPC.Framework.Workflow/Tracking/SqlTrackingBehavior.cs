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
    public class SqlTrackingBehavior : IServiceBehavior
    {
        public SqlTrackingBehavior(string profileName)
        {
            _profileName = profileName;
        }

        public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            WorkflowServiceHost workflowServiceHost = serviceHostBase as WorkflowServiceHost;
            if (workflowServiceHost != null)
            {
                string workflowDisplayName = workflowServiceHost.Activity.DisplayName;
                TrackingProfile trackingProfile = GetProfile(_profileName, workflowDisplayName);

                workflowServiceHost.WorkflowExtensions.Add(() =>
                        {
                            var participant = UnityConfig.GetConfiguredContainer()
                                .Get<SqlTrackingParticipant>();
                            participant.TrackingProfile = trackingProfile;
                            return participant;
                        });
            }
        }

        public virtual void AddBindingParameters(
            ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

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
