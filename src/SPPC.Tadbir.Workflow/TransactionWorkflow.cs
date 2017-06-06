using System;
using System.Collections.Generic;
using System.Diagnostics;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    public class TransactionWorkflow : ITransactionWorkflow
    {
        public ISecurityContextManager ContextManager { get; set; }

        public void Prepare(int transactionId)
        {
            var prepare = StateOperation.Prepare(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new StateWithDecisionClient())
            {
                client.Prepare(prepare);
                client.Close();
            }

            LogOperation(transactionId, "Prepare", "prepared");
        }

        public void Review(int transactionId)
        {
            var review = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new StateWithDecisionClient())
            {
                client.Review(review);
                client.Close();
            }

            LogOperation(transactionId, "Review", "reviewed");
        }

        public void RejectReviewed(int transactionId)
        {
            var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new StateWithDecisionClient())
            {
                client.Reject(reject);
                client.Close();
            }

            LogOperation(transactionId, "RejectReview", "rejected");
        }

        public void Confirm(int transactionId)
        {
            var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new StateWithDecisionClient())
            {
                client.Confirm(confirm);
                client.Close();
            }

            LogOperation(transactionId, "Confirm", "confirmed");
        }

        public void Approve(int transactionId)
        {
            var approve = StateOperation.Approve(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new StateWithDecisionClient())
            {
                client.Approve(approve);
                client.Close();
            }

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

        private int CurrentUserId
        {
            get { return ContextManager.CurrentContext.User.Id; }
        }

        private static void InvokeServiceOperation(StateOperation operation)
        {
            using (var client = new CartableClient())
            {
                client.DoRequest(operation);
                client.Close();
            }
        }

        private void LogOperation(int transactionId, string title, string completedText)
        {
            Debug.WriteLine(
                "{0}=================================================================={0}" +
                "{1}: Transaction '[id]={2}' is {3} by user '[id]={4}'.{0}" +
                "==================================================================",
                Environment.NewLine, title, transactionId, completedText, CurrentUserId);
        }
    }
}
