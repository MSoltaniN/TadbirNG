using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات نقش ها را تعریف می کند
    /// </summary>
    public interface IRoleRepository
    {
        /// <summary>
        /// به روش آسنکرون، لیست نقش های تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست نقش های تعریف شده</returns>
        Task<IList<RoleViewModel>> GetRolesAsync(GridOptions gridOptions = null);

        /// <summary>
        /// Asynchronously initializes and returns a new role object that contains all available security permissions.
        /// </summary>
        /// <returns>A blank <see cref="RoleFullViewModel"/> object that contains full permission list from repository
        /// </returns>
        Task<RoleFullViewModel> GetNewRoleAsync();

        /// <summary>
        /// Asynchronously retrieves a single role with permissions (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleFullViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        Task<RoleFullViewModel> GetRoleAsync(int roleId);

        /// <summary>
        /// Asynchronously retrieves a single role with full details (specified by role identifier) from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleDetailsViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a role defined; otherwise, returns null.</returns>
        Task<RoleDetailsViewModel> GetRoleDetailsAsync(int roleId);

        /// <summary>
        /// Asynchronously retrieves brief information for a single role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to search for</param>
        /// <returns>A <see cref="RoleViewModel"/> instance that corresponds to the specified role identifier,
        /// if there is such a role defined; otherwise, returns null.</returns>
        Task<RoleViewModel> GetRoleBriefAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، تعداد نقش های تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد نقش های تعریف شده</returns>
        Task<int> GetRoleCountAsync(GridOptions gridOptions = null);

        /// <summary>
        /// Asynchronously inserts or updates a single security role, including all permissions in it, in repository
        /// </summary>
        /// <param name="role">Role to insert or update</param>
        Task<RoleViewModel> SaveRoleAsync(RoleFullViewModel role);

        /// <summary>
        /// Asynchronously deletes a role specified by unique identifier from repository.
        /// </summary>
        /// <param name="roleId">Unique identifier of the role to delete</param>
        /// <remarks>If no role with specified identifier could be found, no exception should be thrown.</remarks>
        Task DeleteRoleAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نقش مورد نظر به کاربری تخصیص داده شده یا نه
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اگر نقش مورد نظر به کاربری تخصیص داده شده مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsAssignedRoleAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نقش مورد نظر با یک یا چند شعبه مرتبط شده یا نه
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اگر نقش مورد نظر با یک یا چند شعبه مرتبط شده مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRoleRelatedToBranchAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نقش مورد نظر با یک یا چند دوره مالی مرتبط شده یا نه
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی نقش مورد نظر</param>
        /// <returns>اگر نقش مورد نظر با یک یا چند دوره مالی مرتبط شده مقدار "درست" و
        /// در غیر این صورت مقدار "نادرست" را برمی گرداند</returns>
        Task<bool> IsRoleRelatedToFiscalPeriodAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، شعبه های قابل دسترسی توسط یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی شعبه های قابل دسترسی</returns>
        Task<RelatedItemsViewModel> GetRoleBranchesAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت شعبه های قابل دسترسی توسط یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="roleBranches">اطلاعات نمایشی شعبه های قابل دسترسی</param>
        Task SaveRoleBranchesAsync(RelatedItemsViewModel roleBranches);

        /// <summary>
        /// به روش آسنکرون، کاربران یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی کاربران</returns>
        Task<RelatedItemsViewModel> GetRoleUsersAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت کاربران یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="roleUsers">اطلاعات نمایشی کاربران</param>
        Task SaveRoleUsersAsync(RelatedItemsViewModel roleUsers);

        /// <summary>
        /// به روش آسنکرون، دوره های مالی قابل دسترسی توسط یک نقش را خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه یکی از نقش های موجود</param>
        /// <returns>اطلاعات نمایشی دوره های مالی قابل دسترسی</returns>
        Task<RelatedItemsViewModel> GetRoleFiscalPeriodsAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت دوره های مالی قابل دسترسی توسط یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="rolePeriods">اطلاعات نمایشی دوره های مالی قابل دسترسی</param>
        Task SaveRoleFiscalPeriodsAsync(RelatedItemsViewModel rolePeriods);

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
        /// به روش آسنکرون، تنظیمات دسترسی به سطرهای اطلاعاتی را برای نقش مشخص شده خوانده و برمی گرداند
        /// </summary>
        /// <param name="roleId">شناسه دیتابیسی یکی از نقش های امنیتی موجود</param>
        /// <returns>تنظیمات دسترسی به سطرهای اطلاعاتی</returns>
        Task<RowPermissionsForRoleViewModel> GetRowAccessSettingsAsync(int roleId);

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت تنظیمات دسترسی به سطرهای اطلاعاتی برای یک نقش را ذخیره می کند
        /// </summary>
        /// <param name="permissions">تنظیمات دسترسی به سطرهای اطلاعاتی برای یک نقش</param>
        Task SaveRowAccessSettingsAsync(RowPermissionsForRoleViewModel permissions);

        /// <summary>
        /// مشخص می کند که آیا دسترسی داده شده قابل تخصیص به نقش های عمومی (غیر مدیر سیستم) هست یا نه؟
        /// </summary>
        /// <param name="permission">نقش مورد نظر برای بررسی</param>
        /// <returns>در صورت عمومی بودن نقش، مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        bool IsPublicPermission(PermissionViewModel permission);
    }
}
