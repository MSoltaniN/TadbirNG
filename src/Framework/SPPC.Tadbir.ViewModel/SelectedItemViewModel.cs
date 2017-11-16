using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// این مدل نمایشی امکان انتخاب ردیف های اطلاعاتی را در فرم های لیستی فراهم می کند.
    /// </summary>
    public class SelectedItemViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی موجودیت قابل انتخاب
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مشخص می کند که ردیف مورد نظر انتخاب شده یا نه
        /// </summary>
        public bool IsSelected { get; set; }
    }
}
