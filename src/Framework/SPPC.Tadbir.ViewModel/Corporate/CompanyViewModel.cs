using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.ViewModel.Corporate
{
    public partial class CompanyViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی شرکت والد در ساختار درختی
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// تعداد شرکت زیرمجموعه این شرکت در ساختار درختی
        /// </summary>
        public int ChildCount { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
