using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class User
    {
        /// <summary>
        /// Gets a collection of existing associations between users and roles
        /// </summary>
        public virtual IList<UserRole> UserRoles { get; protected set; }
    }
}
