﻿using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Domain;
using SPPC.Framework.Values;

namespace SPPC.Licensing.Model
{
    public class LicenseModel : IEntity
    {
        public LicenseModel()
        {
            var now = DateTime.Now.Date;
            UserCount = 10;
            Edition = "Professional";
            ActiveModules = 1;
            StartDate = now;
            EndDate = new DateTime(now.Year + 1, now.Month, now.Day);
            RowGuid = Guid.NewGuid();
            ModifiedDate = DateTime.Now.Date;
        }

        public int Id { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "شناسه مشتری")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(36, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string CustomerKey { get; set; }

        [Display(Name = "شناسه مجوز")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(36, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string LicenseKey { get; set; }

        [Display(Name = "شناسه سخت افزاری")]
        [StringLength(256, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string HardwareKey { get; set; }

        [Display(Name = "کلید عمومی")]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ClientKey { get; set; }

        [Display(Name = "رمز مجوز")]
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Secret { get; set; }

        public int UserCount { get; set; }

        [Display(Name = "ویرایش برنامه")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(32, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Edition { get; set; }

        [Display(Name = "شروع قرارداد")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public DateTime StartDate { get; set; }

        [Display(Name = "پایان قرارداد")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public DateTime EndDate { get; set; }

        [Display(Name = "زیرسیستم های فعال")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int ActiveModules { get; set; }

        public bool IsActivated { get; set; }

        /// <summary>
        /// سقف مجاز تعریف شده برای تعداد دفعات گرفتن مجوز برنامه از سرور آفلاین
        /// </summary>
        /// <remarks>
        /// بعد از این تعداد ورود به برنامه، گرفتن مجوز از سرور آنلاین اجباری خواهد بود
        /// </remarks>
        public int OfflineLimit { get; set; }

        public CustomerModel Customer { get; set; }

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public LicenseModel GetCopy()
        {
            return (LicenseModel)MemberwiseClone();
        }
    }
}
