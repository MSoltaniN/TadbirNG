// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 4/30/2020 3:48:06 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// شهر یک استان را نگهداری میکند
    /// </summary>
    public partial class CityViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CityViewModel"/> class.
        /// </summary>
        public CityViewModel()
        {
            this.Name = String.Empty;
            this.Code = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the نام شهر
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the کد شهر
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(16, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Code { get; set; }
    }
}
