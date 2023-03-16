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
    /// عملیات مورد نیاز برای مدیریت دفتر دسته های چک را پیاده سازی می کند
    /// </summary>
    public class CheckBookReportRepository : EntityLoggingRepository<CheckBook, CheckBookReportViewModel>, ICheckBookReportRepository
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

        internal override int? EntityType
        {
            get { return (int?)EntityTypeId.CheckBookReport; }
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
    }
}
