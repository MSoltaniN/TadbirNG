using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// مدل نمایشی برای تمام سطرهای اطلاعاتی مرتبط با یک موجودیت اصلی
    /// </summary>
    /// <remarks>
    /// سطرهای اطلاعاتی مرتبط با یک موجودیت اصلی در جداول واسط نگهداری شده و شامل ارتباطاتی مانند
    /// کاربران یک نقش، دوره های مالی و شعبه های قابل دسترسی توسط یک نقش می شوند.
    /// </remarks>
    public class RelatedItemsViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RelatedItemsViewModel()
        {
            RelatedItems = new List<RelatedItemViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی موجودیت اصلی
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مجموعه ای از سطرهای اطلاعاتی مرتبط با موجودیت اصلی
        /// </summary>
        public IList<RelatedItemViewModel> RelatedItems { get; private set; }
    }
}
