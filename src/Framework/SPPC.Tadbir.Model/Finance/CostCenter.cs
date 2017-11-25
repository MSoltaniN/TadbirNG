using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class CostCenter
    {
        public IList<CostCenter> Children { get; protected set; }
    }
}
