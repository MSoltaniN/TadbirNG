﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class User
    {
        public IList<UserRole> UserRoles { get; protected set; }
    }
}
