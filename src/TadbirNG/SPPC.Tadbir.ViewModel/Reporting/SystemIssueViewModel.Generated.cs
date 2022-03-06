// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.0.0.0
//     Template Version: 1.0
//     Generation Date: 11/23/2019 5:23:17 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.ViewModel.Reporting
{
    /// <summary>
    /// آیتم های مربوط به کنترل سیستم را نگهداری می کند
    /// </summary>
    public partial class SystemIssueViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SystemIssueViewModel"/> class.
        /// </summary>
        public SystemIssueViewModel()
        {
            this.Title = String.Empty;
            this.ApiUrl = String.Empty;
        }

        /// <summary>
        /// Gets or sets the unique identifier for this entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// عنوان محلی شده در زبان جاری برنامه
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        [StringLength(64, MinimumLength = 0, ErrorMessage = "{0} must have from {2} to {1} characters.")]
        public string Title { get; set; }

        /// <summary>
        /// آدرس فراخوانی لیست این آیتم
        /// </summary>
        [StringLength(128, MinimumLength = 0, ErrorMessage = "{0} must have from {2} to {1} characters.")]
        public string ApiUrl { get; set; }

        /// <summary>
        /// آدرس سرویس برای حذف گروهی
        /// </summary>
        [StringLength(128, MinimumLength = 0, ErrorMessage = "{0} must have from {2} to {1} characters.")]
        public virtual string DeleteApiUrl { get; set; }


        /// <summary>
        /// آیا در گزارشگیری انتخاب سطح دسترسی شعب قابل انتخاب باشد یا خیر؟
        /// </summary>
        [Required(ErrorMessage = "{0} is required.")]
        public virtual bool BranchScope { get; set; }
    }
}
