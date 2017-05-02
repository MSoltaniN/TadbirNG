using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SPPC.Framework.Unity.WF;
using SPPC.Tadbir.Service;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Workflow
{
    public class TransactionWorkflow : ITransactionWorkflow
    {
        private TransactionWorkflow()
        {
            _workflows = new Dictionary<int, WorkflowApplication>();
            _dependencyExtension =
                new Lazy<DependencyInjectionExtension>(() => new DependencyInjectionExtension(TypeContainer));
        }

        public static TransactionWorkflow Instance
        {
            get { return _instance.Value; }
        }

        public object TypeContainer { get; set; }

        public ISecurityContextManager ContextManager { get; set; }

        public void Prepare(int transactionId)
        {
            StartNewWorkflow(transactionId, CurrentUserId);
            Debug.WriteLine(
                "Prepare: Transaction '[id]={0}' is prepared by user '[id]={1}'.", transactionId, CurrentUserId);
        }

        public void Review(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, "Reviewed");
            Debug.WriteLine(
                "Review: Transaction '[id]={0}' is reviewed by user '[id]={1}'.", transactionId, CurrentUserId);
        }

        public void RejectReviewed(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, "Prepared");
            Debug.WriteLine(
                "RejectReview: Transaction '[id]={0}' is rejected by user '[id]={1}'.", transactionId, CurrentUserId);
        }

        public void Confirm(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, "Confirmed");
            Debug.WriteLine(
                "Confirm: Transaction '[id]={0}' is confirmed by user '[id]={1}'.", transactionId, CurrentUserId);
        }

        public void Approve(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, "Approved");
            Debug.WriteLine(
                "Approve: Transaction '[id]={0}' is approved by user '[id]={1}'.", transactionId, CurrentUserId);
        }

        public void PrepareMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        public void ReviewMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        public void RejectReviewedMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        public void ConfirmMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        public void ApproveMultiple(IEnumerable<int> transactions)
        {
            throw new NotImplementedException();
        }

        private int CurrentUserId
        {
            get { return ContextManager.CurrentContext.User.Id; }
        }

        private DependencyInjectionExtension DependencyExtension
        {
            get { return _dependencyExtension.Value; }
        }

        private void StartNewWorkflow(int transactionId, int userId)
        {
            if (_workflows.ContainsKey(transactionId))
            {
                string message = String.Format(
                    "This financial transaction is already inside transaction workflow (Id = {0})", transactionId);
                throw ExceptionBuilder.NewInvalidOperationException(message);
            }

            var workflow = new WorkflowApplication(
                new TransactionStateWorkflow(), new Dictionary<string, object>
                {
                    { "StartedById", userId.ToString() },
                    { "TransactionId", transactionId }
                });
            workflow.Extensions.Add(DependencyExtension);
            workflow.Completed += new Action<WorkflowApplicationCompletedEventArgs>(OnWorkflowCompleted);
            workflow.Run();
            _workflows.Add(transactionId, workflow);
        }

        private void TriggerTransition(int transactionId, int userId, string newState)
        {
            if (_workflows.ContainsKey(transactionId))
            {
                var workflow = _workflows[transactionId];
                string userBookmark = String.Format("GetUser{0}", newState[0]);
                string stateBookmark = String.Format("GetState{0}", newState[0]);
                workflow.ResumeBookmark(userBookmark, userId.ToString());
                workflow.ResumeBookmark(stateBookmark, newState);
            }
        }

        private void OnWorkflowCompleted(WorkflowApplicationCompletedEventArgs args)
        {
            if (args.CompletionState == ActivityInstanceState.Closed)
            {
                var workflowEntry = _workflows
                    .Where(wf => wf.Value.Id == args.InstanceId)
                    .First();
                _workflows.Remove(workflowEntry);
            }
        }

        private static Lazy<TransactionWorkflow> _instance =
            new Lazy<TransactionWorkflow>(() => new TransactionWorkflow());
        private Lazy<DependencyInjectionExtension> _dependencyExtension;
        private IDictionary<int, WorkflowApplication> _workflows;
    }
}
