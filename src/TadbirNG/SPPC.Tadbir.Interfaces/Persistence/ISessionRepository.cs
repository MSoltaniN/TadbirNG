using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت جلسات برنامه را تعریف می کند
    /// </summary>
    public interface ISessionRepository
    {
        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات جلسات باز برنامه</returns>
        Task<IList<SessionViewModel>> GetSessionsAsync();

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه جلسات باز یک کاربر مشخص در برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <returns>اطلاعات جلسات باز کاربر در برنامه</returns>
        Task<IList<SessionViewModel>> GetUserSessionsAsync(int userId);

        /// <summary>
        /// به روش آسنکرون، جلسه جدیدی در برنامه را ثبت می کند
        /// </summary>
        /// <param name="userAgent">اطلاعات عامل کاربری دریافت شده از درخواست وب</param>
        /// <param name="ipAddress">آدرس آی پی فرستنده درخواست وب</param>
        /// <param name="userId">شناسه دیتابیسی کاربری که وارد برنامه شده است</param>
        Task SaveSessionAsync(string userAgent, string ipAddress, int userId);

        /// <summary>
        /// به روش آسنکرون، وضعیت جلسه کاری با مشخصات داده شده را به حالت فعال نگه می دارد
        /// </summary>
        /// <param name="userAgent">اطلاعات عامل کاربری دریافت شده از درخواست وب</param>
        /// <returns></returns>
        Task UpdateSessionLastActiveAsync(string userAgent);

        /// <summary>
        /// به روش آسنکرون، جلسه مشخص شده را در برنامه به پایان می رساند
        /// </summary>
        /// <param name="userAgent">اطلاعات عامل کاربری دریافت شده از درخواست وب</param>
        Task DeleteSessionAsync(string userAgent);

        /// <summary>
        /// به روش آسنکرون، جلسات مشخص شده با شناسه دیتابیسی را در برنامه به پایان می رساند
        /// </summary>
        /// <param name="sessionIds">مجموعه شناسه های دیتابیسی جلسات مورد نظر</param>
        Task DeleteSessionsAsync(IEnumerable<int> sessionIds);

        /// <summary>
        /// به روش آسنکرون، جلسات منقضی شده در برنامه را به پایان می رساند
        /// </summary>
        Task CleanupSessionsAsync();

        /// <summary>
        /// به روش آسنکرون، تعداد جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>تعداد جلسات باز برنامه</returns>
        Task<int> GetSessionCountAsync();
    }
}
