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
        /// اطلاعات کلیه جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات جلسات باز برنامه</returns>
        Task<IList<SessionViewModel>> GetSessionsAsync();

        /// <summary>
        /// اطلاعات کلیه جلسات باز یک کاربر مشخص در برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <returns>اطلاعات جلسات باز کاربر در برنامه</returns>
        Task<IList<SessionViewModel>> GetUserSessionsAsync(int userId);

        /// <summary>
        /// جلسه جدیدی در برنامه را ثبت می کند
        /// </summary>
        /// <param name="session">اطلاعات نمایشی جلسه جدید</param>
        Task SaveSessionAsync(SessionViewModel session);

        /// <summary>
        /// جلسه مشخص شده را در برنامه به پایان می رساند
        /// </summary>
        /// <param name="fingerprint">شناسه یکتای تولیدشده برای مرورگر کاربر</param>
        Task DeleteSessionAsync(string fingerprint);

        /// <summary>
        /// تعداد جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>تعداد جلسات باز برنامه</returns>
        Task<int> GetSessionCountAsync();
    }
}
