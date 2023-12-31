// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.930
//     Template Version: 1.0
//     Generation Date: 2018-09-23 8:51:04 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.Framework.Domain;

namespace SPPC.Tadbir.Model.Config
{
    /// <summary>
    /// اطلاعات مربوط به بانک اطلاعاتی یک شرکت را نگهداری می کند
    /// </summary>
    public partial class CompanyDb : PCoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public CompanyDb()
        {
            Name = String.Empty;
            DbName = String.Empty;
            Server = String.Empty;
            UserName = String.Empty;
            Password = String.Empty;
            Description = String.Empty;
            //ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام شرکت به صورتی که در لیست شرکت های موجود نمایش داده می شود
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// نام بانک اطلاعاتی شرکت به صورتی که در لیست بانک های اطلاعاتی سرور نمایش داده می شود
        /// </summary>
        public virtual string DbName { get; set; }

        /// <summary>
        /// نام یا آدرس آی پی سرور دیتابیس
        /// </summary>
        public virtual string Server { get; set; }

        /// <summary>
        /// نام کاربری برای اتصال به سرور دیتابیس
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// رمز ورود برای اتصال به سرور دیتابیس
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// وضعیت فعال بودن یا نبودن شرکت - شرکت های حذف شده غیرفعال می شوند
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// اطلاعات تکمیلی در مورد این شرکت
        /// </summary>
        public virtual string Description { get; set; }
    }
}
