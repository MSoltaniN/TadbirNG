using System.Threading.Tasks;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Corporate;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای اعمال محدودیت های ویرایش برنامه را پیاده سازی می کند
    /// </summary>
    public class EditionRepository : RepositoryBase, IEditionRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public EditionRepository(IRepositoryContext context)
            : base(context)
        {
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که با توجه به محدودیت داده شده امکان ایجاد
        /// شرکت جدید هست یا نه؟
        /// </summary>
        /// <param name="maxCompanies">حداکثر تعداد مجاز برای شرکت ها</param>
        /// <returns>اگر با توجه به محدودیت داده شده امکان ایجاد شرکت جدید باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> CanCreteCompanyAsync(int maxCompanies)
        {
            UnitOfWork.UseSystemContext();
            var repository = UnitOfWork.GetAsyncRepository<CompanyDb>();
            int count = await repository.GetCountByCriteriaAsync(company => company.IsActive);
            return count < maxCompanies;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که با توجه به محدودیت داده شده امکان ایجاد
        /// شعبه سازمانی جدید هست یا نه؟
        /// </summary>
        /// <param name="maxBranches">حداکثر تعداد مجاز برای شعبه های سازمانی</param>
        /// <returns>اگر با توجه به محدودیت داده شده امکان ایجاد شعبه جدید باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> CanCreteBranchAsync(int maxBranches)
        {
            UnitOfWork.UseCompanyContext();
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            int count = await repository.GetCountByCriteriaAsync(branch => true);
            return count < maxBranches;
        }
    }
}
