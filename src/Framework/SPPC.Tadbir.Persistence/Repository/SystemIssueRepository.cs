using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Repository
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت کنترل سیستم را تعریف می کند.
    /// </summary>
    public class SystemIssueRepository : RepositoryBase, ISystemIssueRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public SystemIssueRepository(IRepositoryContext context, ISystemRepository system)
            : base(context)
        {
            _system = system;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی موضوعات سیستم قابل دسترسی توسط کاربر مشخص شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکتای یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از موضوعات سیستم قابل دسترسی توسط کاربر</returns>
        public async Task<IList<SystemIssueViewModel>> GetUserSystemIssuesAsync(int userId)
        {
            var sysIssues = await FilterInaccessibleIssues();

            return sysIssues
                .Select(iss => Mapper.Map<SystemIssueViewModel>(iss))
                .ToList();
        }

        private async Task<IList<int>> GetUserPermissionIdsAsync()
        {
            var permissionIds = new List<int>();
            var roles = await GetUserRolesAsync();

            Array.ForEach(roles.ToArray(),
                role => permissionIds.AddRange(role.RolePermissions.Select(rp => rp.PermissionId)));

            return permissionIds
                .Distinct()
                .ToList();
        }

        private async Task<IList<SystemIssue>> FilterInaccessibleIssues()
        {
            var repository = UnitOfWork.GetAsyncRepository<SystemIssue>();

            var userPermissions = await GetUserPermissionIdsAsync();

            bool isAdmin = UserContext.Roles.Contains(AppConstants.AdminRoleId);

            if (isAdmin)
            {
                return await repository
                    .GetAllAsync();
            }
            else
            {
                return await repository
                    .GetByCriteriaAsync(
                    issue => !issue.PermissionID.HasValue
                    || userPermissions.Contains(issue.PermissionID.Value));
            }
        }

        private async Task<IList<Role>> GetUserRolesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Role>();
            var userRolesIds = UserContext.Roles;

            var roles = await repository
                .GetByCriteriaAsync(
                role => userRolesIds.Contains(role.Id),
                role => role.RolePermissions);

            return roles;
        }

        private readonly ISystemRepository _system;
    }
}
