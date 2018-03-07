using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Project
    {
        /// <summary>
        /// مجموعه ای از پروژه های زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<Project> Children { get; protected set; }
    }
}
