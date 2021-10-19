using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Config
{
    public partial class CompanyDb
    {
        /// <summary>
        /// Gets a collection of existing associations between roles and companies
        /// </summary>
        public virtual IList<RoleCompany> RoleCompanies { get; protected set; }
    }
}
