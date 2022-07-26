using System;

namespace SPPC.Framework.Values
{
    /// <summary>
    /// اطلاعات یک فایل به دست آمده از سرویس وب را نگهداری می کند
    /// </summary>
    public class FileResource
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public FileResource()
        {
        }

        /// <summary>
        /// نام فایل به صورت ارسال شده از سرویس
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// اندازه فایل بر حسب بایت
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// نوع محتوای اطلاعات موجود در فایل - به صورت استاندارد اینترنتی
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// اطلاعات خام فایل به صورت آرایه ای از بایت
        /// </summary>
        public byte[] RawData { get; set; }
    }
}
