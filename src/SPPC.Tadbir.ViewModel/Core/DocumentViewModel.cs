using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Core
{
    public partial class DocumentViewModel
    {
        public int TypeId { get; set; }

        public int StatusId { get; set; }

        public IList<DocumentActionViewModel> Actions { get; protected set; }
    }
}
