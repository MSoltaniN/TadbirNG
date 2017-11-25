using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class DetailAccount
    {
        public IList<DetailAccount> Children { get; protected set; }
    }
}
