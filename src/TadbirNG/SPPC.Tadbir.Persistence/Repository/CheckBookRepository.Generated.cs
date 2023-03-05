using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Reporting;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// <param name="checkBookId">شناسه عددی یکی از دسته چک ها موجود</param>
        /// <returns>دسته چک مشخص شده با شناسه عددی</returns>
        public async Task<CheckBookViewModel> GetCheckBookAsync(int checkBookId)
        {
            CheckBookViewModel checkbook = null;
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var existing = await repository.GetByIDAsync(checkBookId);
            if (existing != null)
            {
                checkbook = Mapper.Map<CheckBookViewModel>(existing);
            }

            return checkbook;
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookNo">شماره یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شماره</returns>
        public async Task<CheckBookViewModel> GetCheckBookByNoAsync(int checkBookNo)
        {
            var byNo = default(CheckBookViewModel);
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var checkBookByNo = Repository.ApplyRowFilter(await repository.GetFirstByCriteriaAsync(
                v => v.No == checkBookNo), ViewId.CheckBook);
            if (checkBookByNo != null)
            {
                byNo = Mapper.Map<CheckBookViewModel>(checkBookByNo);
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
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            if (checkBook.Id == 0)
            {
                checkBookModel = Mapper.Map<CheckBook>(checkBook);
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

            return Mapper.Map<CheckBookViewModel>(checkBookModel);
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی دسته چک مورد نظر برای حذف</param>
        public async Task DeleteCheckBookAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var checkBook = await repository.GetByIDAsync(checkBookId);
            if (checkBook != null)
            {
                await DeleteAsync(repository, checkBook);
            }
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک ها مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="checkBookIds">مجموعه ای از شناسه های عددی دسته چک ها مورد نظر برای حذف</param>
        public async Task DeleteCheckBooksAsync(IList<int> checkBookIds)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            foreach (int checkBookId in checkBookIds)
            {
                var checkBook = await repository.GetByIDAsync(checkBookId);
                if (checkBook != null)
                {
                    await DeleteNoLogAsync(repository, checkBook);
                }
            }

            await OnEntityGroupDeleted(checkBookIds);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.CheckBook; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="checkBookViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="checkBook">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CheckBookViewModel checkBookViewModel, CheckBook checkBook)
        {
            checkBook.CheckBookNo = checkBookViewModel.CheckBookNo;
            checkBook.Name = checkBookViewModel.Name;
            checkBook.IssueDate = checkBookViewModel.IssueDate;
            checkBook.StartNo = checkBookViewModel.StartNo;
            checkBook.EndNo = checkBookViewModel.EndNo;
            checkBook.BankName = checkBookViewModel.BankName;
            checkBook.IsArchived = checkBookViewModel.IsArchived;
        }

        #region CheckBook Page
        /// <summary>
        /// به روش آسنکرون، برگه های یک دسته چک مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>برگه های دسته چک مشخص شده با شناسه عددی</returns>
        public async Task<PagedList<CheckBookPageViewModel>> GetPagesAsync(int checkBookId, GridOptions gridOptions = null)
        {
            var query = GetCheckBookPagesQuery(checkBookId);
            query = Repository.ApplyRowFilter(ref query, ViewId.CheckBook);
            var pages = await query
                .Select(page => Mapper.Map<CheckBookPageViewModel>(page))
                .ToListAsync();
            return new PagedList<CheckBookPageViewModel>(pages, gridOptions);
        }
        #endregion
        
        private IQueryable<CheckBookPage> GetCheckBookPagesQuery(int checkBookId)
        {
            ///Need To Change
            var repository = UnitOfWork.GetRepository<CheckBookPage>();
            //var pagesQuery = 
                //    repository
                //.GetEntityQuery()
                //.Where(page => page.checkBook. == checkBookId)
                //.OrderBy(page => page.CheckBookPageID);
            return null;
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
                    "{0} : {1} , {2} : {3} , {4} : {5} , {6} : {7} , {8} : {9}",
                    AppStrings.CheckBookNo, entity.CheckBookNo, AppStrings.CheckBookName, entity.Name,
                    AppStrings.IssueDate, entity.IssueDate, AppStrings.StartNo, entity.StartNo,
                    AppStrings.EndNoCheck, entity.EndNo)
                : null;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }
        private const string DefaultSorting = "c.IssueDate, c.No";
        private readonly ISystemRepository _system;
        private readonly IReportDirectUtility _report;
    }
}
