using System;
using System.Collections.Generic;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class ColumnMetaModel
    {
        public int ViewId { get; set; }

        public ColumnViewModel Column { get; set; }
    }
}
