using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Corporate
{
    public partial class Branch
    {
        public virtual IList<RoleBranch> RoleBranches { get; protected set; }
    }
}
