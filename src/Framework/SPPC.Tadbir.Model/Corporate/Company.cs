using System;
using System.Collections.Generic;
using System.Text;

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

        public override string ToString()
        {
            return Name;
        }
    }
}
