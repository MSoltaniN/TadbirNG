using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Workflow
{
    internal sealed class StateOperationName
    {
        private StateOperationName()
        {
        }

        public const string Prepare = "Prepare";
        public const string Review = "Review";
        public const string Reject = "Reject";
        public const string Confirm = "Confirm";
        public const string Approve = "Approve";
    }
}
