// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.338
//     Template Version: 1.0
//     Generation Date: 2018-07-11 3:20:59 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Model.Config
{
    /// <summary>
    /// اطلاعات یکی از تنظیمات برنامه را نگهداری می کند
    /// </summary>
    public partial class Setting : CoreEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public Setting()
        {
            TitleKey = String.Empty;
            ModelType = String.Empty;
            Values = String.Empty;
            DefaultValues = String.Empty;
            ModifiedDate = DateTime.Now;
            InitReferences();
        }

        /// <summary>
        /// نام زیرسیستم مورد نیاز برای تنظیمات - مورد استفاده در تنظیمات خاص یک زیرسیستم
        /// </summary>
        public virtual string Subsystem { get; set; }

        /// <summary>
        /// شناسه متن چندزبانه برای عنوان تنظیمات در واسط کاربری
        /// </summary>
        public virtual string TitleKey { get; set; }

        /// <summary>
        /// نوع تنظیمات که مشخص کننده سطح دسترسی برای مدیریت و تغییر تنظیمات است
        /// </summary>
        public virtual short Type { get; set; }

        /// <summary>
        /// سطح اعمال تنظیمات در برنامه که می تواند سراسری، در سطح زیرسیستم و یا در سطح موجودیت تعریف شود
        /// </summary>
        public virtual short ScopeType { get; set; }

        /// <summary>
        /// نوع کلاس مدل مورد استفاده برای نگهداری مقادیر تنظیمات
        /// </summary>
        public virtual string ModelType { get; set; }

        /// <summary>
        /// ریز اطلاعات تنظیمات که با فرمت مشخصی ذخیره و بازیابی می شود
        /// </summary>
        public virtual string Values { get; set; }

        /// <summary>
        /// ریز اطلاعات تنظیمات پیش فرض که با فرمت مشخصی ذخیره و بازیابی می شود
        /// </summary>
        public virtual string DefaultValues { get; set; }

        /// <summary>
        /// شناسه متن چندزبانه برای شرح تنظیمات در واسط کاربری
        /// </summary>
        public virtual string DescriptionKey { get; set; }

        /// <summary>
        /// آیا این تنظیمات مستقل از سایر تنظیمات است یا خیر؟
        /// </summary>
        public virtual bool IsStandalone { get; set; }

        /// <summary>
        /// تنظیمات والد برای این تنظیمات در ساختار درختی
        /// </summary>
        public virtual Setting Parent { get; set; }

        private void InitReferences()
        {
        }
    }
}
