using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// اطلاعات نمایشی مشترک بین همه مدل های نمایشی را تعریف می کند
    /// </summary>
    public class ViewModelBase
    {
        /// <summary>
        /// شماره ردیف سطر اطلاعاتی در لیست اطلاعاتی با مرتب سازی پیش فرض
        /// </summary>
        public int RowNo { get; set; }
    }
}
