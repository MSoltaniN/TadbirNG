using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای اعمال محدودیت های ویرایش برنامه را تعریف می کند
    /// </summary>
    public interface IEditionRepository
    {
        /// <summary>
        /// به روش آسنکرون، مشخص می کند که با توجه به محدودیت داده شده امکان ایجاد
        /// شرکت جدید هست یا نه؟
        /// </summary>
        /// <param name="maxCompanies">حداکثر تعداد مجاز برای شرکت ها</param>
        /// <returns>اگر با توجه به محدودیت داده شده امکان ایجاد شرکت جدید باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> CanCreteCompanyAsync(int maxCompanies);

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که با توجه به محدودیت داده شده امکان ایجاد
        /// شعبه سازمانی جدید هست یا نه؟
        /// </summary>
        /// <param name="maxBranches">حداکثر تعداد مجاز برای شعبه های سازمانی</param>
        /// <returns>اگر با توجه به محدودیت داده شده امکان ایجاد شعبه جدید باشد مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        Task<bool> CanCreteBranchAsync(int maxBranches);
    }
}
