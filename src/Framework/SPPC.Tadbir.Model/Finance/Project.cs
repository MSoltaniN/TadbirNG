using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Project
    {
        /// <summary>
        /// Gets a collection of all projects that are immediately below this item in the project hierarchy
        /// </summary>
        public IList<Project> Children { get; protected set; }
    }
}
