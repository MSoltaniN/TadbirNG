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

namespace SPPC.Tadbir.ViewModel.Finance
{
    public partial class CostCenterViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CostCenterViewModel"/> class.
        /// </summary>
        public CostCenterViewModel()
        {
            this.Code = String.Empty;
            this.FullCode = String.Empty;
            this.Name = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(16, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Code { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(256, ErrorMessage = "{0} must have at most {1} characters.")]
        public string FullCode { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(256, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "{0} is required.")]
        public short Level { get; set; }
    }
}
