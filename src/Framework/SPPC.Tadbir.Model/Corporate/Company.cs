using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Corporate
{
    public partial class Company
    {
        /// <summary>
        /// مجموعه ای از شرکت های زیرشاخه (فرزند) در ساختار درختی
        /// </summary>
        public IList<Company> Children { get; protected set; }

        /// <summary>
        /// شناسه دیتابیسی شرکت والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// یک رشته متنی برای این آبجکت برمی گرداند
        /// </summary>
        /// <returns>رشته متنی برای این آبجکت</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
