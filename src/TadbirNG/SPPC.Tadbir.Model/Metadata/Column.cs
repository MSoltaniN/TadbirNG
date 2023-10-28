using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Metadata
{
    /// <summary>
    /// اطلاعات فراداده ای یک ستون در یک نمای اطلاعاتی را نگهداری می کند
    /// </summary>
    public partial class Column : PCoreEntity
    {
        /// <summary>
        /// شناسه موجودیتی که این ستون در آن تعریف شده است
        /// </summary>
        public virtual int? ViewId { get; set; }
    }
}
