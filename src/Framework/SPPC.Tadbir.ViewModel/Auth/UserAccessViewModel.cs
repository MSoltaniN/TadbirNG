using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// اطلاعات خلاصه دسترسی های امنیتی کاربر را نگهداری می کند
    /// </summary>
    public class UserAccessViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public UserAccessViewModel()
        {
            Roles = new List<int>();
            Branches = new List<int>();
            FiscalPeriods = new List<int>();
        }

        /// <summary>
        /// شناسه دیتابیسی کاربر
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی نقش های اختصاص داده شده به کاربر
        /// </summary>
        public IList<int> Roles { get; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی شعب سازمانی که کاربر به آنها دسترسی دارد
        /// </summary>
        public IList<int> Branches { get; }

        /// <summary>
        /// مجموعه ای از شناسه های دیتابیسی دوره های مالی که کاربر به اطلاعات آنها دسترسی دارد
        /// </summary>
        public IList<int> FiscalPeriods { get; }
    }
}
