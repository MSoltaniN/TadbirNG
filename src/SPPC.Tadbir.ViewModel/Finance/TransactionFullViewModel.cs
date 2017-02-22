using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Provides complete details for a financial transaction, including additional fields from related entities.
    /// </summary>
    public class TransactionFullViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionFullViewModel"/> class.
        /// </summary>
        public TransactionFullViewModel()
        {
            Lines = new List<TransactionLineViewModel>();
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user-friendly identifier of this financial transaction
        /// </summary>
        [Display(Name = FieldNames.NumberField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string No { get; set; }

        /// <summary>
        /// Gets or sets the date when this financial transaction occured.
        /// The date is in persian (Jalali/Shamsi) calendar.
        /// </summary>
        [Display(Name = FieldNames.DateField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public string Date { get; set; }

        /// <summary>
        /// Gets or sets the detail information that further describes this financial transaction
        /// </summary>
        [Display(Name = FieldNames.DescriptionField)]
        [StringLength(512, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name of fiscal period in which this transaction is defined.
        /// </summary>
        [Display(Name = FieldNames.FiscalPeriodEntity)]
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// Gets or sets the name of company branch in which the fiscal period of this transaction is defined.
        /// </summary>
        [Display(Name = FieldNames.BranchEntity)]
        public string FiscalPeriodBranchName { get; set; }

        /// <summary>
        /// Gets or sets the name of company in which the branch for this transaction is defined.
        /// </summary>
        [Display(Name = FieldNames.CompanyEntity)]
        public string FiscalPeriodBranchCompanyName { get; set; }

        /// <summary>
        /// Gets or sets a collection of lines in this transaction.
        /// </summary>
        public IList<TransactionLineViewModel> Lines { get; private set; }
    }
}
