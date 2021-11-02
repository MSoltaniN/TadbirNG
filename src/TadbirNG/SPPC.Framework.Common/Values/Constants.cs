using System;

namespace SPPC.Framework.Values
{
    /// <summary>
    /// مقادیر ثابت مورد استفاده در سایر بخش های کد را در یک کلاس مرکزی تعریف می کند
    /// </summary>
    public sealed class Constants
    {
        private Constants()
        {
        }

        /// <summary>
        /// اندازه پیش فرض صفحه در فهرست های اطلاعاتی
        /// </summary>
        public const int GridPageSize = 10;

        /// <summary>
        /// طول پیش فرض برای کلید رمزنگاری مورد استفاده توسط صادرکننده گواهینامه امنیتی
        /// </summary>
        public const int IssuerKeySizeInBits = 4096;


        /// <summary>
        /// طول پیش فرض برای کلید رمزنگاری مورد استفاده برای گواهینامه امنیتی
        /// </summary>
        public const int CertKeySizeInBits = 2048;
    }
}
