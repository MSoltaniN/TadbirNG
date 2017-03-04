// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-02-15 2:58:00 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;
using SPPC.Tadbir.Values;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Represents a financial transaction that provides monetary information for a business event
    /// </summary>
    public partial class TransactionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionViewModel"/> class.
        /// </summary>
        public TransactionViewModel()
        {
            this.No = String.Empty;
            this.Date = "1390/09/09";
            this.Description = String.Empty;
            this.Status = Transactions.UnregisteredStatus;
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
        /// Gets or sets the current status of this financial transaction
        /// </summary>
        [Display(Name = FieldNames.StatusField)]
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        [StringLength(64, ErrorMessage = ValidationMessages.TextFieldIsTooLong)]
        public string Status { get; set; }
    }
}
