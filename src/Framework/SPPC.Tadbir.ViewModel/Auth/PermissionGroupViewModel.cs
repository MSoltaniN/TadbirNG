using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    public partial class PermissionGroupViewModel
    {
        /// <summary>
        /// مجموعه دسترسی های تعریف شده در این گروه دسترسی
        /// </summary>
        public List<PermissionViewModel> Permissions { get; }
    }
}
