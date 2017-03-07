using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    public partial class UserViewModel
    {
        /// <summary>
        /// Gets or sets the first name of the organization person related to this user.
        /// </summary>
        public string PersonFirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the organization person related to this user.
        /// </summary>
        public string PersonLastName { get; set; }
    }
}
