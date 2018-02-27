using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// یک لیست اطلاعاتی را به همراه متادیتای اقلام داخل آن نمایش می دهد
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class EntityListViewModel<TModel>
        where TModel : class, new()
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس ایجاد می کند
        /// </summary>
        public EntityListViewModel()
        {
            List = new List<TModel>();
        }

        /// <summary>
        /// لیست اطلاعاتی
        /// </summary>
        public IList<TModel> List { get; set; }

        /// <summary>
        /// متادیتای اقلام داخل لیست اطلاعاتی
        /// </summary>
        public EntityViewModel Metadata { get; set; }
    }
}
