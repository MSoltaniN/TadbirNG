// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2017-08-03 7:59:17 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Core
{
    /// <summary>
    /// TODO: Add description...
    /// </summary>
    public partial class DocumentActionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentActionViewModel"/> class.
        /// </summary>
        public DocumentActionViewModel()
        {
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public string CreatedDate { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string ConfirmedDate { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        public string ApprovedDate { get; set; }
    }
}
