using System;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class ChartParameter
    {
        /// <summary>
        /// شناسه دیتابیسی نموداری که این پارامتر برای آن تعریف شده است
        /// </summary>
        public virtual int ChartId { get; set; }
    }
}
