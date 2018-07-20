using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.ViewModel.Auth
{
    /// <summary>
    /// تنظیمات محدودیت دسترسی در سطح سطرهای اطلاعاتی را برای یک نقش امنیتی نگهداری می کند
    /// </summary>
    public class RowPermissionsForRoleViewModel
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public RowPermissionsForRoleViewModel()
        {
            RowPermissions = new List<ViewRowPermissionViewModel>();
        }

        /// <summary>
        /// شناسه دیتابیسی نقش امنیتی که دسترسی به سطرهای اطلاعاتی برای آن تعریف می شود
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// مجموعه ای از تنظیمات دسترسی به سطرهای اطلاعاتی برای نقش امنیتی
        /// </summary>
        public IList<ViewRowPermissionViewModel> RowPermissions { get; protected set; }
    }
}
