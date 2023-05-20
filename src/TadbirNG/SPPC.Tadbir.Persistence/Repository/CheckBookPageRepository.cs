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
    /// عملیات مورد نیاز برای مدیریت برگه های چک را پیاده سازی می کند
    /// </summary>
    public class CheckBookPageRepository :
        EntityLoggingRepository<CheckBookPage, CheckBookPageViewModel>, ICheckBookPageRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CheckBookPageRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت یک برگه چک را اصلاح می کند
        /// </summary>
        /// <param name="checkBookPageId">شناسه عددی یکی از برگه های چک موجود</param>
        /// <param name="status">وضعیت برگه چک</param>
        /// <param name="logDescription">توضیحات جهت ذخیره لاگ</param>
        /// <returns>اطلاعات نمایشی برگه چک ایجاد یا اصلاح شده</returns>
        public async Task<CheckBookPageViewModel> ChangeCheckStateAsync(
            int checkBookPageId, CheckBookPageState status, string logDescription)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            var checkBookPage = await repository.GetByIDAsync(checkBookPageId);
            if (checkBookPage != null)
            {
                var operationId = status == CheckBookPageState.Canceled
                    ? OperationId.CancelPage
                    : OperationId.UndoCancelPage;
                checkBookPage.Status = status;
                repository.Update(checkBookPage);
                await UnitOfWork.CommitAsync();
                OnEntityAction(operationId);
                Log.Description = String.Format(logDescription, checkBookPage.SerialNo);
                await TrySaveLogAsync();
            }
            
            return Mapper.Map<CheckBookPageViewModel>(checkBookPage);
            
        }

        /// <summary>
        /// به روش آسنکرون، برگه های چک مشخص شده با شناسه دسته چک را حذف می کند
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک جهت حذف برگه های چک</param>
        public async Task DeletePagesAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            var checkBookPageIds = await repository
                .GetEntityQuery()
                .Where(c => c.CheckBookId == checkBookId)
                .Select(c => c.Id)
                .ToListAsync();
            foreach (int checkBookPageId in checkBookPageIds)
            {
                var checkBookPage = await repository.GetByIDAsync(checkBookPageId);
                if (checkBookPage != null)
                {
                    await DeleteNoLogAsync(repository, checkBookPage);
                }
            }

            await OnEntityGroupDeleted(checkBookPageIds, OperationId.DeletePages);
        }

        /// <summary>
        ///  به روش آسنکرون، اطلاعات نمایشی برگه های یک دسته چک جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <returns>برگه های چک مشخص شده با شناسه عددی</returns>
        public async Task<PagedList<CheckBookPageViewModel>> CreatePagesAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            CheckBook checkBook = await repository.GetByIDAsync(checkBookId);
            var pages = new CheckBookPages(
                checkBook.StartNo, checkBook.EndNo, checkBook.SeriesNo, checkBook.SayyadStartNo);
            var pageRepository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            var checkBookPageIds = new List<int>();
            for (int i = 0; i < pages.Serials.Count(); i++)
            {
                var checkBookPage = new CheckBookPage
                {
                    CheckBookId = checkBookId,
                    SerialNo = pages.Serials.ElementAt(i),
                    SayyadNo = pages.SayyadSerials.ElementAt(i),
                    Status = CheckBookPageState.Blank
                };

                pageRepository.Insert(checkBookPage);
                await UnitOfWork.CommitAsync();
                checkBookPageIds.Add(checkBookPage.Id);
            }

            await OnEntityGroupInserted(checkBookPageIds, OperationId.CreatePages);
            return await GetPagesAsync(checkBookId, null);
        }

        /// <summary>
        /// به روش آسنکرون، برگه های یک دسته چک مشخص شده با شناسه عددی را از محل ذخیره خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <param name="gridOptions">گزینه های مورد نظر برای نمایش رکوردها در نمای لیستی</param>
        /// <returns>برگه های دسته چک مشخص شده با شناسه عددی</returns>
        public async Task<PagedList<CheckBookPageViewModel>> GetPagesAsync(
            int checkBookId, GridOptions gridOptions)
        {
            var query = GetCheckBookPagesQuery(checkBookId);
            var pages = await query
                .Select(page => Mapper.Map<CheckBookPageViewModel>(page))
                .ToArrayAsync();
            Array.ForEach(pages, page => page.StatusName = Context.Localize(GetStatusName(page.Status)));
            return new PagedList<CheckBookPageViewModel>(pages, gridOptions);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.CheckBook; }
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="pageViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="page">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CheckBookPageViewModel pageViewModel, CheckBookPage page)
        {
            page.Status = pageViewModel.Status;
        }

        /// <summary>
        /// اطلاعات خلاصه سطر اطلاعاتی داده شده را به صورت یک رشته متنی برمی گرداند
        /// </summary>
        /// <param name="entity">یکی از سطرهای اطلاعاتی موجود</param>
        /// <returns>اطلاعات خلاصه سطر اطلاعاتی داده شده به صورت رشته متنی</returns>
        protected override string GetState(CheckBookPage entity)
        {
            return (entity != null)
                ? String.Format(
                    "{0} : {1}",
                    AppStrings.CheckBookNo, entity.SerialNo)
                : null;
        }

        private static string GetStatusName(CheckBookPageState? status)
        {
            string statusName;
            switch (status.Value)
            {
                case CheckBookPageState.Blank:
                    statusName = AppStrings.Blank;
                    break;
                case CheckBookPageState.Used:
                    statusName = AppStrings.Used;
                    break;
                case CheckBookPageState.Canceled:
                    statusName = AppStrings.Canceled;
                    break;
                default:
                    statusName = String.Empty;
                    break;
            }

            return statusName;
        }

        private IQueryable<CheckBookPage> GetCheckBookPagesQuery(int checkBookId)
        {
            var repository = UnitOfWork.GetRepository<CheckBookPage>();
            var pagesQuery = repository
                .GetEntityQuery()
                .Where(checkBookPage => checkBookPage.CheckBookId == checkBookId)
                .OrderBy(checkBookPage => checkBookPage.Id);
            return pagesQuery;
        }
    }
}
