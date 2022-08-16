using System;

namespace SPPC.Tadbir.Configuration
{
    public class DbParameters
    {
        public string LoginName { get; set; }

        public string Password { get; set; }

        public string SysDbName { get; set; }

        public string FirstDbName { get; set; }

        public string FirstCompanyName { get; set; }

        public string AdminUserName { get; set; }

        public string AdminPassword { get; set; }

        public string AdminPasswordHash { get; set; }

        public string AdminFirstName { get; set; }

        public string AdminLastName { get; set; }
    }
}
