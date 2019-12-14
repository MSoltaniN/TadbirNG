using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel
{
    /// <summary>
    /// اطلاعات نمایشی مورد نیاز برای کار با یک بردار حساب را تعریف می کند
    /// </summary>
    public interface IAccountView
    {
        /// <summary>
        /// کد کامل مولفه سرفصل حسابداری در بردار حساب
        /// </summary>
        string AccountFullCode { get; }

        /// <summary>
        /// کد کامل مولفه تفصیلی شناور در بردار حساب
        /// </summary>
        string DetailAccountFullCode { get; }

        /// <summary>
        /// کد کامل مولفه مرکز هزینه در بردار حساب
        /// </summary>
        string CostCenterFullCode { get; }

        /// <summary>
        /// کد کامل مولفه پروژه در بردار حساب
        /// </summary>
        string ProjectFullCode { get; }
    }
}
