using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class RoleBranch
    {
        public int RoleBranchId { get; set; }
        public int RoleId { get; set; }
        public int BranchId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Branch Branch { get; set; }
        public Role Role { get; set; }
    }
}
