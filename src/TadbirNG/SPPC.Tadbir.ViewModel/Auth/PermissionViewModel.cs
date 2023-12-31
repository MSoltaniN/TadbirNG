﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    public partial class PermissionViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the subsystem in which containing permission group is defined
        /// </summary>
        public int GroupSubsystemId { get; set; }

        /// <summary>
        /// Gets or sets the name of the subsystem in which containing permission group is defined
        /// </summary>
        public string GroupSubsystemName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the operation source type of containing permission group
        /// </summary>
        public int GroupSourceTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the operation source type of containing permission group
        /// </summary>
        public string GroupSourceTypeName { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the permission group in which this permission is defined
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Gets or sets the name of the permission group in which this permission is defined
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates if this permission is assigned to a role
        /// </summary>
        public bool IsEnabled { get; set; }
    }
}
