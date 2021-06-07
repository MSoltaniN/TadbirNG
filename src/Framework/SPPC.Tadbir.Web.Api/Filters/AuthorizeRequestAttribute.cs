using System;
using Microsoft.AspNetCore.Mvc.Filters;
using SPPC.Framework.Common;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Web.Api.Filters
{
    /// <summary>
    /// امکانات احراز هویت و مجوزدهی امنیتی را با استفاده از
    /// مکانیزم فیلترهای درخواست پیاده سازی می کند
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public sealed class AuthorizeRequestAttribute : Attribute, IFilterMetadata
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public AuthorizeRequestAttribute()
        {
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس با توجه به موجودیت محافظت شده و دسترسی مورد نیاز می سازد
        /// </summary>
        /// <param name="entity">موجودیت محافظت شده ای که مجوزدهی امنیتی باید برای آن انجام شود</param>
        /// <param name="permission">دسترسی امنیتی مورد نیاز برای ادامه عملیات</param>
        public AuthorizeRequestAttribute(string entity, int permission)
            : this()
        {
            Verify.ArgumentNotNullOrWhitespace(entity, nameof(entity));
            _requiredPermissions = new PermissionBriefViewModel[]
            {
                new PermissionBriefViewModel(entity, permission)
            };
        }

        /// <summary>
        /// موجودیت محافظت شده ای که مجوزدهی امنیتی باید برای آن انجام شود
        /// </summary>
        public string Entity
        {
            get { return _requiredPermissions?[0].EntityName; }
        }

        /// <summary>
        /// دسترسی امنیتی مورد نیاز برای ادامه عملیات
        /// </summary>
        public int Permission
        {
            get { return (_requiredPermissions != null) ? _requiredPermissions[0].Flags : 0; }
        }

        private readonly PermissionBriefViewModel[] _requiredPermissions;
    }
}
