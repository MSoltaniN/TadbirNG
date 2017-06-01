﻿using System;
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
            InvokeServiceOperation(prepare);
            LogOperation(transactionId, "Prepare", "prepared");
        }

        public void Review(int transactionId)
        {
            var review = StateOperation.Review(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(review);
            LogOperation(transactionId, "Review", "reviewed");
        }

        public void RejectReviewed(int transactionId)
        {
            var reject = StateOperation.RejectReview(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(reject);
            LogOperation(transactionId, "RejectReview", "rejected");
        }

        public void Confirm(int transactionId)
        {
            var confirm = StateOperation.Confirm(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(confirm);
            LogOperation(transactionId, "Confirm", "confirmed");
        }

        public void Approve(int transactionId)
        {
            var approve = StateOperation.Approve(CurrentUserId, transactionId, DocumentType.Transaction);
            InvokeServiceOperation(approve);
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
