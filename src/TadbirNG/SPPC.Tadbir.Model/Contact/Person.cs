using SPPC.Tadbir.Model.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Model.Contact
{
    /// <summary>
    /// Represents information about a business person
    /// </summary>
    public partial class Person : CoreEntity
    {
        /// <summary>
        /// Gets or sets the application user Id that represents this user in security subsystem
        /// </summary>
        public virtual int UserId { get; set; }
    }
}
