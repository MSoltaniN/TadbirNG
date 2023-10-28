using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Config
{
    /// <summary>
    /// اطلاعات یکی از تنظیمات برنامه را نگهداری می کند
    /// </summary>
    public partial class Setting : CoreEntity
    {
        
        /// <summary>
        /// شناسه تنظیمات والد برای این تنظیمات در ساختار درختی
        /// </summary>
        public virtual int? ParentId { get; set; }
    }
}
