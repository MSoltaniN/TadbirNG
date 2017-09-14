using System;
using System.Collections.Generic;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Workflow
{
    public interface IDocumentWorkflow
    {
        ISecurityContext CurrentContext { get; set; }

        bool ValidateAction(string documentType, string status, string action);

        void Prepare(int entityId, int documentId, string documentType, string paraph);

        void Review(int entityId, int documentId, string documentType, string paraph);

        void Reject(int entityId, int documentId, string documentType, string paraph);

        void Confirm(int entityId, int documentId, string documentType, string paraph);

        void Approve(int entityId, int documentId, string documentType, string paraph);
    }
}
