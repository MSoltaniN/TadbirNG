using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    public sealed class WorkflowEvent
    {
        private WorkflowEvent()
        {
        }

        public const string StateChanged = "RunningStateEvent";
    }
}
