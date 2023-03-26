using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت دفتر دسته های چک را پیاده سازی می کند
    /// </summary>
    public class CheckBookReportRepository : LoggingRepositoryBase, ICheckBookReportRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CheckBookReportRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه دفتر دسته های چک را خوانده و برمی گرداند
        /// </summary>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>مجموعه ای از دسته های چک تعریف شده</returns>
        public async Task<PagedList<CheckBookReportViewModel>> GetCheckBooksReportAsync(GridOptions gridOptions = null)
        {
            var options = gridOptions ?? new GridOptions();
            var checkBooks = new List<CheckBookReportViewModel>();
            if (options.Operation != (int)OperationId.Print)
            {
                var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
                checkBooks = await repository
                    .GetEntityQuery()
                    .Include(cb => cb.Account)
                    .Include(cb => cb.DetailAccount)
                    .Include(cb => cb.CostCenter)
                    .Include(cb => cb.Project)
                    .Where(cb => cb.BranchId == UserContext.BranchId)
                    .Select(item => Mapper.Map<CheckBookReportViewModel>(item))
                    .ToListAsync();
            }

            await ReadAsync(options);
            return new PagedList<CheckBookReportViewModel>(checkBooks, options);
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت بایگانی دسته چک های مشخص شده با شناسه عددی را تغییر می دهد
        /// </summary>
        /// <param name="checkBookIds">مجموعه ای از شناسه های عددی دسته چک های مورد نظر 
        /// برای تغییر وضعیت بایگانی</param>
        /// <param name="archiveValue">مقدار مورد نظر برای تغییر وضعیت بایگانی دسته چک ها</param>
        public async Task EditArchiveCheckBooksAsync(IList<int> checkBookIds, bool archiveValue)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            foreach (int checkBookId in checkBookIds)
            {
                var checkBook = await repository.GetByIDAsync(checkBookId);
                if(checkBook != null)
                {
                    checkBook.IsArchived = archiveValue;
                    repository.Update(checkBook);
                }
            }

            await UnitOfWork.CommitAsync();
            if(archiveValue == true)
            {
                string description = Context.Localize(String.Format(
                    "{0} : {1}", AppStrings.ArchivedItemCount, checkBookIds.Count));
                await OnSourceActionAsync(OperationId.Archive, description);
            }
            else
            {
                string description = Context.Localize(String.Format(
                   "{0} : {1}", AppStrings.UndoArchivedItemCount, checkBookIds.Count));
                await OnSourceActionAsync(OperationId.UndoArchiv, description);
            }
        }

        /// <summary>
        /// به روش آسنکرون، دسته چک با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه عددی یکی از دسته چک های موجود</param>
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
        internal override OperationSourceId OperationSource
        {
            get { return OperationSourceId.CheckBook; }
        }
    }
}
