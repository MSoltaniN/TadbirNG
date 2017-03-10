﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Values
{
    /// <summary>
    /// Provides definitions for special-purpose constant values.
    /// </summary>
    public sealed class Constants
    {
        private Constants()
        {
        }

        /// <summary>
        /// Special-purpose name of the system administrator user
        /// </summary>
        public const string AdminUserName = "admin";

        /// <summary>
        /// Dummy text to display in password boxes
        /// </summary>
        public const string DummyPassword = "************";

        /// <summary>
        /// Special identifier for System Administrator role in security system
        /// </summary>
        public const int AdminRoleId = 1;
    }
}
