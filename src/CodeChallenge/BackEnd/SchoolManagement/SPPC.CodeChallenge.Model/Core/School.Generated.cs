// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1448
//     Template Version: 1.0
//     Generation Date: 2022-11-08 12:30:07 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using SPPC.CodeChallenge.Model.Metadata;

namespace SPPC.CodeChallenge.Model.Core
{
    /// <summary>
    /// اطلاعات مدرسه را نگهداری می کند
    /// </summary>
    public partial class School : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public School()
        {
            Name = String.Empty;
            AdminSystem = String.Empty;
            Manager = String.Empty;
            Address = String.Empty;
            FoundedDate = DateTime.Now;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام ثبت شده برای مدرسه
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// سیستم مدیریت مدرسه - دولتی، نیمه دولتی، هیئت امنایی یا غیرانتفاعی
        /// </summary>
        public virtual string AdminSystem { get; set; }

        /// <summary>
        /// نام و نام خانوادگی مدیر مدرسه
        /// </summary>
        public virtual string Manager { get; set; }

        /// <summary>
        /// ظرفیت پذیرش دانش آموز در مدرسه
        /// </summary>
        public virtual int Capacity { get; set; }

        /// <summary>
        /// مبلغ شهریه متوسط هر دانش آموز در یک دوره تحصیلی
        /// </summary>
        public virtual decimal Tuition { get; set; }

        /// <summary>
        /// نشانی پستی مدرسه
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// تاریخ تأسیس مدرسه
        /// </summary>
        public virtual DateTime FoundedDate { get; set; }

        /// <summary>
        /// مشخص می کند که مدرسه در فهرست مدارس ثبت شده یا نه
        /// </summary>
        public virtual bool IsListed { get; set; }

        /// <summary>
        /// شهری که مدرسه در آن قرار گرفته است
        /// </summary>
        public virtual City City { get; set; }

        /// <summary>
        /// استانی که مدرسه در آن قرار گرفته است
        /// </summary>
        public virtual Province Province { get; set; }
    }
}