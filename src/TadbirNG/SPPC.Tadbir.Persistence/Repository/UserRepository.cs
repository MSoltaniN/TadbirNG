using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Configuration;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت اطلاعات کاربران را پیاده سازی می کند
    /// </summary>
    public class UserRepository : SystemEntityLoggingRepository<User, UserViewModel>, IUserRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        /// <param name="crypto">امکان رمزنگاری اطلاعات را فراهم می کند</param>
        public UserRepository(IRepositoryContext context, ISystemRepository system,
            ICryptoService crypto)
            : base(context, system?.Logger)
        {
            _system = system;
            _crypto = crypto;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، لیست کاربران برنامه را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>لیست کاربران برنامه</returns>
        public async Task<PagedList<UserViewModel>> GetUsersAsync(GridOptions gridOptions = null)
        {
            var users = new List<UserViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                var repository = UnitOfWork.GetAsyncRepository<User>();
                users = await repository
                    .GetEntityQuery(u => u.Person)
                    .Select(user => Mapper.Map<UserViewModel>(user))
                    .ToListAsync();
                users = HandleLoginDateFilter(users, gridOptions);
            }

            await ReadAsync(gridOptions);
            return new PagedList<UserViewModel>(users, gridOptions);
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
            var user = await repository
                .GetSingleByCriteriaAsync(usr => usr.UserName == userName, usr => usr.Person);
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
        /// به روش آسنکرون، اطلاعات یک کاربر را از روی داده های ورود کاربر به برنامه خوانده و برمی گرداند
        /// </summary>
        /// <param name="login">اطلاعات ورود کاربر به برنامه</param>
        /// <returns>اطلاعات به دست آمده برای کاربر</returns>
        public async Task<UserViewModel> GetUserAsync(LoginViewModel login)
        {
            Verify.ArgumentNotNull(login, nameof(login));
            var user = await GetUserAsync(login.UserName);
            await ProcessFailedLoginAsync(user, login);
            return user;
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
                var roleIds = user.UserRoles.Select(ur => ur.RoleId);
                if (roleIds.Contains(AppConstants.AdminRoleId))
                {
                    Array.ForEach(roleIds.ToArray(), id => userContext.Roles.Add(id));
                    return userContext;
                }

                var permissions = new List<PermissionBriefViewModel>();
                var roleRepository = UnitOfWork.GetAsyncRepository<Role>();
                foreach (var roleId in roleIds)
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
            var user = await GetUserAsync(UserContext.Id);
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
        /// به روش آسنکرون، اطلاعات نمایشی تمام کلیدهای میانبر قابل دسترسی توسط کاربر مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه یکتای یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از کلیدهای میانبر قابل دسترسی توسط کاربر</returns>
        public async Task<IList<ShortcutCommandViewModel>> GetUserHotKeysAsync(int userId)
        {
            var shortcuts = await Metadata.GetShortcutCommandsAsync();
            bool isAdmin = UserContext.Roles.Contains(AppConstants.AdminRoleId);
            if (!isAdmin)
            {
                var permissionIds = await GetUserPermissionIdsAsync(userId);
                shortcuts = shortcuts
                    .Where(sc => sc.PermissionId == null
                        || permissionIds.Contains(sc.PermissionId.Value))
                    .ToList();
            }

            return shortcuts;
        }

        /// <summary>
        /// اطلاعات نمایشی تمام دستورات قابل دسترسی توسط کاربر مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی یکی از کاربران موجود</param>
        /// <returns>مجموعه ای از دستورات قابل دسترسی توسط کاربر</returns>
        public async Task<IList<CommandViewModel>> GetUserCommandsAsync(int userId)
        {
            var topCommands = await Metadata.GetTopLevelCommandsAsync();
            bool isAdmin = UserContext.Roles.Contains(AppConstants.AdminRoleId);
            if (!isAdmin)
            {
                var userPermissions = await GetUserPermissionIdsAsync(userId);
                FilterInaccessibleCommands(userPermissions, topCommands);
            }

            return topCommands;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی گزینه های منوی پیش فرض کاربران را خوانده و برمی گرداند
        /// </summary>
        /// <returns>گزینه های منوی پیش فرض کاربران</returns>
        public async Task<IList<CommandViewModel>> GetUserCommandsAsync()
        {
            return await Metadata.GetDefaultCommandsAsync();
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
            Verify.ArgumentNotNull(userRoles, nameof(userRoles));
            int[] removedRoleIds = Array.Empty<int>();
            var repository = UnitOfWork.GetAsyncRepository<UserRole>();
            var existing = await repository.GetByCriteriaAsync(rc => rc.UserId == userRoles.Id);
            if (AreRolesModified(existing, userRoles))
            {
                if (existing.Count > 0)
                {
                    removedRoleIds = RemoveUnassignedRoles(repository, existing, userRoles);
                }

                var newRoleIds = AddNewRoles(repository, existing, userRoles);
                await UnitOfWork.CommitAsync();
                if (removedRoleIds.Length > 0 || newRoleIds.Length > 0)
                {
                    await InsertAssignedItemsLogAsync(newRoleIds, removedRoleIds,
                        userRoles.Id, OperationId.AssignRole);
                }
            }
        }

        /// <summary>
        /// Asynchronously inserts or updates a single user in repository.
        /// </summary>
        /// <param name="userView">Item to insert or update</param>
        public async Task<UserViewModel> SaveUserAsync(UserViewModel userView)
        {
            Verify.ArgumentNotNull(userView, "userView");
            var user = default(User);
            var repository = UnitOfWork.GetAsyncRepository<User>();
            if (userView.Id == 0)
            {
                user = GetNewUser(userView);
                await InsertAsync(repository, user);
            }
            else
            {
                user = await repository.GetByIDAsync(userView.Id, u => u.Person);
                if (user != null)
                {
                    await UpdateAsync(repository, user, userView);
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
            Verify.ArgumentNotNull(companyLogin, nameof(companyLogin));
            Verify.ArgumentNotNull(userContext, nameof(userContext));
            var currentLogin = await GetCurrentLoginAsync(
                userContext.CompanyId, userContext.FiscalPeriodId, userContext.BranchId);
            currentLogin.UserId = userContext.Id;

            userContext.CompanyId = (int)companyLogin.CompanyId;
            userContext.BranchId = (int)companyLogin.BranchId;
            userContext.FiscalPeriodId = (int)companyLogin.FiscalPeriodId;

            await SetCurrentCompanyAsync(userContext.CompanyId);
            var newLogin = await GetCurrentLoginAsync(
                userContext.CompanyId, userContext.FiscalPeriodId, userContext.BranchId);
            userContext.Connection = newLogin.Connection;
            userContext.CompanyName = newLogin.CompanyName;
            userContext.FiscalPeriodName = newLogin.FiscalPeriodName;
            userContext.InventoryMode = newLogin.InventoryMode;
            userContext.BranchName = newLogin.BranchName;
            await OnEnvironmentChangeAsync(currentLogin, newLogin);
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

        /// <inheritdoc/>
        protected override async Task InsertAsync(IRepository<User> repository,
            User entity, OperationId operation = OperationId.Create)
        {
            OnEntityAction(operation);
            Log.Description = Context.Localize(GetState(entity));
            repository.Insert(entity, usr => usr.Person);
            await FinalizeActionAsync(entity);
        }

        /// <inheritdoc/>
        protected override async Task UpdateAsync(IRepository<User> repository,
            User entity, UserViewModel entityView, OperationId operation = OperationId.Edit)
        {
            var clone = CloneUser(entity);
            OnEntityAction(operation);
            UpdateExisting(entityView, entity);
            Log.Description = Context.Localize(
                String.Format("{0} : ({1}) , {2} : ({3})",
                AppStrings.Old, Context.Localize(GetState(clone)),
                AppStrings.New, Context.Localize(GetState(entity))));
            repository.Update(entity, usr => usr.Person);
            await FinalizeActionAsync(entity);
        }

        internal override int? EntityType
        {
            get { return (int)SysEntityTypeId.User; }
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
                    "{0} : {1} , {2} : {3} , {4} : {5}",
                    AppStrings.UserName, entity.UserName, AppStrings.PersonFirstName, entity.Person.FirstName,
                    AppStrings.PersonLastName, entity.Person.LastName)
                : null;
        }

        private IMetadataRepository Metadata
        {
            get { return _system.Metadata; }
        }

        private static bool AreRolesModified(IList<UserRole> existing, RelatedItemsViewModel roleItems)
        {
            var existingItems = existing
                .Select(ur => ur.RoleId)
                .ToArray();
            var enabledItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id)
                .ToArray();
            return !AreEqual(existingItems, enabledItems);
        }

        private static int[] RemoveUnassignedRoles(
            IRepository<UserRole> repository, IList<UserRole> existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = roleItems.RelatedItems
                .Where(item => item.IsSelected)
                .Select(item => item.Id);
            var removedItems = existing
                .Select(ur => ur.RoleId)
                .Where(id => !currentItems.Contains(id))
                .ToArray();
            foreach (int id in removedItems)
            {
                var removed = existing
                    .Where(rc => rc.RoleId == id)
                    .Single();
                repository.Delete(removed);
            }

            return removedItems;
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
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
                    int index = commands.IndexOf(command);
                    commands.RemoveAt(index);
                    count--;
                    i--;
                }

                if (IsTopLevelCommand(command) && command.Children.Count == 0)
                {
                    int index = commands.IndexOf(command);
                    commands.RemoveAt(index);
                    count--;
                    i--;
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

        private static List<UserViewModel> HandleLoginDateFilter(
            List<UserViewModel> users, GridOptions gridOptions)
        {
            var filtered = users;
            var filters = gridOptions?.Filter?.GetAllFilters();
            if (filters != null && filters.Any())
            {
                var dateFilter = filters
                    .Where(f => f.FieldName == "lastLoginDate")
                    .FirstOrDefault();
                if (dateFilter != null)
                {
                    filtered = users
                        .Where(FromGridFilter(dateFilter))
                        .ToList();
                    gridOptions.Filter.RemoveFilter(dateFilter);
                }
            }

            return filtered;
        }

        private static Func<UserViewModel, bool> FromGridFilter(GridFilter gridFilter)
        {
            Func<UserViewModel, bool> func = null;
            DateTime value = DateTime.Now.Parse(gridFilter.Value, false);
            switch (gridFilter.Operator)
            {
                case GridFilterOperator.IsEqualTo:
                    func = user => user.LastLoginDate.HasValue
                        && (value.TimeOfDay == TimeSpan.Zero
                            ? user.LastLoginDate.Value.Date == value.Date
                            : user.LastLoginDate.Value == value);
                    break;
                case GridFilterOperator.IsNotEqualTo:
                    func = user => user.LastLoginDate.HasValue
                        && (value.TimeOfDay == TimeSpan.Zero
                            ? user.LastLoginDate.Value.Date != value.Date
                            : user.LastLoginDate.Value != value);
                    break;
                case GridFilterOperator.IsLessThan:
                    func = user => user.LastLoginDate.HasValue
                        && (value.TimeOfDay == TimeSpan.Zero
                            ? user.LastLoginDate.Value.Date < value.Date
                            : user.LastLoginDate.Value < value);
                    break;
                case GridFilterOperator.IsLessOrEqualTo:
                    func = user => user.LastLoginDate.HasValue
                        && (value.TimeOfDay == TimeSpan.Zero
                            ? user.LastLoginDate.Value.Date <= value.Date
                            : user.LastLoginDate.Value <= value);
                    break;
                case GridFilterOperator.IsGreaterThan:
                    func = user => user.LastLoginDate.HasValue
                        && (value.TimeOfDay == TimeSpan.Zero
                            ? user.LastLoginDate.Value.Date > value.Date
                            : user.LastLoginDate.Value > value);
                    break;
                case GridFilterOperator.IsGreaterOrEqualTo:
                    func = user => user.LastLoginDate.HasValue
                        && (value.TimeOfDay == TimeSpan.Zero
                            ? user.LastLoginDate.Value.Date >= value.Date
                            : user.LastLoginDate.Value >= value);
                    break;
            }

            return func;
        }

        private static int[] AddNewRoles(
            IRepository<UserRole> repository, IList<UserRole> existing, RelatedItemsViewModel roleItems)
        {
            var currentItems = existing.Select(rc => rc.RoleId);
            var newItems = roleItems.RelatedItems
                .Where(item => item.IsSelected
                    && !currentItems.Contains(item.Id));
            foreach (var item in newItems)
            {
                var userRole = new UserRole()
                {
                    RoleId = item.Id,
                    UserId = roleItems.Id
                };
                repository.Insert(userRole);
            }

            return newItems
                .Select(item => item.Id)
                .ToArray();
        }

        private static User CloneUser(User user)
        {
            return new User()
            {
                Id = user.Id,
                UserName = user.UserName,
                IsEnabled = user.IsEnabled,
                Person = new Person()
                {
                    Id = user.Person.Id,
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName
                }
            };
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

        private async Task<CompanyLoginViewModel> GetCurrentLoginAsync(
            int companyId, int fiscalPeriodId, int branchId)
        {
            var login = new CompanyLoginViewModel()
            {
                CompanyId = companyId,
                FiscalPeriodId = fiscalPeriodId,
                BranchId = branchId
            };

            var companyRepo = UnitOfWork.GetAsyncRepository<CompanyDb>();
            var company = await companyRepo.GetByIDAsync(companyId);
            login.CompanyName = company?.Name;
            login.Connection = BuildConnectionString(company);
            if (!String.IsNullOrEmpty(login.Connection))
            {
                UnitOfWork.UseCompanyContext();
                var fiscalPeriodRepo = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
                var fiscalPeriod = await fiscalPeriodRepo.GetByIDAsync(fiscalPeriodId);
                var repository = UnitOfWork.GetAsyncRepository<Setting>();
                var systemConfig = await repository.GetByIDAsync((int)SettingId.SystemConfiguration);

                login.FiscalPeriodName = fiscalPeriod?.Name;
                login.InventoryMode =
                    systemConfig != null
                    ? JsonHelper.To<SystemConfig>(systemConfig.Values).InventoryMode
                    :
                    (int)InventoryMode.Perpetual;

                var branchRepo = UnitOfWork.GetAsyncRepository<Branch>();
                var branch = await branchRepo.GetByIDAsync(branchId);
                login.BranchName = branch?.Name;

                UnitOfWork.UseSystemContext();
            }

            return login;
        }

        private async Task ProcessFailedLoginAsync(UserViewModel user, LoginViewModel login)
        {
            string description;
            if (user == null)
            {
                description = String.Format("{0} : {1}", AppStrings.InvalidUserName, login.UserName);
            }
            else if (!user.IsEnabled)
            {
                description = String.Format("{0} : {1}", AppStrings.DisabledUser, login.UserName);
            }
            else if (!_crypto.ValidateHash(login.Password, user.Password))
            {
                description = AppStrings.InvalidPassword;
            }
            else
            {
                description = String.Empty;
            }

            if (user == null || !user.IsEnabled || !_crypto.ValidateHash(login.Password, user.Password))
            {
                int? userId = user?.Id;
                await OnSystemLoginAsync(userId, description);
            }
        }

        private readonly ISystemRepository _system;
        private readonly ICryptoService _crypto;
    }
}
