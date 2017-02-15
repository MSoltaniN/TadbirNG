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

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// Represents a single entry (article) in a financial transaction
    /// </summary>
    public partial class TransactionLineViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionLineViewModel"/> class.
        /// </summary>
        public TransactionLineViewModel()
        {
            this.Description = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the detail information that further describes this financial entry (article)
        /// </summary>
        [Display(Name = "")]
        [StringLength(512, ErrorMessage = "{0} can have a maximum of {1} characters.")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount debited in this financial transaction entry
        /// </summary>
        [Display(Name = "")]
        [Required(ErrorMessage = "{0} is required.")]
        public decimal Debit { get; set; }

        /// <summary>
        /// Gets or sets the amount credited in this financial transaction entry
        /// </summary>
        [Display(Name = "")]
        [Required(ErrorMessage = "{0} is required.")]
        public decimal Credit { get; set; }
    }
}
