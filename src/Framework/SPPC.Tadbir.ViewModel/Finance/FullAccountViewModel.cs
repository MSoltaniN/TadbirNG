using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات نمایشی بردار حساب را نگهداری می کند
    /// </summary>
    public class FullAccountViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی مولفه سرفصل حسابداری
        /// </summary>
        [Display(Name = FieldNames.AccountField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int? AccountId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه تفصیلی شناور
        /// </summary>
        [Display(Name = FieldNames.DetailAccountField)]
        public int? DetailId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه مرکز هزینه
        /// </summary>
        [Display(Name = FieldNames.CostCenterField)]
        public int? CostCenterId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی مولفه پروژه
        /// </summary>
        [Display(Name = FieldNames.ProjectField)]
        public int? ProjectId { get; set; }
    }
}
