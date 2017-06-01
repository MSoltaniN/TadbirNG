using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Activities;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using SPPC.Framework.Unity.WF;

namespace SPPC.Tadbir.WorkflowService.Unity
{
    public class DependencyInjectionBehavior : IServiceBehavior
    {
        public void AddBindingParameters(
            ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            WorkflowServiceHost host = serviceHostBase as WorkflowServiceHost;
            if (host != null)
            {
                var container = UnityConfig.GetConfiguredContainer();
                host.WorkflowExtensions.Add<DependencyInjectionExtension>(
                    () => new DependencyInjectionExtension(container));
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}
