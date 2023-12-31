// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 4/6/2020 6:34:00 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

namespace SPPC.Tadbir.Model.Finance
{
    /// <summary>
    /// مشخصات اطلاعات مالیاتی طرف حساب ها را نگهداری میکند
    /// </summary>
    public partial class CustomerTaxInfo : CoreEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerTaxInfo"/> class.
        /// </summary>
        public CustomerTaxInfo()
        {
        }

        /// <summary>
        /// Gets or sets the نام طرف حساب
        /// </summary>
        public virtual string CustomerFirstName { get; set; }

        /// <summary>
        /// Gets or sets the در صورتی که نوع شخص حقوقی باشد نام شرکت و در صورتی که حقیقی باشد نام خانوادگی
        /// </summary>
        public virtual string CustomerName { get; set; }

        /// <summary>
        /// Gets or sets the نوع شخص
        /// </summary>
        public virtual int PersonType { get; set; }

        /// <summary>
        /// Gets or sets the نوع خریدار
        /// </summary>
        public virtual int BuyerType { get; set; }

        /// <summary>
        /// Gets or sets the کد اقتصادی
        /// </summary>
        public virtual string EconomicCode { get; set; }

        /// <summary>
        /// Gets or sets the آدرس
        /// </summary>
        public virtual string Address { get; set; }

        /// <summary>
        /// Gets or sets the در صورتی که نوع شخص حقوقی باشد شناسه ملی و در صورتی که حقیقی باشد کد ملی است
        /// </summary>
        public virtual string NationalCode { get; set; }

        /// <summary>
        /// Gets or sets the پیش شماره تلفن
        /// </summary>
        public virtual string PerCityCode { get; set; }

        /// <summary>
        /// Gets or sets the شماره تلفن ثابت
        /// </summary>
        public virtual string PhoneNo { get; set; }

        /// <summary>
        /// Gets or sets the شماره تلفن همراه
        /// </summary>
        public virtual string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the کد پستی
        /// </summary>
        public virtual string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the کد استان
        /// </summary>
        public virtual string ProvinceCode { get; set; }

        /// <summary>
        /// Gets or sets the کد شهر
        /// </summary>
        public virtual string CityCode { get; set; }

        /// <summary>
        /// Gets or sets the توضیحات
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the حساب انتخاب شده
        /// </summary>
        public virtual Account Account { get; set; }
    }
}
