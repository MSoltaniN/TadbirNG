using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.CashFlow;
using SPPC.Tadbir.Model.Contact;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.CashFlow;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت صندوق ها را پیاده سازی می کند
    /// </summary>
    public class CashRegisterRepository : EntityLoggingRepository<CashRegister, CashRegisterViewModel>, ICashRegisterRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CashRegisterRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه صندوق ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از صندوق های تعریف شده</returns>
        public async Task<PagedList<CashRegisterViewModel>> GetCashRegistersAsync(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var cashRegisters = new List<CashRegisterViewModel>();
            if (options.Operation != (int)OperationId.Print)
            {
                var query = Repository.GetAllQuery<CashRegister>(ViewId.CashRegister);
                cashRegisters = await query
                    .Select(item => Mapper.Map<CashRegisterViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(options);
            return new PagedList<CashRegisterViewModel>(cashRegisters, options);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی یکی از صندوق های موجود</param>
        /// <returns>صندوق مشخص شده با شناسه عددی</returns>
        public async Task<CashRegisterViewModel> GetCashRegisterAsync(int cashRegisterId)
        {
            CashRegisterViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            var cashRegister = await repository.GetByIDAsync(cashRegisterId);
            if (cashRegister != null)
            {
                item = Mapper.Map<CashRegisterViewModel>(cashRegister);
            }

            return item;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک صندوق را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="cashRegister">صندوق مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی صندوق ایجاد یا اصلاح شده</returns>
        public async Task<CashRegisterViewModel> SaveCashRegisterAsync(CashRegisterViewModel cashRegister)
        {
            Verify.ArgumentNotNull(cashRegister, nameof(cashRegister));
            CashRegister cashRegisterModel;
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            if (cashRegister.Id == 0)
            {
                cashRegisterModel = Mapper.Map<CashRegister>(cashRegister);
                await InsertAsync(repository, cashRegisterModel);
            }
            else
            {
                cashRegisterModel = await repository.GetByIDAsync(cashRegister.Id);
                if (cashRegisterModel != null)
                {
                    await UpdateAsync(repository, cashRegisterModel, cashRegister);
                }
            }

            return Mapper.Map<CashRegisterViewModel>(cashRegisterModel);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی صندوق مورد نظر برای حذف</param>
        public async Task DeleteCashRegisterAsync(int cashRegisterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            var cashRegister = await repository.GetByIDAsync(cashRegisterId);
            if (cashRegister != null)
            {
                await DeleteAsync(repository, cashRegister);
            }
        }

        /// <summary>
        /// به روش آسنکرون، صندوق های مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="cashRegisterIds">مجموعه ای از شناسه های عددی صندوق های مورد نظر برای حذف</param>
        public async Task DeleteCashRegistersAsync(IList<int> cashRegisterIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            foreach (int cashRegisterId in cashRegisterIds)
            {
                var cashRegister = await repository.GetByIDAsync(cashRegisterId);
                if (cashRegister != null)
                {
                    await DeleteNoLogAsync(repository, cashRegister);
                }
            }

            await OnEntityGroupDeleted(cashRegisterIds);
        }

        /// <summary>
        /// به روش آسنکرون، کاربران اختصاص داده شده به صندوق را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashRegisterId">شناسه عددی یکی از صندوق های موجود</param>
        /// <returns>مجموعه ای از کاربران تخصیص داده شده به صندوق</returns>
        public async Task<RelatedItemsViewModel> GetCashRegisterUsersAsync(int cashRegisterId)
        {
            RelatedItemsViewModel userCashRegisters = null;
            var repository = UnitOfWork.GetAsyncRepository<CashRegister>();
            var cashRegister = await repository.GetByIDAsync(cashRegisterId);
            if (cashRegister != null)
            {
                var userCashRegRepository = UnitOfWork.GetAsyncRepository<UserCashRegister>();
                var currUserCashRegisters = await userCashRegRepository
                    .GetByCriteriaAsync(ucr => ucr.CashRegisterId == cashRegisterId);
                var assignedUserIds = await userCashRegRepository
                    .GetEntityQuery()
                    .Select(ucr => ucr.UserId)
                    .ToArrayAsync();

                IEnumerable<int> validRoleIds = null;
                if (cashRegister.BranchScope != (short)BranchScope.AllBranches)
                {
                    validRoleIds = GetValidRoleIds(cashRegister.BranchId, cashRegister.BranchScope);
                }
                UnitOfWork.UseSystemContext();
                var userRepository = UnitOfWork.GetAsyncRepository<User>();
                var selectedUserIds = currUserCashRegisters.Select(ucr => ucr.UserId);
                var selectedUsers = await userRepository
                    .GetEntityQuery()
                    .Include(u => u.Person)
                    .Where(u => selectedUserIds.Contains(u.Id))
                    .Select(u => Mapper.Map<RelatedItemViewModel>(u))
                    .ToArrayAsync();
                Array.ForEach(selectedUsers, u => u.IsSelected = true);

                IEnumerable<RelatedItemViewModel> unSelectedUsers = null;
                if (cashRegister.BranchScope != (short)BranchScope.AllBranches)
                {
                    var validUserIds = GetUserIdsByRoleIds(validRoleIds);
                    foreach(var user in selectedUsers)
                    {
                        if(!validUserIds.Contains(user.Id))
                        {
                            user.IsValid = false;
                        }
                    }
                    unSelectedUsers = userRepository
                        .GetEntityQuery()
                        .Include(u => u.Person)
                        .Where(u => !assignedUserIds.Contains(u.Id) &&
                             validUserIds.Contains(u.Id))
                        .Select(u => Mapper.Map<RelatedItemViewModel>(u))
                        .ToArray();
                }
                else
                {
                    unSelectedUsers = await userRepository
                        .GetEntityQuery()
                        .Include(u => u.Person)
                        .Where(u => !assignedUserIds.Contains(u.Id))
                        .Select(u => Mapper.Map<RelatedItemViewModel>(u))
                        .ToArrayAsync();
                }

                var userList = selectedUsers
                    .Concat(unSelectedUsers)
                    .OrderBy(u => u.Id)
                    .ToArray();
                userCashRegisters = new RelatedItemsViewModel
                {
                    Id = cashRegisterId
                };
                Array.ForEach(userList, u => userCashRegisters.RelatedItems.Add(u));
            }

            return userCashRegisters;
        }

        /// <summary>
        /// به روش آسنکرون، کاربران را به صندوق تخصیص می دهد
        /// </summary>
        /// <param name="userCashRegisters">اطلاعات نمایشی کاربران</param>
        public async Task SaveCashRegisterUsersAsync(RelatedItemsViewModel userCashRegisters)
        {
            Verify.ArgumentNotNull(userCashRegisters, nameof(userCashRegisters));
            int[] removedUserIds = Array.Empty<int>();
            var repository = UnitOfWork.GetAsyncRepository<UserCashRegister>();
            var existing = await repository
                .GetByCriteriaAsync(ucr => ucr.CashRegisterId == userCashRegisters.Id);
            if (AreUsersModified(existing, userCashRegisters))
            {
                if (existing.Count > 0)
                {
                    removedUserIds = RemoveUnassinedUsers(repository, existing, userCashRegisters);
                }

                var newUserIds = AddNewUsers(repository, existing, userCashRegisters);
                await UnitOfWork.CommitAsync();
                if (removedUserIds.Length > 0 || newUserIds.Length > 0)
                {
                    await InsertAssignedItemsLogAsync(newUserIds, removedUserIds,
                        userCashRegisters.Id, OperationId.AssignCashRegisterUser);
                }
            }
        }

        /// <summary>
        /// به روش آسنکرون، بررسی می کند که نام صندوق تکراری هست یا خیر
        /// </summary>
        /// <param name="cashRegister">صندوق مورد نظر</param>
        /// <returns>برای نام تکراری مقدار درست و در غیر این صورت مقدار نادرست برمی گرداند</returns>
        public async Task<bool> IsDuplicateCashRegisterName(CashRegisterViewModel cashRegister)
        {
            Verify.ArgumentNotNull(cashRegister, nameof(cashRegister));
            var reopository = UnitOfWork.GetAsyncRepository<CashRegister>();
            return await reopository
                .GetEntityQuery()
                .AnyAsync(cr => cr.Name.ToLower() == cashRegister.Name.ToLower()
                    && cr.Id != cashRegister.Id);
        }

        /// <summary>
        /// به روش آسنکرون، بررسی می کند که به صندوق کاربر اختصاص یافته هست یا خیر
        /// </summary>
        /// <param name="cashRegisterId">شناسه یکتای صندوق مورد نظر</param>
        /// <returns>اگر کاربر به صندوق اختصاص یافته مقدار درست و در غیر این صورت 
        /// مقدار نادرست برمی گرداند</returns>
        public async Task<bool> HasAssignedUsersToCashRegAsync(int cashRegisterId)
        {
            var repository = UnitOfWork.GetAsyncRepository<UserCashRegister>();
            return await repository
                   .GetEntityQuery()
                   .AnyAsync(ucr => ucr.CashRegisterId == cashRegisterId);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.CashRegister; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="cashRegisterViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="cashRegister">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CashRegisterViewModel cashRegisterViewModel, CashRegister cashRegister)
        {
            cashRegister.Name = cashRegisterViewModel.Name;
            cashRegister.BranchScope = cashRegisterViewModel.BranchScope;
            cashRegister.Description = cashRegisterViewModel.Description;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CashRegister entity)
        {
            return entity != null
                ? $"{AppStrings.Name} : {entity.Name}, {AppStrings.Description} : {entity.Description}"
                : String.Empty;
        }

        /// <summary>
        /// به روش آسنکرون، لیست رشته ای از عناوین آیتم های ورودی را بر اساس کد عملیاتی برمی گرداند 
        /// </summary>
        /// <param name="itemIds">لیستی از شناسه آیتم های مورد نظر</param>
        /// <param name="operationId">کد عملیاتی مورد نظر</param>
        /// <returns>لیست رشته ای از عناوین آیتم ها</returns>
        protected override async Task<string[]> GetItemNamesAsync(int[] itemIds, OperationId operationId)
        {
            if (operationId == OperationId.AssignCashRegisterUser)
            {
                UnitOfWork.UseSystemContext();
                var userRepository = UnitOfWork.GetAsyncRepository<User>();
                return await userRepository
                    .GetEntityQuery()
                    .Where(u => itemIds.Contains(u.Id))
                    .Include(u => u.Person)
                    .Select(u => Mapper.Map<RelatedItemViewModel>(u).Name)
                    .ToArrayAsync();
            }
            return Array.Empty<string>();
        }

        private static bool AreEqual(IEnumerable<int> left, IEnumerable<int> right)
        {
            return left.Count() == right.Count()
                && left.All(value => right.Contains(value));
        }

        private static bool AreUsersModified(IList<UserCashRegister> existing, RelatedItemsViewModel userItems)
        {
            var exsitigUserIds = existing
                .Select(ucr => ucr.UserId)
                .ToArray();
            var newUserIds = userItems.RelatedItems
                .Select(item => item.Id)
                .ToArray();
            return !AreEqual(exsitigUserIds, newUserIds);
        }

        private static int[] RemoveUnassinedUsers(
            IRepository<UserCashRegister> repository, IList<UserCashRegister> existing, RelatedItemsViewModel userItems)
        {
            var currentUserIds = userItems.RelatedItems
                .Select(item => item.Id)
                .ToArray();
            var RemovedUsers = existing
                .Where(ucr => !currentUserIds.Contains(ucr.UserId))
                .ToArray();
            foreach (var user in RemovedUsers)
            {
                repository.Delete(user);
            }

            return RemovedUsers
                .Select(u=>u.UserId)
                .ToArray();
        }

        private int[] AddNewUsers(
            IRepository<UserCashRegister> repository, IList<UserCashRegister> existing, RelatedItemsViewModel userItems)
        {
            var existingUserIds = existing.Select(ucr => ucr.UserId);
            var newUserItems = userItems.RelatedItems
                .Where(item => !existingUserIds.Contains(item.Id));
            foreach (var item in newUserItems)
            {
                UserCashRegister userCashRegister = new UserCashRegister
                {
                    UserId = item.Id,
                    CashRegisterId = userItems.Id
                };
                repository.Insert(userCashRegister);
            }
            
            return newUserItems
                .Select(item => item.Id)
                .ToArray();
        }

        private IEnumerable<int> GetValidRoleIds(int branchId, int branchScope)
        {
            IEnumerable<int> validRoleIds = null;
            if (branchScope == (short)BranchScope.CurrentBranchAndChildren)
            {
                var childBranches = GetChildTree(branchId);
                var roleBranchRepository = UnitOfWork.GetAsyncRepository<RoleBranch>();
                validRoleIds = roleBranchRepository
                    .GetEntityQuery()
                    .Where(rb => rb.BranchId == branchId
                        || childBranches.Contains(rb.BranchId))
                    .Select(rb => rb.RoleId)
                    .ToArray();
            }
            else if (branchScope == (short)BranchScope.CurrentBranch)
            {
                var roleBranchRepository = UnitOfWork.GetAsyncRepository<RoleBranch>();
                validRoleIds = roleBranchRepository
                    .GetEntityQuery()
                    .Where(rb => rb.BranchId == branchId)
                    .Select(rb => rb.RoleId)
                    .ToArray();
            }

            return validRoleIds;
        }

        private IEnumerable<int> GetUserIdsByRoleIds(IEnumerable<int> validRoleIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<UserRole>();
            var validUserIds = repository
                    .GetEntityQuery()
                    .Where(ur => validRoleIds.Contains(ur.RoleId))
                    .Select(ur => ur.UserId)
                    .ToArray();
            return validUserIds;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
