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

namespace SPPC.Tadbir.ViewModel.Contact
{
    /// <summary>
    /// TODO: Add description...
    /// </summary>
    public partial class CustomerViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerViewModel"/> class.
        /// </summary>
        public CustomerViewModel()
        {
            this.Name = String.Empty;
            this.Phone = String.Empty;
            this.Email = String.Empty;
            this.CommerceCode = String.Empty;
            this.Address = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(128, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Name { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Phone { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Email { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string CommerceCode { get; set; }

        /// <summary>
        /// TODO: Add description...
        /// </summary>
        [MaxLength(256, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Address { get; set; }
    }
}
