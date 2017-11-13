using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Role Role { get; set; }
        public User User { get; set; }
    }
}
