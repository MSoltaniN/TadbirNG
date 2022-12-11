using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model.Auth;

namespace SPPC.Tadbir.Model.Reporting
{
    public partial class Widget
    {
        /// <summary>
        /// شناسه دیتابیسی تابع محاسباتی استفاده شده در این ویجت
        /// </summary>
        public virtual int FunctionId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی نوع انتخاب شده برای ویجت
        /// </summary>
        public virtual int TypeId { get; set; }

        /// <summary>
        /// Get a collection of existing associations between roles and widgets  
        /// </summary>
        public virtual IList<RoleWidget> RoleWidgets{ get; protected set; }
    }
}
