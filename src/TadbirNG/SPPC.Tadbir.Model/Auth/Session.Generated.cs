// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1328
//     Template Version: 1.0
//     Generation Date: 2021-11-02 10:28:47 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;

namespace SPPC.Tadbir.Model.Auth
{
    /// <summary>
    /// اطلاعات یک جلسه کاری باز در برنامه را نگهداری می کند
    /// </summary>
    public partial class Session : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Session()
        {
            Device = String.Empty;
            Fingerprint = String.Empty;
            IPAddress = String.Empty;
            TimeZone = String.Empty;
            SinceUtc = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام وسیله مورد استفاده در این جلسه برنامه
        /// </summary>
        public virtual string Device { get; set; }

        /// <summary>
        /// نام استاندارد مرورگر مورد استفاده در این جلسه برنامه
        /// </summary>
        public virtual string Browser { get; set; }

        /// <summary>
        /// شناسه یکتای تولیدشده برای مرورگر مورد استفاده در این جلسه برنامه
        /// </summary>
        public virtual string Fingerprint { get; set; }

        /// <summary>
        /// آدرس آی پی وسیله مورد استفاده
        /// </summary>
        public virtual string IPAddress { get; set; }

        /// <summary>
        /// زمان شروع جلسه برنامه به وقت استاندارد - گرینویچ
        /// </summary>
        public virtual DateTime SinceUtc { get; set; }

        /// <summary>
        /// تاریخ و زمان آخرین فعالیت کاربر در برنامه به وقت استاندارد - گرینویچ
        /// </summary>
        public virtual DateTime LastActivityUtc { get; set; }

        /// <summary>
        /// ناحیه زمانی مورد استفاده توسط وسیله مورد استفاده
        /// </summary>
        public virtual string TimeZone { get; set; }

        /// <summary>
        /// کاربر واردشده به برنامه در این جلسه
        /// </summary>
        public virtual User User { get; set; }
    }
}
