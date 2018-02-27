using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// یک رکورد اطلاعاتی را به همراه متادیتای آن نمایش می دهد
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class EntityItemViewModel<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// مدل نمایشی رکورد اطلاعاتی
        /// </summary>
        public TModel Item { get; set; }

        /// <summary>
        /// متادیتای رکورد اطلاعاتی
        /// </summary>
        public EntityViewModel Metadata { get; set; }
    }
}
