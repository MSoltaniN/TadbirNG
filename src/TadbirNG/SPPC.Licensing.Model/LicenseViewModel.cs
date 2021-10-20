using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Licensing.Model
{
    public class LicenseViewModel
    {
        public LicenseViewModel()
        {
        }

        [Display(Name = "نام مشتری")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string CustomerName { get; set; }

        [Display(Name = "نام صاحب مجوز")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ContactName { get; set; }

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
    }
}
