using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel.Check;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت برگه های چک را پیاده سازی می کند
    /// </summary>
    public class CheckBookPageRepository : EntityLoggingRepository<CheckBookPage, CheckBookPageViewModel>, ICheckBookPageRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public CheckBookPageRepository(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت یک برگه چک را اصلاح می کند
        /// </summary>
        /// <param name="checkBookPageId">شناسه عددی یکی از برگه های چک موجود</param>
        /// <param name="status">وضعیت برگه چک</param>
        /// <param name="descriptionLog">توضیحات جهت ذخیره لاگ</param>
        /// <returns>اطلاعات نمایشی برگه چک ایجاد یا اصلاح شده</returns>
        public async Task<CheckBookPageViewModel> ChangeStateCheckAsync(int checkBookPageId, CheckBookPageState status,string descriptionLog)
        {
            var operationId = status == CheckBookPageState.Cancelled
                ? OperationId.CancelPage
                : OperationId.UndoCancelPage;

            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            var checkBookPage = await repository.GetByIDAsync(checkBookPageId);
            if (checkBookPage != null)
            {
                 checkBookPage.Status = status;
                 repository.Update(checkBookPage);
                 await UnitOfWork.CommitAsync();
                 OnEntityAction(operationId);
                 Log.Description = String.Format(descriptionLog, checkBookPage.SerialNo);
                 await TrySaveLogAsync();
            }
            
            return Mapper.Map<CheckBookPageViewModel>(checkBookPage);
            
        }

        /// <summary>
        /// آخرین تغییرات موجودیت را از مدل نمایشی به سطر اطلاعاتی موجود کپی می کند
        /// </summary>
        /// <param name="bookPageViewModel">مدل نمایشی شامل آخرین تغییرات</param>
        /// <param name="checkBookPage">سطر اطلاعاتی موجود</param>
        protected override void UpdateExisting(CheckBookPageViewModel bookPageViewModel, CheckBookPage checkBookPage)
        {
            checkBookPage.Status = bookPageViewModel.Status;
        }

        /// <summary>
        /// به روش آسنکرون، برگه های چک مشخص شده با شناسه دسته چک را حذف می کند
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک جهت حذف برگه های چک</param>
        public async Task DeleteCheckBookPagesAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            var checkBookPageIds = (await repository
                .GetByCriteriaAsync(c => c.CheckBookId == checkBookId))
                .Select(c => c.Id).ToList();
            foreach (int checkBookPageId in checkBookPageIds)
            {
                var checkBookPage = await repository.GetByIDAsync(checkBookPageId);
                if (checkBookPage != null)
                {
                    await DeleteNoLogAsync(repository, checkBookPage);
                }
            }

            await OnEntityGroupDeleted(checkBookPageIds,OperationId.DeletePages);
        }

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.CheckBook; }
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

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        /// <summary>
        ///  به روش آسنکرون، اطلاعات نمایشی برگه های یک دسته چک جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <returns>برگه های چک مشخص شده با شناسه عددی</returns>
        public async Task<PagedList<CheckBookPageViewModel>> CreatePagesAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            CheckBook checkBookModel = await repository.GetByIDAsync(checkBookId);
            var repositoryPage = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            var seriesAndSerialCheck = ExtractSeriesAndSerialCheck(checkBookModel.StartNo);
            int startPage = Int32.Parse(seriesAndSerialCheck.Serial);
            int endPage = Int32.Parse(ExtractSeriesAndSerialCheck(checkBookModel.EndNo).Serial);
            List<int> checkBookPageIds = new();
            for (int i = startPage; i <= endPage; i++)
            {
                var checkBookPage = new CheckBookPage
                {
                    CheckBookId = checkBookId,
                    SerialNo = seriesAndSerialCheck.Series + i,
                    Status = CheckBookPageState.Blank
                };

                repositoryPage.Insert(checkBookPage);
                await UnitOfWork.CommitAsync();
                checkBookPageIds.Add(checkBookPage.Id);
            }

            await OnEntityGroupInserted(checkBookPageIds, OperationId.CreatePages);
            var query = GetCheckBookPagesQuery(checkBookId);
            var pages = await query
                .Select(page => Mapper.Map<CheckBookPageViewModel>(page))
                .ToListAsync();
            return new PagedList<CheckBookPageViewModel>(pages);
        }

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

        /// <summary>
        /// تفکیک شماره سری و سریال چک
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public (string Series,string Serial) ExtractSeriesAndSerialCheck(string inputString)
        {
            int separateIndex = 0;
            for (int index = inputString.Length - 1; index > 0; index--)
            {
                separateIndex = index;
                if (!Char.IsDigit(inputString[index]))
                {
                    break;
                }
            }

           string serial = inputString.Substring(separateIndex + 1);
           string series = inputString.Substring(0, separateIndex + 1);
            return (series, serial);
        }
        private IQueryable<CheckBookPage> GetCheckBookPagesQuery(int checkBookId)
        {
            var repository = UnitOfWork.GetRepository<CheckBookPage>();
            var pagesQuery = repository
                .GetEntityQuery()
                .Where(checkBookPage => checkBookPage.CheckBook.Id == checkBookId)
                .OrderBy(checkBookPage => checkBookPage.Id);
            return pagesQuery;
        }
        private readonly ISystemRepository _system;
    }
}
