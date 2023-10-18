using SPPC.Tadbir.Model.Auth;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        ///
        [Column("UserID")]
        public virtual int UserId { get; set; }
    }
}
