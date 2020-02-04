using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات کاربران را تعریف می کند
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// به روش آسنکرون، لیست کاربران برنامه را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست کاربران برنامه</returns>
        Task<IList<UserViewModel>> GetUsersAsync(GridOptions gridOptions = null);

        /// <summary>
        /// Asynchronously retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        Task<UserViewModel> GetUserAsync(string userName);

        /// <summary>
        /// Asynchronously retrieves a single user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a user defined; otherwise, returns null.</returns>
        Task<UserViewModel> GetUserAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک کاربر را از روی داده های ورود کاربر به برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="login">اطلاعات ورود کاربر به برنامه</param>
        /// <returns>اطلاعات به دست آمده برای کاربر</returns>
        Task<UserViewModel> GetUserAsync(LoginViewModel login);

        /// <summary>
        /// Asynchronously retrieves context information for a user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserContextViewModel"/> instance containing context information, if there is
        /// such a user defined; otherwise, returns null.</returns>
        Task<UserContextViewModel> GetUserContextAsync(int userId);

        /// <summary>
        /// نام کاربر جاری را با قالب پیش فرض (نام خانوادگی، نام) برمی گرداند
        /// </summary>
        /// <returns>نام کاربر جاری با قالب پیش فرض</returns>
        Task<string> GetCurrentUserDisplayNameAsync();

        /// <summary>
        /// به روش آسنکرون، دسترسی های امنیتی داده شده به یک کاربر را به صورت مجموعه ای از شناسه های یکتا
        /// از محل ذخیره خوانده و بر می گرداند
        /// </summary>
        /// <param name="userId">شناسه یکتای یکی از کاربران موجود</param>
        /// <returns>مجموعه شناسه های دسترسی های داده شده به کاربر</returns>
        Task<IList<int>> GetUserPermissionIdsAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی تمام دستورات قابل دسترسی توسط کاربر مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکتای یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از دستورات قابل دسترسی توسط کاربر</returns>
        Task<IList<CommandViewModel>> GetUserCommandsAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی گزینه های منوی پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>گزینه های منوی پیش فرض کاربران</returns>
        Task<IList<CommandViewModel>> GetUserCommandsAsync();

        /// <summary>
        /// به روش آسنکرون، تعداد کاربران تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد کاربران تعریف شده</returns>
        Task<int> GetUserCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// به روش آسنکرون، نقش های یک کاربر را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکی از کاربران موجود</param>
        /// <returns>اطلاعات نمایشی نقش ها</returns>
        Task<RelatedItemsViewModel> GetUserRolesAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های یک کاربر را ذخیره می کند
        /// </summary>
        /// <param name="userRoles">اطلاعات نمایشی نقش ها</param>
        Task SaveUserRolesAsync(RelatedItemsViewModel userRoles);

        /// <summary>
        /// Asynchronously inserts or updates a single user in repository.
        /// </summary>
        /// <param name="user">Item to insert or update</param>
        Task<UserViewModel> SaveUserAsync(UserViewModel user);

        /// <summary>
        /// Asynchronously sets LastLoginDate field of the specified user to current system date/time.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        Task UpdateUserLastLoginAsync(int userId);

        /// <summary>
        /// Asynchronously updates a user profile in repository.
        /// </summary>
        /// <param name="profile">User profile to update</param>
        Task UpdateUserPasswordAsync(UserProfileViewModel profile);

        /// <summary>
        /// به روش آسنکرون، وضعیت ورود یک کاربر را به یک شرکت و دوره مالی و شعبه بروزرسانی می کند
        /// </summary>
        /// <param name="companyLogin">اطلاعات ورود کاربر به شرکت</param>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر</param>
        Task UpdateUserCompanyLoginAsync(CompanyLoginViewModel companyLogin, UserContextViewModel userContext);

        /// <summary>
        /// Asynchronously determines if the specified <see cref="UserViewModel"/> instance has a user name that is already used
        /// by a different user.
        /// </summary>
        /// <param name="user">User item to check for duplicate user name</param>
        /// <returns>True if the user name is already used; otherwise returns false.</returns>
        Task<bool> IsDuplicateUserAsync(UserViewModel user);
    }
}
