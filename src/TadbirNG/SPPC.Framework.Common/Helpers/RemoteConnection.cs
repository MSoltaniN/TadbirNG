using System;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// اطلاعات مورد نیاز برای اتصال امن به یک دامنه میزبان را نگهداری می کند
    /// </summary>
    public class RemoteConnection
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RemoteConnection()
        {
            Port = DefaultSshPort;
        }

        /// <summary>
        /// آدرس وب یا آی پی دامنه مورد نظر برای اتصال امن از راه دور
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// شماره پورت مورد نظر برای اتصال
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// نام کاربری مورد استفاده برای اتصال
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// رمز عبور مورد استفاده برای احراز هویت
        /// </summary>
        public string Password { get; set; }

        private const int DefaultSshPort = 22;
    }
}
