using System;
using System.Activities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using SPPC.Framework.Unity.WF;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Workflow
{
    public class TransactionWorkflow : ITransactionWorkflow, IDisposable
    {
        private TransactionWorkflow()
        {
            _workflows = new Dictionary<int, WorkflowApplication>();
            _dependencyExtension =
                new Lazy<DependencyInjectionExtension>(() => new DependencyInjectionExtension(TypeContainer));
            _syncEvent = new AutoResetEvent(false);
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
            LogOperation(transactionId, "Prepare", "prepared");
        }

        public void Review(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, DocumentStatus.Reviewed);
            LogOperation(transactionId, "Review", "reviewed");
        }

        public void RejectReviewed(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, DocumentStatus.Prepared);
            LogOperation(transactionId, "RejectReview", "rejected");
        }

        public void Confirm(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, DocumentStatus.Confirmed);
            LogOperation(transactionId, "Confirm", "confirmed");
        }

        public void Approve(int transactionId)
        {
            TriggerTransition(transactionId, CurrentUserId, DocumentStatus.Approved);
            LogOperation(transactionId, "Approve", "approved");
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

        #region IDisposable Support

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Supports correct implementation of the Disposable pattern for this class.
        /// </summary>
        /// <param name="disposing">Indicates if this instance is currently being disposed</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _syncEvent.Dispose();
                _disposed = true;
            }
        }

        #endregion

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
            workflow.Completed = new Action<WorkflowApplicationCompletedEventArgs>(OnWorkflowCompleted);
            workflow.Idle = new Action<WorkflowApplicationIdleEventArgs>(OnWorkflowIdle);
            workflow.Run();
            _workflows.Add(transactionId, workflow);
            _syncEvent.WaitOne();
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
                _syncEvent.WaitOne();
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

        private void OnWorkflowIdle(WorkflowApplicationIdleEventArgs args)
        {
            _syncEvent.Set();
        }

        private void LogOperation(int transactionId, string title, string completedText)
        {
            Debug.WriteLine(
                "{0}=================================================================={0}" +
                "{1}: Transaction '[id]={2}' is {3} by user '[id]={4}'.{0}" +
                "==================================================================",
                Environment.NewLine, title, transactionId, completedText, CurrentUserId);
        }

        private static Lazy<TransactionWorkflow> _instance =
            new Lazy<TransactionWorkflow>(() => new TransactionWorkflow());
        private Lazy<DependencyInjectionExtension> _dependencyExtension;
        private IDictionary<int, WorkflowApplication> _workflows;
        private AutoResetEvent _syncEvent;
        private bool _disposed = false;
    }
}
