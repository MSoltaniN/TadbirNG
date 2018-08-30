using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Corporate
{
    public partial class Branch
    {
        /// <summary>
        /// شناسه دیتابیسی شرکتی که این شعبه برای آن تعریف شده است
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// مجموعه ای از شعب سازمانی زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<Branch> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه سازمانی والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets a collection of existing associations between roles and branches
        /// </summary>
        public virtual IList<RoleBranch> RoleBranches { get; protected set; }
    }
}
