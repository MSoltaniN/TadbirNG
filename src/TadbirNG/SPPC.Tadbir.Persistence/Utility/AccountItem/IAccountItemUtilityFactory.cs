using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// عملیات مورد نیاز برای ساختن کلاس کمکی محاسبات مالی مولفه های حساب را تعریف می کند
    /// </summary>
    public interface IAccountItemUtilityFactory
    {
        /// <summary>
        /// کلاس کمکی را برای مولفه حساب با شناسه داده می سازد
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب</param>
        /// <returns>کلاس کمکی ساخته شده</returns>
        IAccountItemUtility Create(int viewId);
    }
}
