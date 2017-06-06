﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SPPC.Tadbir.Workflow
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IStateWithDecision")]
    public interface IStateWithDecision
    {

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Prepare", ReplyAction = "http://tempuri.org/IStateWithDecision/PrepareResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Prepare(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Prepare", ReplyAction = "http://tempuri.org/IStateWithDecision/PrepareResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> PrepareAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Review", ReplyAction = "http://tempuri.org/IStateWithDecision/ReviewResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Review(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Review", ReplyAction = "http://tempuri.org/IStateWithDecision/ReviewResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> ReviewAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Approve", ReplyAction = "http://tempuri.org/IStateWithDecision/ApproveResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Approve(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Approve", ReplyAction = "http://tempuri.org/IStateWithDecision/ApproveResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> ApproveAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Confirm", ReplyAction = "http://tempuri.org/IStateWithDecision/ConfirmResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Confirm(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Confirm", ReplyAction = "http://tempuri.org/IStateWithDecision/ConfirmResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> ConfirmAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Reject", ReplyAction = "http://tempuri.org/IStateWithDecision/RejectResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Reject(SPPC.Tadbir.Workflow.StateOperation operation);

        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IStateWithDecision/Reject", ReplyAction = "http://tempuri.org/IStateWithDecision/RejectResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> RejectAsync(SPPC.Tadbir.Workflow.StateOperation operation);
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IStateWithDecisionChannel : IStateWithDecision, System.ServiceModel.IClientChannel
    {
    }

    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class StateWithDecisionClient : System.ServiceModel.ClientBase<IStateWithDecision>, IStateWithDecision
    {

        public StateWithDecisionClient()
        {
        }

        public StateWithDecisionClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        public StateWithDecisionClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public StateWithDecisionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        public StateWithDecisionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        public int Prepare(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Prepare(operation);
        }

        public System.Threading.Tasks.Task<int> PrepareAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.PrepareAsync(operation);
        }

        public int Review(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Review(operation);
        }

        public System.Threading.Tasks.Task<int> ReviewAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.ReviewAsync(operation);
        }

        public int Approve(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Approve(operation);
        }

        public System.Threading.Tasks.Task<int> ApproveAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.ApproveAsync(operation);
        }

        public int Confirm(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Confirm(operation);
        }

        public System.Threading.Tasks.Task<int> ConfirmAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.ConfirmAsync(operation);
        }

        public int Reject(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Reject(operation);
        }

        public System.Threading.Tasks.Task<int> RejectAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.RejectAsync(operation);
        }
    }
}
