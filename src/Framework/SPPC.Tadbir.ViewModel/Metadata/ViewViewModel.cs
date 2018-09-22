using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class ViewViewModel
    {
        /// <summary>
        /// مجموعه ای از ویژگی های تعریف شده برای موجودیت
        /// </summary>
        public IList<ColumnViewModel> Columns { get; protected set; }
    }
}
