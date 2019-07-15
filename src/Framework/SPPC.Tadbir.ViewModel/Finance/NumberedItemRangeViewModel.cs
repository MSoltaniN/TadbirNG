using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات کلی محدوده ای از موجودیت های شماره گذاری شده را تگهداری می کند
    /// </summary>
    public class NumberedItemRangeViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی نمای اطلاعاتی مرتبط با موجودیت
        /// </summary>
        public int ViewId { get; set; }

        /// <summary>
        /// شماره اولین سطر اطلاعاتی در محدوده
        /// </summary>
        public int FirstNo { get; set; }

        /// <summary>
        /// شماره آخرین سطر اطلاعاتی در محدوده
        /// </summary>
        public int LastNo { get; set; }
    }
}
