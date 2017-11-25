using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Project
    {
        public IList<Project> Children { get; protected set; }
    }
}
