// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 4/6/2020 6:34:03 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// مشخصات اطلاعات مالیاتی طرف حساب ها را نگهداری میکند
    /// </summary>
    public partial class CustomerTaxInfoViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTaxInfoViewModel"/> class.
        /// </summary>
        public CustomerTaxInfoViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the نام طرف حساب
        /// </summary>
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string CustomerFirstName { get; set; }

        /// <summary>
        /// Gets or sets the در صورتی که نوع شخص حقوقی باشد نام شرکت و در صورتی که حقیقی باشد نام خانوادگی
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(128, ErrorMessage = "{0} must have at most {1} characters.")]
        public string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the نوع شخص
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public int PersonType { get; set; }

        /// <summary>
        /// Gets or sets the نوع خریدار
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public int BuyerType { get; set; }

        /// <summary>
        /// Gets or sets the کد اقتصادی
        /// </summary>
        [MaxLength(12, ErrorMessage = "{0} must have at most {1} characters.")]
        public string EconomicCode { get; set; }

        /// <summary>
        /// Gets or sets the آدرس
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(256, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the در صورتی که نوع شخص حقوقی باشد شناسه ملی و در صورتی که حقیقی باشد کد ملی میباشد
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(11, ErrorMessage = "{0} must have at most {1} characters.")]
        public string NationalCode { get; set; }

        /// <summary>
        /// Gets or sets the پیش شماره تلفن
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(10, ErrorMessage = "{0} must have at most {1} characters.")]
        public string PerCityCode { get; set; }

        /// <summary>
        /// Gets or sets the شماره تلفن ثابت
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} must have at most {1} characters.")]
        public string PhoneNo { get; set; }

        /// <summary>
        /// Gets or sets the شماره تلفن همراه
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} must have at most {1} characters.")]
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the کد پستی
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(10, ErrorMessage = "{0} must have at most {1} characters.")]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the توضیحات
        /// </summary>
        [MaxLength(1024, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Description { get; set; }
    }
}