// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-02 7:42:32 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Warehousing
{
    public partial class IssueReceiptVoucherViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueReceiptVoucherViewModel"/> class.
        /// </summary>
        public IssueReceiptVoucherViewModel()
        {
            this.No = String.Empty;
            this.DocumentNo = String.Empty;
            this.Status = String.Empty;
            this.OperationalStatus = String.Empty;
            this.Reference = String.Empty;
            this.Description = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string No { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string DocumentNo { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Status { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string OperationalStatus { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsActive { get; set; }
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Reference { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public short Type { get; set; }
        [MaxLength(256, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public DateTime CreatedDate { get; set; }
        public DateTime? ConfirmedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public byte Timestamp { get; set; }
    }
}
