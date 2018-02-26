// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 2018-02-26 2:04:01 PM
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
    /// اطلاعات فراداده ای یک موجودیت در برنامه را نگهداری می کند
    /// </summary>
    public partial class EntityViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityViewModel"/> class.
        /// </summary>
        public EntityViewModel()
        {
            Name = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the نام موجودیت به صورت غیر محلی شده - به زبان انگلیسی
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(64, ErrorMessage = "{0} must have at most {1} characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the مشخص می کند که موجودیت ساختار سلسله مراتبی یا درختی دارد یا نه
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsHierarchy { get; set; }

        /// <summary>
        /// Gets or sets the مشخص می کند که موجودیت امکان تعامل با کارتابل را دارد یا نه
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public bool IsCartableIntegrated { get; set; }
    }
}
