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

        /// <summary>
        /// شناسه دیتابیسی پروژه والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// مجموعه ای از سرفصل های حسابداری مرتبط با این پروژه
        /// </summary>
        public IList<AccountProject> AccountProjects { get; protected set; }
    }
}
