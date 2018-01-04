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
    /// <summary>
    /// Generated Code
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "IDocumentStateDecision")]
    public interface IDocumentStateDecision
    {

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Prepare", ReplyAction = "http://tempuri.org/IDocumentStateDecision/PrepareResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Prepare(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Prepare", ReplyAction = "http://tempuri.org/IDocumentStateDecision/PrepareResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> PrepareAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Review", ReplyAction = "http://tempuri.org/IDocumentStateDecision/ReviewResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Review(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Review", ReplyAction = "http://tempuri.org/IDocumentStateDecision/ReviewResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> ReviewAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Approve", ReplyAction = "http://tempuri.org/IDocumentStateDecision/ApproveResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Approve(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Approve", ReplyAction = "http://tempuri.org/IDocumentStateDecision/ApproveResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> ApproveAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Confirm", ReplyAction = "http://tempuri.org/IDocumentStateDecision/ConfirmResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Confirm(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Confirm", ReplyAction = "http://tempuri.org/IDocumentStateDecision/ConfirmResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> ConfirmAsync(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Reject", ReplyAction = "http://tempuri.org/IDocumentStateDecision/RejectResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        int Reject(SPPC.Tadbir.Workflow.StateOperation operation);

        /// <summary>
        /// Generated Code
        /// </summary>
        [System.ServiceModel.OperationContractAttribute(Action = "http://tempuri.org/IDocumentStateDecision/Reject", ReplyAction = "http://tempuri.org/IDocumentStateDecision/RejectResponse")]
        [return: System.ServiceModel.MessageParameterAttribute(Name = "resultCode")]
        System.Threading.Tasks.Task<int> RejectAsync(SPPC.Tadbir.Workflow.StateOperation operation);
    }

    /// <summary>
    /// Generated Code
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDocumentStateDecisionChannel : IDocumentStateDecision, System.ServiceModel.IClientChannel
    {
    }

    /// <summary>
    /// Generated Code
    /// </summary>
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DocumentStateDecisionClient : System.ServiceModel.ClientBase<IDocumentStateDecision>, IDocumentStateDecision
    {

        /// <summary>
        /// Generated Code
        /// </summary>
        public DocumentStateDecisionClient()
        {
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public DocumentStateDecisionClient(string endpointConfigurationName) :
                base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public DocumentStateDecisionClient(string endpointConfigurationName, string remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public DocumentStateDecisionClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) :
                base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public DocumentStateDecisionClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) :
                base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public int Prepare(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Prepare(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public System.Threading.Tasks.Task<int> PrepareAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.PrepareAsync(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public int Review(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Review(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public System.Threading.Tasks.Task<int> ReviewAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.ReviewAsync(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public int Approve(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Approve(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public System.Threading.Tasks.Task<int> ApproveAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.ApproveAsync(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public int Confirm(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Confirm(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public System.Threading.Tasks.Task<int> ConfirmAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.ConfirmAsync(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public int Reject(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.Reject(operation);
        }

        /// <summary>
        /// Generated Code
        /// </summary>
        public System.Threading.Tasks.Task<int> RejectAsync(SPPC.Tadbir.Workflow.StateOperation operation)
        {
            return base.Channel.RejectAsync(operation);
        }
    }
}
