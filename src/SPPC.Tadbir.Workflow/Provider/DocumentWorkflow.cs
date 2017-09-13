using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SPPC.Tadbir.Metadata.Workflow;
using SPPC.Tadbir.Repository;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.Workflow
{
    public class DocumentWorkflow : IDocumentWorkflow
    {
        public DocumentWorkflow(IMetadataRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// اطلاعات امنیتی کاربر جاری در برنامه
        /// </summary>
        public ISecurityContext CurrentContext { get; set; }

        public bool ValidateAction(string documentType, string status, string action)
        {
            var metadata = _repository.GetStateWorkflow(documentType);
            var existing = metadata.AllActions
                .Where(act => act.Name == action && act.FromDocumentStatus == status)
                .FirstOrDefault();
            return (existing != null);
        }

        public void Prepare(int documentId, string documentType, string paraph)
        {
            var metadata = _repository.GetStateWorkflow(documentType);
            var prepare = StateOperation.Prepare(CurrentUserId, 0, documentId, documentType, paraph);
            using (var client = new DocumentStateServiceClient())
            {
                var workflowAction = GetWorkflowAction(DocumentActionName.Prepare, metadata, client);
                workflowAction(prepare);
                client.Close();
            }

            LogOperation(documentId, DocumentActionName.Prepare, "prepared");
        }

        public void Review(int documentId, string documentType, string paraph)
        {
            var metadata = _repository.GetStateWorkflow(documentType);
            var review = StateOperation.Review(CurrentUserId, 0, documentId, documentType, paraph);
            using (var client = new DocumentStateServiceClient())
            {
                var workflowAction = GetWorkflowAction(DocumentActionName.Review, metadata, client);
                workflowAction(review);
                client.Close();
            }

            LogOperation(documentId, DocumentActionName.Review, "reviewed");
        }

        public void Reject(int documentId, string documentType, string paraph)
        {
            var metadata = _repository.GetStateWorkflow(documentType);
            var reject = StateOperation.RejectReview(CurrentUserId, 0, documentId, documentType, paraph);
            using (var client = new DocumentStateServiceClient())
            {
                var workflowAction = GetWorkflowAction(DocumentActionName.Reject, metadata, client);
                workflowAction(reject);
                client.Close();
            }

            LogOperation(documentId, DocumentActionName.Reject, "rejected");
        }

        public void Confirm(int documentId, string documentType, string paraph)
        {
            var metadata = _repository.GetStateWorkflow(documentType);
            var confirm = StateOperation.Confirm(CurrentUserId, 0, documentId, documentType, paraph);
            using (var client = new DocumentStateServiceClient())
            {
                var workflowAction = GetWorkflowAction(DocumentActionName.Confirm, metadata, client);
                workflowAction(confirm);
                client.Close();
            }

            LogOperation(documentId, DocumentActionName.Confirm, "confirmed");
        }

        public void Approve(int documentId, string documentType, string paraph)
        {
            var metadata = _repository.GetStateWorkflow(documentType);
            var approve = StateOperation.Approve(CurrentUserId, 0, documentId, documentType, paraph);
            using (var client = new DocumentStateServiceClient())
            {
                var workflowAction = GetWorkflowAction(DocumentActionName.Approve, metadata, client);
                workflowAction(approve);
                client.Close();
            }

            LogOperation(documentId, DocumentActionName.Approve, "approved");
        }

        private int CurrentUserId
        {
            get { return CurrentContext.User.Id; }
        }

        private static bool IsFirstAction(string action, StateWorkflow metadata)
        {
            return (metadata.FirstAction.Name == action);
        }

        private static bool IsNextAction(string action, StateWorkflow metadata)
        {
            var nextAction = metadata.NextActions
                .Where(act => act.Name == action && act.IsReverse == false)
                .FirstOrDefault();
            return (nextAction != null);
        }

        private static bool IsReverseAction(string action, StateWorkflow metadata)
        {
            var reverseAction = metadata.NextActions
                .Where(act => act.Name == action && act.IsReverse == true)
                .FirstOrDefault();
            return (reverseAction != null);
        }

        private WorkflowAction GetWorkflowAction(
            string action, StateWorkflow metadata, DocumentStateServiceClient client)
        {
            WorkflowAction workflowAction;
            if (IsFirstAction(action, metadata))
            {
                workflowAction = client.FirstAction;
            }
            else if (IsNextAction(action, metadata))
            {
                workflowAction = client.NextAction;
            }
            else if (IsReverseAction(action, metadata))
            {
                workflowAction = client.ReverseAction;
            }
            else
            {
                workflowAction = null;
            }

            return workflowAction;
        }

        /// <summary>
        /// لاگ مربوط به عملیات را در نمای خروجی ویژوال استودیو ایجاد می کند.
        /// </summary>
        /// <param name="documentId">شناسه دیتابیسی سند مالی مورد اقدام</param>
        /// <param name="title">نام انگلیسی اقدام جاری</param>
        /// <param name="completedText"></param>
        private void LogOperation(int documentId, string title, string completedText)
        {
            Debug.WriteLine(
                "{0}=================================================================={0}" +
                "{1}: Document '[id]={2}' is {3} by user '[id]={4}'.{0}" +
                "==================================================================",
                Environment.NewLine, title, documentId, completedText, CurrentUserId);
        }

        private IMetadataRepository _repository;
    }

    public delegate int WorkflowAction(StateOperation operation);
}
