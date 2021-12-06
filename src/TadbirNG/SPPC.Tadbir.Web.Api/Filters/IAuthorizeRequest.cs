using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// عملیات مورد نیاز برای احراز هویت، مجوزدهی و کنترل لایسنس را تعریف می کند
    /// </summary>
    public interface IAuthorizeRequest
    {
        /// <summary>
        /// مجوزهای امنیتی مورد نیاز برای عملیات کنترل دسترسی را تنظیم می کند
        /// </summary>
        /// <param name="permissions">مجوزهای امنیتی مورد نیاز</param>
        void SetRequiredPermissions(IEnumerable<PermissionBriefViewModel> permissions);

        /// <summary>
        /// با توجه به محتوای درخواست داده شده، کنترل دسترسی امنیتی را انجام داده و نتیجه را برمی گرداند
        /// </summary>
        /// <param name="httpRequest">درخواست وب جاری</param>
        /// <returns>نتیجه عملیات پس از کنترل لایسنس، احراز هویت و کنترل دسترسی امنیتی</returns>
        IActionResult GetAuthorizationResult(HttpRequest httpRequest);
    }
}
