// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-02-26 2:04:18 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// اطلاعات فراداده ای یک ویژگی از یک موجودیت را نگهداری می کند
    /// </summary>
    public partial class PropertyViewModel
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        public PropertyViewModel()
        {
            Name = String.Empty;
            DotNetType = String.Empty;
            StorageType = String.Empty;
            ScriptType = String.Empty;
            NameResourceId = String.Empty;
        }

        /// <summary>
        /// شناسه دیتابیسی این موجودیت
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// نام ویژگی به صورت غیر محلی شده - به زبان انگلیسی
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Name { get; set; }

        /// <summary>
        /// نوع داده ای مورد استفاده در دات نت
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string DotNetType { get; set; }

        /// <summary>
        /// نوع داده ای مورد استفاده در دیتابیس
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string StorageType { get; set; }

        /// <summary>
        /// نوع داده ای مورد استفاده در جاواسکریپت
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ScriptType { get; set; }

        /// <summary>
        /// تعداد کاراکتر در ویژگی متنی - به طور پیش فرض مقدار صفر دارد و برای ویژگی های غیرمتنی کاربردی ندارد
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// مشخص می کند که تعداد کاراکترها در یک ویژگی متنی ثابت است یا نه - به طور پیش فرض مقدار نادرست دارد و برای ویژگی های غیرمتنی کاربردی ندارد
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public bool IsFixedLength { get; set; }

        /// <summary>
        /// مشخص می کند که وارد کردن مقدار برای ویژگی اجباری است یا نه
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public bool IsNullable { get; set; }

        /// <summary>
        /// شناسه یکتای متن محلی شده برای نام ویژگی - به زبان انگلیسی
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(128, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string NameResourceId { get; set; }
    }
}
