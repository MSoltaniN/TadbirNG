using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// مدل نمایشی برای تمام سطرهای اطلاعاتی مرتبط با یک نقش
    /// </summary>
    /// <remarks>
    /// سطرهای اطلاعاتی مرتبط با یک نقش در جداول واسط نگهداری شده و شامل کاربران یک نقش،
    /// دوره های مالی و شعبه های قابل دسترسی توسط یک نقش می شوند.
    /// </remarks>
    public class RoleItemsViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RoleItemsViewModel()
        {
            RelatedItems = new List<RelatedItemViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی یکی از نقش های موجود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی سطرهای اطلاعاتی مرتبط با این نقش
        /// </summary>
        public IList<RelatedItemViewModel> RelatedItems { get; private set; }
    }
}
