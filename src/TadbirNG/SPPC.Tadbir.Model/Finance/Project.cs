using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Finance
{
    public partial class Project
    {
        /// <summary>
        /// مجموعه ای از پروژه های زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<Project> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی پروژه والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }
    }
}
