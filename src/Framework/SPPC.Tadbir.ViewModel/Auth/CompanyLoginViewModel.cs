using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// اطلاعات مربوط به ورود یک کاربر به یک شرکت را نگهداری می کند
    /// </summary>
    public class CompanyLoginViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی شرکتی که کاربر به آن وارد شده
        /// </summary>
        [Display(Name = FieldNames.Company)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? CompanyId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شعبه ای از شرکت جاری که کاربر به آن وارد شده
        /// </summary>
        [Display(Name = FieldNames.Branch)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? BranchId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی دوره مالی از شرکت جاری که کاربر به آن وارد شده
        /// </summary>
        [Display(Name = FieldNames.FiscalPeriod)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? FiscalPeriodId { get; set; }
    }
}
