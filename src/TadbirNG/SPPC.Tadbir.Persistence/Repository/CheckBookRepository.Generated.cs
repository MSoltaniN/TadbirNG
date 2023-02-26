using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Utility;
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
        /// به روش آسنکرون، اطلاعات کلیه دسته چک ها را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دسته چک ها تعریف شده</returns>
        public async Task<PagedList<CheckBookViewModel>> GetCheckBooksAsync(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var checkBooks = new List<CheckBookViewModel>();
            if (options.Operation != (int)OperationId.Print)
            {
                //var query = Repository.GetAllQuery<CheckBook>(ViewId.CheckBook);
                //checkBooks = await query
                //    .Select(item => Mapper.Map<CheckBookViewModel>(item))
                //    .ToListAsync();
            }

            await ReadAsync(options);
            return new PagedList<CheckBookViewModel>(checkBooks, options);
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی یکی از دسته چک ها موجود</param>
        /// <returns>دسته چک مشخص شده با شناسه عددی</returns>
        public async Task<CheckBookViewModel> GetCheckBookAsync(int checkBookId)
        {
            CheckBookViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var checkBook = await repository.GetByIDAsync(checkBookId);
            if (checkBook != null)
            {
                item = Mapper.Map<CheckBookViewModel>(checkBook);
            }

            return item;
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
            checkBook.CheckBookID = checkBookViewModel.CheckBookID;
            checkBook.CheckBookNo = checkBookViewModel.CheckBookNo;
            checkBook.Name = checkBookViewModel.Name;
            checkBook.IssueDate = checkBookViewModel.IssueDate;
            checkBook.SartNo = checkBookViewModel.SartNo;
            checkBook.EndNo = checkBookViewModel.EndNo;
            checkBook.BankName = checkBookViewModel.BankName;
            checkBook.AccountID = checkBookViewModel.AccountID;
            checkBook.DetailAccountID = checkBookViewModel.DetailAccountID;
            checkBook.CostCenterID = checkBookViewModel.CostCenterID;
            checkBook.ProjectID = checkBookViewModel.ProjectID;
            checkBook.IsArchived = checkBookViewModel.IsArchived;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CheckBook entity)
        {
            return String.Empty;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private readonly ISystemRepository _system;
    }
}
