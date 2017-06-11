using System;
using System.Collections.Generic;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    public class TransactionBasicWorkflow : TransactionWorkflow, ITransactionWorkflow
    {
        public override void Prepare(int transactionId)
        {
            var prepare = StateOperation.Prepare(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(prepare);
            LogOperation(transactionId, "Prepare", "prepared");
        }

        public override void Review(int transactionId)
        {
            var review = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(review);
            LogOperation(transactionId, "Review", "reviewed");
        }

        public override void RejectReviewed(int transactionId)
        {
            var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(reject);
            LogOperation(transactionId, "Reject", "rejected");
        }

        public override void Confirm(int transactionId)
        {
            var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(confirm);
            LogOperation(transactionId, "Confirm", "confirmed");
        }

        public override void Approve(int transactionId)
        {
            var approve = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(approve);
            LogOperation(transactionId, "Approve", "approved");
        }

        private static void InvokeServiceOperation(StateOperation operation)
        {
            using (var client = new DocumentStateBasicClient())
            {
                client.DoRequest(operation);
                client.Close();
            }
        }
    }
}
