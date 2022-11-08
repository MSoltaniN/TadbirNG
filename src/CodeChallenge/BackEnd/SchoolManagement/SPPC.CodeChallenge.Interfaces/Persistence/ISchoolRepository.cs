using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.CodeChallenge.ViewModel.Core;

namespace SPPC.CodeChallenge.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت مدارس را تعریف می کند
    /// </summary>
    public interface ISchoolRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه مدارس را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه ای از مدارس تعریف شده</returns>
        Task<IList<SchoolViewModel>> GetSchoolsAsync();

        /// <summary>
        /// به روش آسنکرون، مدرسه با شناسه عددی مشخص شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="schoolId">شناسه عددی یکی از مدارس موجود</param>
        /// <returns>مدرسه مشخص شده با شناسه عددی</returns>
        Task<SchoolViewModel> GetSchoolAsync(int schoolId);

        /// <summary>
        /// به روش آسنکرون، اطلاعات یک مدرسه را ایجاد یا اصلاح می کند
        /// </summary>
        /// <param name="school">مدرسه مورد نظر برای ایجاد یا اصلاح</param>
        /// <returns>اطلاعات نمایشی مدرسه ایجاد یا اصلاح شده</returns>
        Task<SchoolViewModel> SaveSchoolAsync(SchoolViewModel school);

        /// <summary>
        /// به روش آسنکرون، مدرسه مشخص شده با شناسه عددی را حذف می کند
        /// </summary>
        /// <param name="schoolId">شناسه عددی مدرسه مورد نظر برای حذف</param>
        Task DeleteSchoolAsync(int schoolId);
    }
}
