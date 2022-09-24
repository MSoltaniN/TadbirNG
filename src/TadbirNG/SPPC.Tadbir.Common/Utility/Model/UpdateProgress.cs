using System;

namespace SPPC.Tadbir.Utility.Model
{
    /// <summary>
    /// اطلاعات وضعیت پیشرفت عملیات را در فرایند به روزرسانی برنامه نگهداری می کند
    /// </summary>
    public class UpdateProgress
    {
        /// <summary>
        /// درصد پیشرفت در مرحله دانلود سرویس ها از سرور به روزرسانی
        /// </summary>
        public int DownloadProgress { get; set; }

        /// <summary>
        /// درصد پیشرفت در مرحله تهیه پشتیبان از سرویس های داکری نصب شده
        /// </summary>
        public int BackupProgress { get; set; }

        /// <summary>
        /// درصد پیشرفت در مرحله نصب سرویس های جدید که از سرور به روزرسانی دانلود شده
        /// </summary>
        public int SetupProgress { get; set; }
    }
}
