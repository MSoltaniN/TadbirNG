using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    /// <summary>
    /// اطلاعات یک نمونه نصب شده از برنامه را نگهداری می کند
    /// </summary>
    public class ClientInstanceModel
    {
        public ClientInstanceModel()
        {
            BaseUrl = "http://";
            LicenseServerUrl = "http://";
        }

        /// <summary>
        /// آدرس پایه سرویس وب در نمونه نصب شده
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// آدرس وب سرور آفلاین مدیریت مجوزها در نمونه نصب شده
        /// </summary>
        public string LicenseServerUrl { get; set; }

        /// <summary>
        /// شناسه برنامه نصب شده
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// نسخه برنامه نصب شده
        /// </summary>
        public string Version { get; set; }
    }
}
