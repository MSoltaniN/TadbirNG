// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1596
//     Template Version: 1.0
//     Generation Date: 10/07/2023 4:44:16 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using SPPC.Tadbir.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;

namespace SPPC.Tadbir.Model.ProductScope
{
    /// <summary>
    /// اطلاعات یک واحد را نگهدارری می کند
    /// </summary>
    [Table("File", Schema = "ProductScope")]
    public partial class File : PBaseEntity
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public File()
        {
            Name = String.Empty;
            Description = String.Empty;
            UniqeName = String.Empty;
            ModifiedDate = DateTime.Now;
        }

        /// <summary>
        /// نام فایل
        /// </summary>
        [MaxLength(2048)]
        public virtual string Name { get; set; }

        /// <summary>
        /// شرح تکمیلی  فایل
        /// </summary>
        [MaxLength(2048)]
        public virtual string? Description { get; set; }

        /// <summary>
        /// وضعیت فعال بودن فایل
        /// </summary>
        public virtual bool? IsActive { get; set; }

        /// <summary>
        /// نام یکتای فایل
        /// </summary>
        [MaxLength(2048)]
        [Required]
        public virtual string UniqeName { get; set; }

        /// <summary>
        /// نوع فایل
        /// </summary>
        [MaxLength(10)]
        public virtual string? Type { get; set; }

        /// <summary>
        /// داده فایل
        /// </summary>
        public virtual byte[]? Data { get; set; }
    }
}
