using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// نام رکوردهای ردگیری خاص در گردش های کاری را در یک کلاس مرکزی تعریف می کند.
    /// </summary>
    public sealed class WorkflowEvent
    {
        private WorkflowEvent()
        {
        }

        /// <summary>
        /// نام رکوردهای ردگیری که برای اعلام وضعیت گردش های کاری استفاده می شود
        /// </summary>
        public const string StateChanged = "RunningStateEvent";
    }
}
