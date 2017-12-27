using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Corporate
{
    public partial class Branch
    {
        /// <summary>
        /// Gets a collection of existing associations between roles and branches
        /// </summary>
        public virtual IList<RoleBranch> RoleBranches { get; protected set; }
    }
}
