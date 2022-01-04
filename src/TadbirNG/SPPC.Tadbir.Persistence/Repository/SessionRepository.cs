﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Persistence;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Security;
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
        /// <param name="sessionProvider">امکان تشخیص اطلاعات یک جلسه کاری در برنامه را فراهم می کند</param>
        public SessionRepository(IRepositoryContext context, ISessionProvider sessionProvider)
            : base(context)
        {
            _sessionProvider = sessionProvider;
            UnitOfWork.UseSystemContext();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات جلسات باز برنامه</returns>
        public async Task<IList<SessionViewModel>> GetSessionsAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Session>();
            var now = DateTime.UtcNow;
            var sessions = await repository.GetByCriteriaAsync(
                session => now - session.SinceUtc < Constants.SessionTimeout);
            return sessions
                .Select(session => Mapper.Map<SessionViewModel>(session))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات کلیه جلسات باز یک کاربر مشخص در برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر مورد نظر</param>
        /// <returns>اطلاعات جلسات باز کاربر در برنامه</returns>
        public async Task<IList<SessionViewModel>> GetUserSessionsAsync(int userId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Session>();
            var now = DateTime.UtcNow;
            var sessions = await repository.GetByCriteriaAsync(session =>
                now - session.SinceUtc < Constants.SessionTimeout &&
                session.User.Id == userId);
            return sessions
                .Select(session => Mapper.Map<SessionViewModel>(session))
                .ToList();
        }

        /// <summary>
        /// به روش آسنکرون، جلسه جدیدی در برنامه را ثبت می کند
        /// </summary>
        /// <param name="userAgent">اطلاعات عامل کاربری دریافت شده از درخواست وب</param>
        /// <param name="ipAddress">آدرس آی پی فرستنده درخواست وب</param>
        public async Task SaveSessionAsync(string userAgent, string ipAddress)
        {
            var repository = UnitOfWork.GetAsyncRepository<Session>();
            var session = _sessionProvider.GetSession(userAgent, ipAddress);
            session.UserId = UserContext.Id;
            repository.Insert(Mapper.Map<Session>(session));
            await UnitOfWork.CommitAsync();
        }

        /// <summary>
        /// به روش آسنکرون، وضعیت جلسه کاری با مشخصات داده شده را به حالت فعال نگه می دارد
        /// </summary>
        /// <param name="userAgent">اطلاعات عامل کاربری دریافت شده از درخواست وب</param>
        public async Task UpdateSessionLastActiveAsync(string userAgent)
        {
            var repository = UnitOfWork.GetAsyncRepository<Session>();
            var now = DateTime.UtcNow;
            var existing = await GetActiveSessionAsync(userAgent);
            if (existing != null)
            {
                existing.LastActivityUtc = now;
                repository.Update(existing);
                await UnitOfWork.CommitAsync();
            }
        }

        /// <summary>
        /// به روش آسنکرون، جلسه مشخص شده را در برنامه به پایان می رساند
        /// </summary>
        /// <param name="userAgent">اطلاعات عامل کاربری دریافت شده از درخواست وب</param>
        public async Task DeleteSessionAsync(string userAgent)
        {
            var repository = UnitOfWork.GetAsyncRepository<Session>();
            var now = DateTime.UtcNow;
            var existing = await GetActiveSessionAsync(userAgent);
            if (existing != null)
            {
                repository.Delete(existing);
                await UnitOfWork.CommitAsync();
            }

            await CleanupSessions(repository, now);
        }

        /// <summary>
        /// به روش آسنکرون، تعداد جلسات باز برنامه را خوانده و برمی گرداند
        /// </summary>
        /// <returns>تعداد جلسات باز برنامه</returns>
        public async Task<int> GetSessionCountAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Session>();
            var now = DateTime.UtcNow;
            return await repository.GetCountByCriteriaAsync(
                session => now - session.SinceUtc < Constants.SessionTimeout);
        }

        private async Task<Session> GetActiveSessionAsync(string userAgent)
        {
            var repository = UnitOfWork.GetAsyncRepository<Session>();
            var now = DateTime.UtcNow;
            var fingerprint = GetFingerprint(userAgent);
            return await repository.GetFirstByCriteriaAsync(session =>
                now - session.SinceUtc < Constants.SessionTimeout &&
                session.Fingerprint == fingerprint);
        }

        private string GetFingerprint(string userAgent)
        {
            var session = _sessionProvider.GetSession(userAgent, null);
            return session.Fingerprint;
        }

        private async Task CleanupSessions(IAsyncRepository<Session> repository, DateTime now)
        {
            var expired = await repository.GetByCriteriaAsync(session =>
                now - session.SinceUtc >= Constants.SessionTimeout);
            foreach (var item in expired)
            {
                repository.Delete(item);
            }

            await UnitOfWork.CommitAsync();
        }

        private readonly ISessionProvider _sessionProvider;
    }
}
