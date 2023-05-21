using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Check;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دسته چک ها را پیاده سازی می کند
    /// </summary>
    public class CheckBookRepository : EntityLoggingRepository<CheckBook, CheckBookViewModel>, ICheckBookRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CheckBookRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شناسه عددی</returns>
        public async Task<CheckBookViewModel> GetCheckBookAsync(int checkBookId)
        {
            CheckBookViewModel checkBook = null;
            var existing = await GetCheckBookQuery(checkBookId)
                .FirstOrDefaultAsync();
            if (existing != null)
            {
                checkBook = Mapper.Map<CheckBookViewModel>(existing);
                var pages = new CheckBookPages(
                    checkBook.StartNo, checkBook.EndNo, checkBook.SeriesNo, checkBook.SayyadStartNo);
                checkBook.PageCount = pages.Count;
            }

            await ReadAsync(new GridOptions(), GetState(existing));
            return checkBook;
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک جدید با مقادیر پیشنهادی را برمی گرداند
        /// </summary>
        /// <returns>دسته چک جدید با مقادیر پیشنهادی</returns>
        public async Task<CheckBookViewModel> GetNewCheckBookAsync()
        {
            int lastNo = await GetLastCheckBookNoAsync();
            var newCheckBook = new CheckBookViewModel()
            {
                CheckBookNo = lastNo + 1,
                BranchId = UserContext.BranchId,
            };

            return newCheckBook;
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookNo">شماره یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شماره</returns>
        public async Task<CheckBookViewModel> GetCheckBookByNoAsync(int checkBookNo)
        {
            var byNo = default(CheckBookViewModel);
            var checkBookByNo = await Repository
                .GetAllOperationQuery<CheckBook>(ViewId.CheckBook,
                    cb => cb.Account, cb => cb.DetailAccount, cb => cb.CostCenter, cb => cb.Project)
                .Where(cb => cb.CheckBookNo == checkBookNo)
                .FirstOrDefaultAsync();
            if (checkBookByNo != null)
            {
                byNo = Mapper.Map<CheckBookViewModel>(checkBookByNo);
                var pages = new CheckBookPages(byNo.StartNo, byNo.EndNo, byNo.SeriesNo, byNo.SayyadStartNo);
                byNo.PageCount = pages.Count;
                await ReadAsync(new GridOptions(), GetState(checkBookByNo));
            }

            return byNo;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک دسته چک را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="checkBook">دسته چک مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی دسته چک ایجاد یا اصلاح شده</returns>
        public async Task<CheckBookViewModel> SaveCheckBookAsync(CheckBookViewModel checkBook)
        {
            Verify.ArgumentNotNull(checkBook, nameof(checkBook));
            CheckBook checkBookModel;
            var pages = new CheckBookPages(
                checkBook.StartNo, checkBook.SeriesNo, checkBook.SayyadStartNo, checkBook.PageCount);
            checkBook.EndNo = pages.LastPage;
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            if (checkBook.Id == 0)
            {
                checkBookModel = Mapper.Map<CheckBook>(checkBook);
                checkBookModel.CreatedById = UserContext.Id;
                checkBookModel.ModifiedById = UserContext.Id;
                checkBookModel.CreatedDate = DateTime.Now;
                await InsertAsync(repository, checkBookModel);
            }
            else
            {
                checkBookModel = await repository.GetByIDAsync(checkBook.Id);
                if (checkBookModel != null)
                {
                    await UpdateAsync(repository, checkBookModel, checkBook);
                }
            }

            var saved = Mapper.Map<CheckBookViewModel>(checkBookModel);
            pages = new CheckBookPages(saved.StartNo, saved.EndNo, checkBook.SeriesNo, checkBook.SayyadStartNo);
            saved.PageCount = pages.Count;
            return saved;
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی دسته چک مورد نظر برای حذف</param>
        public async Task DeleteCheckBookAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var checkBook = await repository.GetByIDWithTrackingAsync(checkBookId, cb => cb.Pages);
            if (checkBook != null)
            {
                checkBook.Pages.Clear();
                await DeleteAsync(repository, checkBook);
            }
        }

        /// <summary>
        /// به روش آسنکرون، اولین دسته چک را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>اولین دسته چک</returns>
        public async Task<CheckBookViewModel> GetFirstCheckBookAsync(GridOptions gridOptions = null)
        {
            var checkBooks = await Repository.GetAllOperationAsync<CheckBook>(
                ViewId.CheckBook,
                cb => cb.Account, cb => cb.DetailAccount, cb => cb.CostCenter, cb => cb.Project);
            var options = gridOptions ?? new GridOptions();
            var firstCheckBook = checkBooks
                .OrderBy(c => c.IssueDate)
                .Select(cb => Mapper.Map<CheckBookViewModel>(cb))
                .Apply(options, false)
                .FirstOrDefault();
            if (firstCheckBook != null)
            {
                await SetCheckBookNavigationAsync(firstCheckBook, options);
            }

            return firstCheckBook;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دسته چک قبلی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issueDate">تاریخ صدور دسته چک در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>دسته چک قبلی</returns>
        public async Task<CheckBookViewModel> GetPreviousCheckBookAsync(
            DateTime issueDate, GridOptions gridOptions = null)
        {
            var checkBooks = await Repository.GetAllOperationAsync<CheckBook>(
                ViewId.CheckBook,
                cb => cb.Account, cb => cb.DetailAccount, cb => cb.CostCenter, cb => cb.Project);
            var options = gridOptions ?? new GridOptions();
            var previousCheckBook = checkBooks
                .OrderByDescending(c => c.IssueDate)
                .Select(cb => Mapper.Map<CheckBookViewModel>(cb))
                .Apply(options, false)
                .Where(cb => cb.IssueDate < issueDate)
                .FirstOrDefault();
            if (previousCheckBook != null)
            {
                await SetCheckBookNavigationAsync(previousCheckBook, options);
            }

            return previousCheckBook;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات دسته چک بعدی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="issueDate">تاریخ صدور دسته چک در برنامه</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>دسته چک بعدی</returns>
        public async Task<CheckBookViewModel> GetNextCheckBookAsync(
            DateTime issueDate, GridOptions gridOptions = null)
        {
            var checkBooks = await Repository.GetAllOperationAsync<CheckBook>(
                ViewId.CheckBook,
                cb => cb.Account, cb => cb.DetailAccount, cb => cb.CostCenter, cb => cb.Project);
            var options = gridOptions ?? new GridOptions();
            var nextCheckBook = checkBooks
                .OrderBy(c => c.IssueDate)
                .Select(cb => Mapper.Map<CheckBookViewModel>(cb))
                .Apply(options, false)
                .Where(cb => cb.IssueDate > issueDate)
                .FirstOrDefault();
            if (nextCheckBook != null)
            {
                await SetCheckBookNavigationAsync(nextCheckBook, options);
            }

            return nextCheckBook;
        }

        /// <summary>
        /// به روش آسنکرون، آخرین دسته چک را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>آخرین دسته چک</returns>
        public async Task<CheckBookViewModel> GetLastCheckBookAsync(GridOptions gridOptions = null)
        {
            var checkBooks = await Repository.GetAllOperationAsync<CheckBook>(
                ViewId.CheckBook,
                cb => cb.Account, cb => cb.DetailAccount, cb => cb.CostCenter, cb => cb.Project);
            var options = gridOptions ?? new GridOptions();
            var lastCheckBook = checkBooks
                .OrderByDescending(c => c.IssueDate)
                .Select(cb => Mapper.Map<CheckBookViewModel>(cb))
                .Apply(options, false)
                .FirstOrDefault();
            if (lastCheckBook != null)
            {
                await SetCheckBookNavigationAsync(lastCheckBook, options);
            }

            return lastCheckBook; 
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا دسته چک دارای برگه هست یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <returns>در حالتی که دسته چک دارای برگه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> HasPagesAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            int count = await repository.GetCountByCriteriaAsync(ch => ch.CheckBookId == checkBookId);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که دسته چک وجود دارد یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که دسته چک وجود داشته باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> ExistsCheckBookAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            int count = await repository.GetCountByCriteriaAsync(ch => ch.Id == checkBookId);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا نام دسته چک مورد نظر تکراری است یا نه
        /// </summary>
        /// <param name="checkBook">دسته چکی که تکراری بودن نام آن باید بررسی شود</param>
        /// <returns>مقدار بولی درست در صورت تکراری بودن نام، در غیر این صورت مقدار بولی نادرست</returns>
        public async Task<bool> IsDuplicateCheckBookNameAsync(CheckBookViewModel checkBook)
        {
            Verify.ArgumentNotNull(checkBook, nameof(checkBook));
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            int count = await repository
                .GetCountByCriteriaAsync(
                    c => c.Id != checkBook.Id
                    && c.Name == checkBook.Name
                    && c.BranchId == checkBook.BranchId);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حداقل یک برگ از دسته چک با چک ارتباط دارد یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که حداقل یک برگ از دسته چک ارتباط داشته باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsConnectedToCheckAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            int count = await repository
                .GetCountByCriteriaAsync(
                    c => c.CheckBookId == checkBookId 
                    && c.CheckId != null);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا حداقل یک برگ از دسته ابطال شده است یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که حداقل یک برگ از دسته چک ابطال شد باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> ExistsCancelledPage(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            int count = await repository
                .GetCountByCriteriaAsync(
                    c => c.CheckBookId == checkBookId
                         && c.Status == CheckBookPageState.Canceled);
            return count > 0;
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.CheckBook; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="checkBookView">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="checkBook">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CheckBookViewModel checkBookView, CheckBook checkBook)
        {
            checkBook.CheckBookNo = checkBookView.CheckBookNo;
            checkBook.Name = checkBookView.Name;
            checkBook.IssueDate = checkBookView.IssueDate;
            checkBook.StartNo = checkBookView.StartNo;
            checkBook.EndNo = checkBookView.EndNo;
            checkBook.BankName = checkBookView.BankName;
            checkBook.IsArchived = checkBookView.IsArchived;
            checkBook.SayyadStartNo = checkBookView.SayyadStartNo;
            checkBook.SeriesNo = checkBookView.SeriesNo;
            checkBook.ModifiedById = UserContext.Id;
            checkBook.AccountId = checkBookView.FullAccount.Account.Id;
            checkBook.DetailAccountId = checkBookView.FullAccount.DetailAccount.Id > 0
                ? checkBookView.FullAccount.DetailAccount.Id
                : null;
            checkBook.CostCenterId = checkBookView.FullAccount.CostCenter.Id > 0
                ? checkBookView.FullAccount.CostCenter.Id
                : null;
            checkBook.ProjectId = checkBookView.FullAccount.Project.Id > 0
                ? checkBookView.FullAccount.Project.Id
                : null;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CheckBook entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7} , {8} : {9} , {10} : {11}",
                    AppStrings.CheckBookNo, entity.CheckBookNo, AppStrings.CheckBookName, entity.Name,
                    AppStrings.IssueDate, entity.IssueDate, AppStrings.StartNo, entity.StartNo,
                    AppStrings.EndNo, entity.EndNo, AppStrings.BankName, entity.BankName)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، شماره آخرین دسته چک موجود را برمی گرداند
        /// </summary>
        protected async Task<int> GetLastCheckBookNoAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var lastCheckBook = await repository
                .GetEntityQuery()
                .Where(checkBook => checkBook.BranchId == UserContext.BranchId)
                .OrderByDescending(checkBook => checkBook.CheckBookNo)
                .FirstOrDefaultAsync();
            return (lastCheckBook != null) ? lastCheckBook.CheckBookNo ?? 0 : 0;
        }

        private IQueryable<CheckBook> GetCheckBookQuery(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var query = repository
                .GetEntityQuery(
                    cb => cb.Account, cb => cb.DetailAccount, cb => cb.CostCenter, cb => cb.Project)
                .Where(cb => cb.Id == checkBookId);
            return query;
        }

        private async Task SetCheckBookNavigationAsync(CheckBookViewModel checkBook, GridOptions gridOptions = null)
        {
            int nextCount, prevCount;
            var options = gridOptions ?? new GridOptions();
            var pages = new CheckBookPages(
                checkBook.StartNo, checkBook.EndNo, checkBook.SeriesNo, checkBook.SayyadStartNo);
            checkBook.PageCount = pages.Count;
            var query = Repository
                .GetAllOperationQuery<CheckBook>(ViewId.CheckBook)
                .Where(cb => cb.IssueDate <= checkBook.IssueDate
                    && cb.Id != checkBook.Id);
            if (!options.IsEmpty)
            {
                var items = await query.ToListAsync();
                prevCount = items
                    .Select(cb => Mapper.Map<CheckBookViewModel>(cb))
                    .ApplyQuickFilter(options, false)
                    .Apply(options, false)
                    .Count();
            }
            else
            {
                prevCount = await query.CountAsync();
            }

            query = Repository
                .GetAllOperationQuery<CheckBook>(ViewId.CheckBook)
                .Where(cb => cb.IssueDate >= checkBook.IssueDate
                    && cb.Id != checkBook.Id);
            if (!options.IsEmpty)
            {
                var items = await query.ToListAsync();
                nextCount = items
                    .Select(cb => Mapper.Map<CheckBookViewModel>(cb))
                    .ApplyQuickFilter(options, false)
                    .Apply(options, false)
                    .Count();
            }
            else
            {
                nextCount = await query.CountAsync();
            }

            checkBook.HasPrevious = prevCount > 0;
            checkBook.HasNext = nextCount > 0;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
