using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// شرایط لازم برای کامل بودن بردار حساب را برای یک حساب تعریف می کند
    /// </summary>
    public class FullAccountCriteriaViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FullAccountCriteriaViewModel()
        {
            RequiresDetailAccount = true;
            RequiresCostCenter = true;
            RequiresProject = true;
        }

        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری در بردار حساب مورد نظر
        /// </summary>
        public int AccountId { get; set; }

        /// <summary>
        /// مشخص می کند که آیا مولفه تفصیلی شناور برای کامل بودن بردار حساب ضروری است یا نه؟
        /// </summary>
        public bool RequiresDetailAccount { get; set; }

        /// <summary>
        /// مشخص می کند که آیا مولفه مرکز هزینه برای کامل بودن بردار حساب ضروری است یا نه؟
        /// </summary>
        public bool RequiresCostCenter { get; set; }

        /// <summary>
        /// مشخص می کند که آیا مولفه پروژه برای کامل بودن بردار حساب ضروری است یا نه؟
        /// </summary>
        public bool RequiresProject { get; set; }
    }
}
