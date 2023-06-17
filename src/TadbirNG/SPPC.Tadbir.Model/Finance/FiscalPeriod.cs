using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class FiscalPeriod
    {
        /// <summary>
        /// Gets a collection of existing associations between roles and fiscal periods
        /// </summary>
        public virtual IList<RoleFiscalPeriod> RoleFiscalPeriods { get; protected set; }
    }
}
