using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SPPC.Framework.Unity.WF;

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

        public void Prepare(int transactionId, int userId)
        {
            StartNewWorkflow(transactionId, userId);
            Debug.WriteLine("Prepare: Transaction '[id]={0}' is prepared by user '[id]={1}'.", transactionId, userId);
        }

        public void Review(int transactionId, int userId)
        {
            TriggerTransition(transactionId, userId, "Reviewed");
            Debug.WriteLine("Review: Transaction '[id]={0}' is reviewed by user '[id]={1}'.", transactionId, userId);
        }

        public void RequestRevision(int transactionId, int userId)
        {
            TriggerTransition(transactionId, userId, "Prepared");
            Debug.WriteLine("RejectReview: Transaction '[id]={0}' is rejected by user '[id]={1}'.", transactionId, userId);
        }

        public void Confirm(int transactionId, int userId)
        {
            TriggerTransition(transactionId, userId, "Confirmed");
            Debug.WriteLine("Confirm: Transaction '[id]={0}' is confirmed by user '[id]={1}'.", transactionId, userId);
        }

        public void Approve(int transactionId, int userId)
        {
            TriggerTransition(transactionId, userId, "Approved");
            Debug.WriteLine("Approve: Transaction '[id]={0}' is approved by user '[id]={1}'.", transactionId, userId);
        }

        public void PrepareMultiple(IEnumerable<int> transactions, int userId)
        {
            throw new NotImplementedException();
        }

        public void ReviewMultiple(IEnumerable<int> transactions, int userId)
        {
            throw new NotImplementedException();
        }

        public void RequestRevisionMultiple(IEnumerable<int> transactions, int userId)
        {
            throw new NotImplementedException();
        }

        public void ConfirmMultiple(IEnumerable<int> transactions, int userId)
        {
            throw new NotImplementedException();
        }

        public void ApproveMultiple(IEnumerable<int> transactions, int userId)
        {
            throw new NotImplementedException();
        }

        private DependencyInjectionExtension DependencyExtension
        {
            get { return _dependencyExtension.Value; }
        }

        private void StartNewWorkflow(int transactionId, int userId)
        {
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
