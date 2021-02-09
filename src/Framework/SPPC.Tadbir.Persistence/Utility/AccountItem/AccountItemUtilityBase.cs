using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Persistence.Utility
{
    /// <summary>
    /// امکانات مشترک مرتبط با محاسبات گردش و مانده را برای مولفه های مختلف حساب فراهم می کند
    /// </summary>
    public abstract class AccountItemUtilityBase : ReportUtility
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="config">امکان خواندن تنظیمات برنامه را فراهم می کند</param>
        /// <param name="repository">امکان اعمال فیلترهای شعبه و سطری را فراهم می کند</param>
        /// <param name="cache"></param>
        public AccountItemUtilityBase(IRepositoryContext context, IConfigRepository config,
            ISecureRepository repository, ICacheUtility<VoucherLineDetailViewModel> cache)
            : base(context, config, cache)
        {
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، مبلغ مانده در تاریخ داده شده را برای یکی از مولفه های حساب محاسبه می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب</param>
        /// <param name="date">تاریخ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده مولفه حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetBalanceAsync(int itemId, DateTime date)
        {
            decimal balance = 0.0M;
            var account = await GetItemAsync(itemId);
            if (account != null)
            {
                balance = await GetBalanceByDateAsync(date, GetItemCriteria(account));
            }

            return balance;
        }

        /// <summary>
        /// به روش آسنکرون، مبلغ مانده تا پیش از شماره سند داده شده را برای یکی از مولفه های حساب محاسبه می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب</param>
        /// <param name="number">شماره سند مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده مولفه حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetBalanceAsync(int itemId, int number)
        {
            decimal balance = 0.0M;
            var account = await GetItemAsync(itemId);
            if (account != null)
            {
                balance = await GetBalanceByNoAsync(number, GetItemCriteria(account));
            }

            return balance;
        }

        /// <summary>
        /// به روش آسنکرون، مانده مولفه حساب مشخص شده را در اسناد مالی با مأخذ داده شده
        /// محاسبه کرده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="origin">مأخذ مورد نظر برای محاسبه مانده</param>
        /// <returns>مانده حساب مشخص شده به صورت علامتدار : عدد مثبت نمایانگر مانده بدهکار
        /// و عدد منفی نمایانگر مانده بستانکار است</returns>
        public async Task<decimal> GetBalanceAsync(int itemId, VoucherOriginId origin)
        {
            decimal balance = 0.0M;
            var account = await GetItemAsync(itemId);
            if (account != null)
            {
                balance = await GetBalanceByVoucherOriginAsync(origin, GetItemCriteria(account));
            }

            return balance;
        }

        /// <summary>
        /// به روش آسنکرون، مبالغ گردش بدهکار و بستانکار برای مولفه حساب مورد نظر را
        /// در محدوده تاریخی داده شده محاسبه می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">تاریخ ابتدای محدوده تاریخی مورد نظر</param>
        /// <param name="to">تاریخ انتهای محدوده تاریخی مورد نظر</param>
        /// <returns>مبالغ گردش محاسبه شده برای مولفه حساب</returns>
        public async Task<VoucherLineAmountsViewModel> GetTurnoverAsync(int itemId, DateTime from, DateTime to)
        {
            var turnover = default(VoucherLineAmountsViewModel);
            var account = await GetItemAsync(itemId);
            if (account != null)
            {
                turnover = await GetTurnoverAsync(from, to, GetItemCriteria(account));
            }

            return turnover;
        }

        /// <summary>
        /// به روش آسنکرون، مبالغ گردش بدهکار و بستانکار برای مولفه حساب مورد نظر را
        /// در محدوده اسناد داده شده محاسبه می کند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <param name="from">اولین سند در محدوده مورد نظر</param>
        /// <param name="to">آخرین سند در محدوده مورد نظر</param>
        /// <returns>مبالغ گردش محاسبه شده برای مولفه حساب</returns>
        public async Task<VoucherLineAmountsViewModel> GetTurnoverAsync(int itemId, int from, int to)
        {
            var turnover = default(VoucherLineAmountsViewModel);
            var account = await GetItemAsync(itemId);
            if (account != null)
            {
                turnover = await GetTurnoverAsync(from, to, GetItemCriteria(account));
            }

            return turnover;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مولفه حساب داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <returns>اطلاعات خوانده شده برای مولفه حساب</returns>
        public abstract Task<TreeEntity> GetItemAsync(int itemId);

        /// <summary>
        /// عبارت شرطی مورد نیاز برای انجام محاسبات مولفه حساب را برمی گرداند
        /// </summary>
        /// <param name="account">اطلاعات مولفه حساب مورد نظر</param>
        /// <returns>عبارت شرطی</returns>
        public abstract Expression<Func<VoucherLine, bool>> GetItemCriteria(TreeEntity account);

        /// <summary>
        /// به روش آسنکرون، اطلاعات مولفه حساب داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع دات نتی مدل اطلاعاتی مولفه حساب</typeparam>
        /// <param name="itemId">شناسه دیتابیسی مولفه حساب مورد نظر</param>
        /// <returns>اطلاعات خوانده شده برای مولفه حساب</returns>
        protected async Task<TModel> GetAccountItemAsync<TModel>(int itemId)
            where TModel : class, ITreeEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TModel>();
            return await repository.GetByIDAsync(itemId);
        }

        private async Task<decimal> GetBalanceByDateAsync(
            DateTime date, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.Date.CompareWith(date) < 0, itemCriteria);
        }

        private async Task<decimal> GetBalanceByNoAsync(
            int number, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.No < number, itemCriteria);
        }

        private async Task<decimal> GetBalanceByVoucherOriginAsync(
            VoucherOriginId origin, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.VoucherOriginId == (int)origin, itemCriteria);
        }

        private async Task<VoucherLineAmountsViewModel> GetTurnoverAsync(
            DateTime from, DateTime to, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetTurnoverAsync(line => line.Voucher.Date.IsBetween(from, to), itemCriteria);
        }

        private async Task<VoucherLineAmountsViewModel> GetTurnoverAsync(
            int from, int to, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetTurnoverAsync(
                line => line.Voucher.No >= from && line.Voucher.No <= to, itemCriteria);
        }

        private async Task<decimal> GetBalanceAsync(
            Expression<Func<VoucherLine, bool>> lineCriteria,
            Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            var lines = await GetFilteredLinesAsync(lineCriteria, itemCriteria);
            return lines
                .Select(line => line.Debit - line.Credit)
                .Sum();
        }

        private async Task<VoucherLineAmountsViewModel> GetTurnoverAsync(
            Expression<Func<VoucherLine, bool>> lineCriteria,
            Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            var lines = await GetFilteredLinesAsync(lineCriteria, itemCriteria);
            var amounts = lines
                .Select(line => new VoucherLineAmountsViewModel() { Debit = line.Debit, Credit = line.Credit });
            return new VoucherLineAmountsViewModel()
            {
                Debit = amounts.Sum(item => item.Debit),
                Credit = amounts.Sum(item => item.Credit)
            };
        }

        private async Task<List<VoucherLine>> GetFilteredLinesAsync(
            Expression<Func<VoucherLine, bool>> lineCriteria,
            Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await _repository
                .GetAllOperationQuery<VoucherLine>(ViewId.VoucherLine)
                .Where(line => line.Voucher.SubjectType != (short)SubjectType.Draft
                    && line.FiscalPeriodId == UserContext.FiscalPeriodId)
                .Where(lineCriteria)
                .Where(itemCriteria)
                .ToListAsync();
        }

        private readonly ISecureRepository _repository;
    }
}
