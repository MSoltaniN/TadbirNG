using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// عملیات مورد نیاز برای ساختن کلاس های کمکی گزارش گردش و مانده را تعریف می کند
    /// </summary>
    public interface ITestBalanceUtilityFactory
    {
        /// <summary>
        /// کلاس کمکی مربوط به مولفه حساب داده شده را ساخته و برمی گرداند
        /// </summary>
        /// <param name="viewId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <returns>کلاس کمکی مربوط به مولفه حساب</returns>
        ITestBalanceUtility Create(int viewId);
    }
}
