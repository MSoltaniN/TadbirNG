using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
