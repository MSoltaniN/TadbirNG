using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Mapper;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات کاربران را پیاده سازی می کند
    /// </summary>
    public class UserRepository : LoggingRepository<User, UserViewModel>, IUserRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="metadata">امکان خواندن اطلاعات فراداده ای برنامه را فراهم می کند</param>
        /// <param name="log">امکان ایجاد لاگ های عملیاتی را در دیتابیس سیستمی برنامه فراهم می کند</param>
        public UserRepository(IRepositoryContext context, IMetadataRepository metadata, IOperationLogRepository log)
            : base(context, log)
        {
            _metadata = metadata;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، لیست کاربران برنامه را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست کاربران برنامه</returns>
        public async Task<IList<UserViewModel>> GetUsersAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetAllAsync(u => u.Person);
            return users
                .Select(user => Mapper.Map<UserViewModel>(user))
                .Apply(gridOptions)
                .ToList();
        }

        /// <summary>
        /// Asynchronously retrieves a single user specified by user name from repository.
        /// </summary>
        /// <param name="userName">User name to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified user name, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public async Task<UserViewModel> GetUserAsync(string userName)
        {
            UserViewModel userViewModel = null;
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetByCriteriaAsync(usr => usr.UserName == userName, usr => usr.Person);
            var user = users.SingleOrDefault();
            if (user != null)
            {
                userViewModel = Mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        /// <summary>
        /// Asynchronously retrieves a single user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserViewModel"/> instance that corresponds to the specified identifier, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public async Task<UserViewModel> GetUserAsync(int userId)
        {
            UserViewModel userViewModel = null;
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var user = await repository.GetByIDAsync(userId, usr => usr.Person);
            if (user != null)
            {
                userViewModel = Mapper.Map<UserViewModel>(user);
            }

            return userViewModel;
        }

        /// <summary>
        /// Asynchronously retrieves context information for a user specified by unique identifier from repository.
        /// </summary>
        /// <param name="userId">Unique identifier of the user to search for</param>
        /// <returns>A <see cref="UserContextViewModel"/> instance containing context information, if there is
        /// such a user defined; otherwise, returns null.</returns>
        public async Task<UserContextViewModel> GetUserContextAsync(int userId)
        {
            UserContextViewModel userContext = null;
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var user = await repository.GetByIDAsync(userId, usr => usr.Person, usr => usr.UserRoles);
            if (user != null)
            {
                userContext = Mapper.Map<UserContextViewModel>(user);
                var permissions = new List<PermissionBriefViewModel>();
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                foreach (var roleId in user.UserRoles.Select(ur => ur.RoleId))
                {
                    var role = await roleRepository.GetByIDAsync(roleId, r => r.RolePermissions);
                    userContext.Roles.Add(roleId);
                    Array.ForEach(
                        role.RolePermissions.ToArray(),
                        rp => permissions.Add(Mapper.Map<PermissionBriefViewModel>(
                            UnitOfWork.GetRepository<Permission>().GetByID(rp.PermissionId, perm => perm.Group))));
                }

                var groups = permissions
                    .Distinct(new PermissionEqualityComparer())
                    .GroupBy(perm => perm.EntityName);
                foreach (var group in groups)
                {
                    var permission = new PermissionBriefViewModel()
                    {
                        EntityName = group.Key,
                        Flags = group.Sum(perm => perm.Flags)
                    };
                    userContext.Permissions.Add(permission);
                }
            }

            return userContext;
        }

        /// <summary>
        /// نام کاربر جاری را با قالب پیش فرض (نام خانوادگی، نام) برمی گرداند
        /// </summary>
        /// <returns>نام کاربر جاری با قالب پیش فرض</returns>
        public async Task<string> GetCurrentUserDisplayNameAsync()
        {
            var user = await GetUserAsync(_currentContext.Id);
            return String.Format("{0}, {1}", user.PersonLastName, user.PersonFirstName);
        }

        /// <summary>
        /// دسترسی های امنیتی داده شده به یک کاربر را به صورت مجموعه ای از شناسه های دیتابیسی
        /// از دیتابیس خوانده و بر می گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه شناسه های دسترسی های داده شده به کاربر</returns>
        public async Task<IList<int>> GetUserPermissionIdsAsync(int userId)
        {
            var permissionIds = new List<int>();
            var query = GetUserPermissionsQuery(userId);
            var user = await query.SingleOrDefaultAsync();
            if (user != null)
            {
                Array.ForEach(user.UserRoles.Select(ur => ur.Role).ToArray(),
                    role => permissionIds.AddRange(role.RolePermissions.Select(rp => rp.PermissionId)));
            }

            return permissionIds
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// اطلاعات نمایشی تمام دستورات قابل دسترسی توسط کاربر مشخص شده را از دیتابیس خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از دستورات قابل دسترسی توسط کاربر</returns>
        public async Task<IList<CommandViewModel>> GetUserCommandsAsync(int userId)
        {
            var topCommands = await _metadata.GetTopLevelCommandsAsync();
            var userPermissions = await GetUserPermissionIdsAsync(userId);
            FilterInaccessibleCommands(userPermissions, topCommands);
            return topCommands;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی گزینه های منوی پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>گزینه های منوی پیش فرض کاربران</returns>
        public async Task<IList<CommandViewModel>> GetUserCommandsAsync()
        {
            return await _metadata.GetDefaultCommandsAsync();
        }

        /// <summary>
        /// به روش آسنکرون، تعداد کاربران تعریف شده را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>تعداد کاربران تعریف شده</returns>
        public async Task<int> GetUserCountAsync(GridOptions gridOptions = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var items = await repository.GetAllAsync();
            return items
                .Select(usr => Mapper.Map<UserViewModel>(usr))
                .Apply(gridOptions, false)
                .Count();
        }

        /// <summary>
        /// به روش آسنکرون، نقش های یک کاربر را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکی از کاربران موجود</param>
        /// <returns>اطلاعات نمایشی نقش ها</returns>
        public async Task<RelatedItemsViewModel> GetUserRolesAsync(int userId)
        {
            RelatedItemsViewModel userRoles = null;
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var existing = await repository
                .GetEntityQuery()
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .Where(u => u.Id == userId)
                .SingleOrDefaultAsync();
            if (existing != null)
            {
                var enabledRoles = existing.UserRoles
                    .Select(ur => ur.Role)
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .ToArray();
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                var allRoles = await roleRepository
                    .GetAllAsync();
                var disabledRoles = allRoles
                    .Select(r => Mapper.Map<RelatedItemViewModel>(r))
                    .Except(enabledRoles, new EntityEqualityComparer<RelatedItemViewModel>())
                    .ToArray();
                Array.ForEach(enabledRoles, item => item.IsSelected = true);

                userRoles = Mapper.Map<RelatedItemsViewModel>(existing);
                Array.ForEach(enabledRoles
                    .Concat(disabledRoles)
                    .OrderBy(item => item.Id)
                    .ToArray(), item => userRoles.RelatedItems.Add(item));
            }

            return userRoles;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین وضعیت نقش های یک کاربر را ذخیره می کند
        /// </summary>
        /// <param name="userRoles">اطلاعات نمایشی نقش ها</param>
        public async Task SaveUserRolesAsync(RelatedItemsViewModel userRoles)
        {
            Verify.ArgumentNotNull(userRoles, "userRoles");
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var existing = await repository.GetByIDWithTrackingAsync(userRoles.Id, r => r.UserRoles);
            if (existing != null && AreRolesModified(existing, userRoles))
            {
                if (existing.UserRoles.Count > 0)
                {
                    RemoveUnassignedRoles(existing, userRoles);
                }

                AddNewRoles(existing, userRoles);
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Asynchronously inserts or updates a single user in repository.
        /// </summary>
        /// <param name="userView">Item to insert or update</param>
        public async Task<UserViewModel> SaveUserAsync(UserViewModel userView)
        {
            Verify.ArgumentNotNull(userView, "userView");
            User user = default(User);
            var repository = UnitOfWork.GetAsyncRepository<User>();
            if (userView.Id == 0)
            {
                user = GetNewUser(userView);
                OnAction("Create", null, user);
                repository.Insert(user, usr => usr.Person);
                await FinalizeActionAsync();
            }
            else
            {
                user = await repository.GetByIDAsync(userView.Id, u => u.Person);
                if (user != null)
                {
                    var clone = Mapper.Map<User>(user);
                    OnAction("Edit", clone, null);
                    UpdateExisting(userView, user);
                    Log.AfterState = GetState(user);
                    repository.Update(user, usr => usr.Person);
                    await FinalizeActionAsync();
                }
            }

            return Mapper.Map<UserViewModel>(user);
        }

        /// <summary>
        /// Asynchronously sets LastLoginDate field of the specified user to current system date/time.
        /// </summary>
        /// <param name="userId">Unique identifier of an existing user</param>
        public async Task UpdateUserLastLoginAsync(int userId)
        {
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var user = await repository.GetByIDAsync(userId);
            if (user != null)
            {
                user.LastLoginDate = DateTime.Now;
                repository.Update(user);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// Asynchronously updates a user profile in repository.
        /// </summary>
        /// <param name="profile">User profile to update</param>
        public async Task UpdateUserPasswordAsync(UserProfileViewModel profile)
        {
            Verify.ArgumentNotNull(profile, "profile");
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetByCriteriaAsync(usr => usr.UserName == profile.UserName);
            var user = users.SingleOrDefault();
            if (user != null)
            {
                user.PasswordHash = profile.NewPassword;
                repository.Update(user);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت ورود یک کاربر را به یک شرکت و دوره مالی و شعبه بروزرسانی می کند
        /// </summary>
        /// <param name="companyLogin">اطلاعات ورود کاربر به شرکت</param>
        /// <param name="userContext">اطلاعات محیطی و امنیتی کاربر</param>
        public async Task UpdateUserCompanyLoginAsync(
            CompanyLoginViewModel companyLogin, UserContextViewModel userContext)
        {
            Verify.ArgumentNotNull(companyLogin, "companyLogin");
            Verify.ArgumentNotNull(userContext, "userContext");
            userContext.CompanyId = (int)companyLogin.CompanyId;
            userContext.BranchId = (int)companyLogin.BranchId;
            userContext.FiscalPeriodId = (int)companyLogin.FiscalPeriodId;

            var companyRepo = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await companyRepo.GetByIDAsync((int)companyLogin.CompanyId);
            if (company != null)
            {
                userContext.CompanyName = company.Name;
                userContext.Connection = BuildConnectionString(company);
            }

            await SetCurrentCompanyAsync((int)companyLogin.CompanyId);
            UnitOfWork.UseCompanyContext();
            var branchRepo = UnitOfWork.GetAsyncRepository<Branch>();
            var fiscalRepo = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            var branch = await branchRepo.GetByIDAsync((int)companyLogin.BranchId);
            userContext.BranchName = branch?.Name;
            var fiscalPeriod = await fiscalRepo.GetByIDAsync((int)companyLogin.FiscalPeriodId);
            userContext.FiscalPeriodName = fiscalPeriod?.Name;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// Asynchronously determines if the specified <see cref="UserViewModel"/> instance has a user name that is already used
        /// by a different user.
        /// </summary>
        /// <param name="user">User item to check for duplicate user name</param>
        /// <returns>True if the user name is already used; otherwise returns false.</returns>
        public async Task<bool> IsDuplicateUserAsync(UserViewModel user)
        {
            Verify.ArgumentNotNull(user, "user");
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var items = await repository
                .GetByCriteriaAsync(usr => usr.Id != user.Id
                    && usr.UserName == user.UserName);
            var existing = items.SingleOrDefault();
            return (existing != null);
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="userView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="user">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(UserViewModel userView, User user)
        {
            var modifiedUser = Mapper.Map<User>(userView);
            user.UserName = userView.UserName;
            user.IsEnabled = userView.IsEnabled;
            user.Person.FirstName = userView.PersonFirstName;
            user.Person.LastName = userView.PersonLastName;
            if (!String.IsNullOrEmpty(modifiedUser.PasswordHash))
            {
                user.PasswordHash = modifiedUser.PasswordHash;
            }
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(User entity)
        {
            return (entity != null)
                ? String.Format(
                    "UserName : {1}{0}IsEnabled : {2}{0}FirstName : {3}{0}LastName : {4}{0}",
                    Environment.NewLine, entity.UserName, entity.IsEnabled,
                    entity.Person.FirstName, entity.Person.LastName)
                : null;
        }

        private static bool AreRolesModified(User existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing.UserRoles
                .Select(ur => ur.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return (!AreEqual(existingItems, enabledItems));
        }

        private static void RemoveUnassignedRoles(User existing, RelatedItemsViewModel role)
        {
            var currentItems = role.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing.UserRoles
                .Select(ur => ur.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                existing.UserRoles.Remove(existing.UserRoles
                    .Where(ur => ur.RoleId == id)
                    .Single());
            }
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static string BuildConnectionString(CompanyDb company)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("Server={0};Database={1};", company.Server, company.DbName);
            if (!String.IsNullOrEmpty(company.UserName) && !String.IsNullOrEmpty(company.Password))
            {
                builder.AppendFormat("User ID={0};Password={1};Trusted_Connection=False;MultipleActiveResultSets=True",
                    company.UserName, company.Password);
            }
            else
            {
                builder.Append("Trusted_Connection=True;MultipleActiveResultSets=True");
            }

            return builder.ToString();
        }

        private static void FilterInaccessibleCommands(IList<int> permissions, IList<CommandViewModel> commands)
        {
            int count = commands.Count;
            for (int i = 0; i < count; i++)
            {
                var command = commands[i];
                if (IsTopLevelCommand(command))
                {
                    FilterInaccessibleCommands(permissions, command.Children);
                }
                else if (IsInaccessibleCommand(command, permissions))
                {
                    command.HasPermission = false;
                }
            }
        }

        private static bool IsTopLevelCommand(CommandViewModel command)
        {
            return command.PermissionId == null
                && String.IsNullOrEmpty(command.RouteUrl);
        }

        private static bool IsInaccessibleCommand(CommandViewModel command, IList<int> permissions)
        {
            return command.PermissionId != null
                && !permissions.Contains(command.PermissionId.Value);
        }

        private void AddNewRoles(User existing, RelatedItemsViewModel roleItems)
        {
            var repository = UnitOfWork.GetRepository<Role>();
            var currentItems = existing.UserRoles.Select(ur => ur.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var role = repository.GetByIDWithTracking(item.Id);
                var userRole = new UserRole()
                {
                    User = existing,
                    UserId = existing.Id,
                    Role = role,
                    RoleId = role.Id
                };
                existing.UserRoles.Add(userRole);
            }
        }

        private User GetNewUser(UserViewModel userViewModel)
        {
            var user = Mapper.Map<User>(userViewModel);
            var person = new Person()
            {
                FirstName = userViewModel.PersonFirstName,
                LastName = userViewModel.PersonLastName
            };

            user.Person = person;
            person.User = user;
            return user;
        }

        private IQueryable<User> GetUserPermissionsQuery(int userId)
        {
            var repository = UnitOfWork.GetRepository<User>();
            var query = repository.GetEntityQuery()
                .Where(usr => usr.Id == userId)
                .Include(usr => usr.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions);
            return query;
        }

        private readonly IMetadataRepository _metadata;
    }
}
