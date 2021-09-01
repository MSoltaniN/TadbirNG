using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
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
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var users = await repository
                .GetEntityQuery(u => u.Person)
                .Select(user => Mapper.Map<UserViewModel>(user))
                .ToListAsync();
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
        /// اطلاعات نمایشی تمام دستورات قابل دسترسی توسط کاربر مشخص شده را از دیتابیس خوانده و برمی گرداند
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
            var repository = UnitOfWork.GetAsyncRepository<UserRole>();
            var existing = await repository.GetByCriteriaAsync(rc => rc.UserId == userRoles.Id);
            if (AreRolesModified(existing, userRoles))
            {
                if (existing.Count > 0)
                {
                    RemoveUnassignedRoles(repository, existing, userRoles);
                }

                AddNewRoles(repository, existing, userRoles);
                await UnitOfWork.CommitAsync();
                OnEntityAction(OperationId.AssignRole);
                Log.Description = await GetUserRoleDescriptionAsync(userRoles.Id);
                await TrySaveLogAsync();
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

        private static void RemoveUnassignedRoles(
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

        private void AddNewRoles(
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

        private User CloneUser(User user)
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

        private bool CheckPassword(string passwordHash, string password)
        {
            byte[] passwordHashBytes = Transform.FromHexString(passwordHash);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            return _crypto.ValidateHash(passwordBytes, passwordHashBytes);
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
                login.FiscalPeriodName = fiscalPeriod?.Name;
                login.InventoryMode = fiscalPeriod != null
                    ? fiscalPeriod.InventoryMode
                    : (int)InventoryMode.Perpetual;

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
            else if (!CheckPassword(user.Password, login.Password))
            {
                description = AppStrings.InvalidPassword;
            }
            else
            {
                description = String.Empty;
            }

            if (user == null || !user.IsEnabled || !CheckPassword(user.Password, login.Password))
            {
                int? userId = user?.Id;
                await OnSystemLoginAsync(userId, description);
            }
        }

        private async Task<string> GetUserRoleDescriptionAsync(int userId)
        {
            string description = String.Empty;
            var repository = UnitOfWork.GetAsyncRepository<User>();
            var user = await repository.GetByIDAsync(userId);
            if (user != null)
            {
                string template = Context.Localize(AppStrings.RolesAssignedToUser);
                description = String.Format(template, user.UserName);
            }

            return description;
        }

        private readonly ISystemRepository _system;
        private readonly ICryptoService _crypto;
    }
}
