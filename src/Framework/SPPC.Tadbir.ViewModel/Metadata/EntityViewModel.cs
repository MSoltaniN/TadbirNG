using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    public partial class EntityViewModel
    {
        /// <summary>
        /// مجموعه ای از ویژگی های تعریف شده برای موجودیت
        /// </summary>
        public IList<PropertyViewModel> Properties { get; protected set; }
    }
}
