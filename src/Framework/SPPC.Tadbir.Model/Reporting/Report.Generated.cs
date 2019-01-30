// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.494
//     Template Version: 1.0
//     Generation Date: 2019-01-19 10:36:48 AM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using SPPC.Framework.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Model.Reporting
{
    /// <summary>
    /// اطلاعات یک گزارش در برنامه را نگهداری می کند
    /// </summary>
    public partial class Report : IEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Report()
        {
            Code = String.Empty;
            Template = String.Empty;
            TemplateLtr = String.Empty;
            ResourceKeys = String.Empty;
            ServiceUrl = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت که به صورت خودکار توسط دیتابیس تولید می شود
        /// </summary>
        public virtual int Id { get; set; }

        /// <summary>
        /// کد شناسایی گزارش سیستمی در زیرساخت گزارشات
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// آدرس مورد نیاز برای خواندن اطلاعات گزارش از سرویس وب
        /// </summary>
        public virtual string ServiceUrl { get; set; }

        /// <summary>
        /// مشخص می کند که آیا شاخه مورد نظر مربوط به گروه بندی گزارشات است یا نه؟
        /// </summary>
        public virtual bool IsGroup { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر سیستمی است یا توسط کاربر ذخیره شده است
        /// </summary>
        public virtual bool IsSystem { get; set; }

        /// <summary>
        /// مشخص می کند که آیا گزارش مورد نظر حالت پیش فرض دارد یا نه؟
        /// </summary>
        public virtual bool IsDefault { get; set; }

        /// <summary>
        /// محتوای الگوی طراحی شده برای نمایش گزارش در برنامه - برای چیدمان راست به چپ
        /// </summary>
        public virtual string Template { get; set; }

        /// <summary>
        /// محتوای الگوی طراحی شده برای نمایش گزارش در برنامه - برای چیدمان چپ به راست
        /// </summary>
        public virtual string TemplateLtr { get; set; }

        /// <summary>
        /// مجموعه کلیدهای متن های چند زبانه مورد نیاز در گزارش که با کاراکتر جداکننده از یکدیگر تفکیک شده اند
        /// </summary>
        public virtual string ResourceKeys { get; set; }

        /// <summary>
        /// شناسه زیرسیستم گزارش
        /// </summary>
        public virtual int SubsystemId { get; set; }

        /// <summary>
        /// شناسه یکتای ردیف دیتابیسی که به صورت خودکار توسط دیتابیس مقداردهی می شود
        /// </summary>
        public virtual Guid RowGuid { get; set; }

        /// <summary>
        /// تاریخ آخرین تغییر رکورد دیتابیس که به صورت خودکار توسط ابزار دسترسی به داده مقداردهی می شود
        /// </summary>
        public virtual DateTime ModifiedDate { get; set; }

        /// <summary>
        /// گروه بندی اصلی این گزارش در ساختار درختی گزارشات
        /// </summary>
        public virtual Report Parent { get; set; }

        /// <summary>
        /// فرمی که به عنوان منبع داده اصلی این گزارش در برنامه شناخته می شود
        /// </summary>
        public virtual ReportView View { get; set; }

        /// <summary>
        /// کاربر ایجادکننده یک گزارش ذخیره شده کاربری
        /// </summary>
        public virtual User CreatedBy { get; set; }

        /// <summary>
        /// مجوعه ای از گزارش های محلی شده برای این گزارش
        /// </summary>
        public virtual IList<LocalReport> LocalReports { get; protected set; }

        /// <summary>
        /// مجموعه ای از پاراکترهای مورد نیاز برای پیش نمایش یا چاپ گزارش
        /// </summary>
        public virtual IList<Parameter> Parameters { get; protected set; }

        private void InitReferences()
        {
            View = new ReportView();
            CreatedBy = new User();
            LocalReports = new List<LocalReport>();
            Parameters = new List<Parameter>();
        }
    }
}
