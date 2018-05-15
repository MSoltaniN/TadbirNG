using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class CommandViewModel
    {
        public int? PermissionId { get; set; }

        /// <summary>
        /// مجموعه ای از دستورات زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<CommandViewModel> Children { get; protected set; }
    }
}
