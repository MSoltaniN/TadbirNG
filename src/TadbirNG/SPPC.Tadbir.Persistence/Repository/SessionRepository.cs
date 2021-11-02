using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت جلسات برنامه را پیاده سازی می کند
    /// </summary>
    public class SessionRepository : RepositoryBase, ISessionRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public SessionRepository(IRepositoryContext context)
            : base(context)
        {
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// اطلاعات کلیه جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات جلسات باز برنامه</returns>
        public async Task<IList<SessionViewModel>> GetSessionsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// اطلاعات کلیه جلسات باز یک کاربر مشخص در برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <returns>اطلاعات جلسات باز کاربر در برنامه</returns>
        public async Task<IList<SessionViewModel>> GetUserSessionsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// جلسه جدیدی در برنامه را ثبت می کند
        /// </summary>
        /// <param name="session">اطلاعات نمایشی جلسه جدید</param>
        public async Task SaveSessionAsync(SessionViewModel session)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// جلسه مشخص شده را در برنامه به پایان می رساند
        /// </summary>
        /// <param name="fingerprint">شناسه یکتای تولیدشده برای مرورگر کاربر</param>
        public async Task DeleteSessionAsync(string fingerprint)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// تعداد جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>تعداد جلسات باز برنامه</returns>
        public async Task<int> GetSessionCountAsync()
        {
            throw new NotImplementedException();
        }
    }
}
