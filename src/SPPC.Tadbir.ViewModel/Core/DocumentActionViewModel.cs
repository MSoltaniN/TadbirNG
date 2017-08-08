using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    public partial class DocumentActionViewModel
    {
        public string ModifiedDate { get; set; }

        public int CreatedById { get; set; }

        public int ModifiedById { get; set; }

        public int ConfirmedById { get; set; }

        public int ApprovedById { get; set; }

        public int LineId { get; set; }
    }
}
