using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CurrencyViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی شعبه ای که ارز در آن تعریف شده است
        /// </summary>
        public int BranchId { get; set; }

        /// <summary>
        /// نام شعبه ای که ارز در آن تعریف شده است
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// کلید متن چند زبانه برای نام ارز جزء مورد استفاده، که برای ارزهای خارجی معمولاً سنت است
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public string MinorUnitKey { get; set; }
    }
}
