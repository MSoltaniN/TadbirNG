using System;
using System.Collections.Generic;
using SPPC.Tadbir.Configuration;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای ذخیره و بازیابی تنظیمات برنامه را تعریف می کند
    /// </summary>
    public interface IConfigRepository
    {
        /// <summary>
        /// تنظیمات موجود برای مدیریت ارتباطات بین مولفه های بردار حساب را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <returns></returns>
        RelationsConfig GetRelationsConfig();
    }
}
