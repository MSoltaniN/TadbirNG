// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1425
//     Template Version: 1.0
//     Generation Date: 2022-08-31 11:04:12 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using SPPC.Framework.Values;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// اطلاعات یکی از نقش های دارای دسترسی به یک ویجت را نگهداری می کند
    /// </summary>
    public partial class RoleWidgetViewModel : ViewModelBase
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RoleWidgetViewModel()
        {
        }

        /// <summary>
        /// شناسه دیتابیسی یکی از نقش های دارای دسترسی به یک ویجت کاربری
        /// </summary>
        [Required(ErrorMessage = ValidationMessages.FieldIsRequired)]
        public int RoleId { get; set; }
    }
}
