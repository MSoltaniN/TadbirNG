using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class FiscalPeriod
    {
        /// <summary>
        /// شناسه دیتابیسی شرکتی که این دوره مالی برای آن تعریف شده است
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Gets a collection of existing associations between roles and fiscal periods
        /// </summary>
        public virtual IList<RoleFiscalPeriod> RoleFiscalPeriods { get; protected set; }
    }
}
