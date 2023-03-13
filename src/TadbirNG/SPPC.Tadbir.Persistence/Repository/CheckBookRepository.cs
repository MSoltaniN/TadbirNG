using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Model.Check;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Check;
using System;
using System.Threading.Tasks;

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

            await ReadAsync(new GridOptions(), GetState(existing));
            return checkbook;
        }
        
        /// <summary>
        /// به روش آسنکرون، دسته چک با شماره مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="checkBookNo">شماره یکی از دسته چک های موجود</param>
        /// <returns>دسته چک مشخص شده با شماره</returns>
        public async Task<CheckBookViewModel> GetCheckBookByNoAsync(string checkBookNo)
        {
            var byNo = default(CheckBookViewModel);
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            var checkBookByNo = await repository.GetFirstByCriteriaAsync(
                c => c.CheckBookNo == checkBookNo);
            if (checkBookByNo != null)
            {
                byNo = Mapper.Map<CheckBookViewModel>(checkBookByNo);
            }

            await ReadAsync(new GridOptions(), GetState(checkBookByNo));
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

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا دسته چک دارای زیرمجموعه هست یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه یکی از دسته چک های موجود</param>
        /// <returns>در حالتی که دسته چک دارای زیرمجموعه باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> HasChildrenAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            int count = await repository.GetCountByCriteriaAsync(ch => ch.CheckBookId == checkBookId);
            return count > 0;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که آیا قسمت والد دسته چک وجود دارد یا نه
        /// </summary>
        /// <param name="checkBookId">شناسه دسته چک موجود</param>
        /// <returns>در حالتی که دسته چک وجود داشته باشد مقدار "درست" و در غیر این صورت
        /// مقدار "نادرست" را برمی گرداند</returns>
        public async Task<bool> HasParentAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBook>();
            int count = await repository.GetCountByCriteriaAsync(ch => ch.Id == checkBookId);
            return count > 0;
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
                .GetCountByCriteriaAsync(c => c.Id != checkBook.Id
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
        public async Task<bool> HasConnectedToCheckAsync(int checkBookId)
        {
            var repository = UnitOfWork.GetAsyncRepository<CheckBookPage>();
            int count = await repository
                .GetCountByCriteriaAsync(c => c.CheckBookId == checkBookId 
                                              && c.CheckId!=null);
            return count > 0;
        }
        private readonly ISystemRepository _system;
    }
}
