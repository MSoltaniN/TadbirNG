using System;
using System.Collections.Generic;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    public class TransactionDecisionWorkflow : TransactionWorkflow, ITransactionWorkflow
    {
        public override void Prepare(int transactionId)
        {
            var prepare = StateOperation.Prepare(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Prepare(prepare);
                client.Close();
            }

            LogOperation(transactionId, "Prepare", "prepared");
        }

        public override void Review(int transactionId)
        {
            var review = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Review(review);
                client.Close();
            }

            LogOperation(transactionId, "Review", "reviewed");
        }

        public override void RejectReviewed(int transactionId)
        {
            var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Reject(reject);
                client.Close();
            }

            LogOperation(transactionId, "RejectReview", "rejected");
        }

        public override void Confirm(int transactionId)
        {
            var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Confirm(confirm);
                client.Close();
            }

            LogOperation(transactionId, "Confirm", "confirmed");
        }

        public override void Approve(int transactionId)
        {
            var approve = StateOperation.Approve(CurrentUserId, transactionId, DocumentType.Transaction);
            using (var client = new DocumentStateDecisionClient())
            {
                client.Approve(approve);
                client.Close();
            }

            LogOperation(transactionId, "Approve", "approved");
        }
    }
}
