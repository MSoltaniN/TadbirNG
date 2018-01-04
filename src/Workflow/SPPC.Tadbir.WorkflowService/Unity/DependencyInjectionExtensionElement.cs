using System;
using System.ServiceModel.Configuration;

namespace SPPC.Tadbir.WorkflowService.Unity
{
    public class DependencyInjectionExtensionElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(DependencyInjectionBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new DependencyInjectionBehavior();
        }
    }
}
