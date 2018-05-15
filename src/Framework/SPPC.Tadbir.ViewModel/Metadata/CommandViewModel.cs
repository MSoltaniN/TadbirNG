using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class CommandViewModel
    {
        /// <summary>
        /// مجموعه ای از دستورات زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<CommandViewModel> Children { get; protected set; }
    }
}
