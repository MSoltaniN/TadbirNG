using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Domain;
using SPPC.Framework.Values;

namespace SPPC.Licensing.Model
{
    public class CustomerModel : IEntity
    {
        public CustomerModel()
        {
            Licenses = new List<LicenseModel>();
            RowGuid = Guid.NewGuid();
            ModifiedDate = DateTime.Now.Date;
        }

        public int Id { get; set; }

        [Display(Name = "شناسه مشتری")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(36, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string CustomerKey { get; set; }

        [Display(Name = "نام شرکت")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string CompanyName { get; set; }

        [Display(Name = "دامنه کاری")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Industry { get; set; }

        [Display(Name = "تعداد پرسنل")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string EmployeeCount { get; set; }

        [Display(Name = "نشانی دفتر مرکزی")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string HeadquartersAddress { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ContactFirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string ContactLastName { get; set; }

        [Display(Name = "شماره مستقیم")]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string WorkPhone { get; set; }

        [Display(Name = "شماره نمابر")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string WorkFax { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(16, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string CellPhone { get; set; }

        public IList<LicenseModel> Licenses { get; }

        public Guid RowGuid { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
