using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CurrencyViewModel : ViewModelBase, IFiscalEntity
    {
        /// <summary>
        /// شناسه دیتابیسی دوره مالی که ارز در آن تعریف شده است - این فیلد فعلاً استفاده نمی شود
        /// </summary>
        public int FiscalPeriodId { get; set; }

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
