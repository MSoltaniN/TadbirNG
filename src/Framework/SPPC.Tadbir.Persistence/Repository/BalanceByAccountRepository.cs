using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence.Repository
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات گزارش مانده به تفکیک حساب را پیاده سازی می کند
    /// </summary>
    public class BalanceByAccountRepository : RepositoryBase, IBalanceByAccountRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        public BalanceByAccountRepository(IRepositoryContext context, ISystemRepository system)
            : base(context)
        {
            _system = system;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش مانده به تفکیک حساب را خوانده و برمیگرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns></returns>
        public async Task<BalanceByAccountViewModel> GetBalanceByAccountAsync(BalanceByAccountParameters parameters)
        {
            var result = new BalanceByAccountViewModel();
            switch (parameters.ViewId)
            {
                case ViewName.Account:
                    {
                        result = await ReportByAccount(parameters);
                        break;
                    }

                case ViewName.DetailAccount:
                    {
                        break;
                    }

                default:
                    break;
            }

            return result;
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private IConfigRepository Config
        {
            get { return _system.Config; }
        }

        private async Task<BalanceByAccountViewModel> ReportByAccount(BalanceByAccountParameters parameters)
        {
            var items = new List<BalanceByAccountItemViewModel>();
            var item = new BalanceByAccountItemViewModel();

            var lines = await GetVoucherLinesAsync(parameters);

            item = lines.First();

            item.StartBalance = await GetInitialBalanceAsync(parameters);
            item.Debit = lines.Sum(f => f.Debit);
            item.Credit = lines.Sum(f => f.Credit);

            items.Add(item);

            return new BalanceByAccountViewModel
            {
                Items = items
            };
        }

        private async Task<List<BalanceByAccountItemViewModel>> GetVoucherLinesAsync(BalanceByAccountParameters parameters)
        {
            var query = Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine,
                line => line.Voucher,
                line => line.Account)
                .Where(line => line.AccountId == parameters.AccountId);

            if (parameters.IsByDate)
            {
                query = query
                    .Where(line => line.Voucher.Date.IsBetween(parameters.FromDate.Value, parameters.ToDate.Value))
                    .OrderBy(line => line.Voucher.No);
            }
            else
            {
                query = query
                    .Where(line => line.Voucher.No >= parameters.FromNo && line.Voucher.No <= parameters.ToNo)
                    .OrderBy(line => line.Voucher.No);
            }

            return await query.Select(line => Mapper.Map<BalanceByAccountItemViewModel>(line)).ToListAsync();
        }

        private async Task<decimal> GetInitialBalanceAsync(BalanceByAccountParameters parameters)
        {
            decimal balance = parameters.IsByDate
                ? await GetBalanceAsync(parameters.AccountId.Value, parameters.FromDate.Value)
                : await GetBalanceAsync(parameters.AccountId.Value, parameters.FromNo.Value);

            return balance;
        }

        private async Task<decimal> GetBalanceAsync(int accountId, DateTime date)
        {
            decimal balance = 0.0M;
            var account = await GetAccountAsync(accountId);
            if (account != null)
            {
                balance = await GetItemBalanceAsync(
                    date, line => line.Account.FullCode.StartsWith(account.FullCode));
            }

            return balance;
        }

        private async Task<decimal> GetBalanceAsync(int accountId, int number)
        {
            decimal balance = 0.0M;
            var account = await GetAccountAsync(accountId);
            if (account != null)
            {
                balance = await GetItemBalanceAsync(
                    number, line => line.Account.FullCode.StartsWith(account.FullCode));
            }

            return balance;
        }

        private async Task<Account> GetAccountAsync(int accountId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Account>();
            return await repository.GetByIDAsync(accountId);
        }

        private async Task<decimal> GetItemBalanceAsync(
           DateTime date, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.Date.CompareWith(date) < 0, itemCriteria);
        }

        private async Task<decimal> GetItemBalanceAsync(
           int number, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.No < number, itemCriteria);
        }

        private async Task<decimal> GetBalanceAsync(
            Expression<Func<VoucherLine, bool>> lineCriteria,
            Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await Repository
                .GetAllOperationQuery<VoucherLine>(ViewName.VoucherLine)
                .Where(line => line.FiscalPeriodId == UserContext.FiscalPeriodId)
                .Where(lineCriteria)
                .Where(itemCriteria)
                .Select(line => line.Debit - line.Credit)
                .SumAsync();
        }

        private readonly ISystemRepository _system;
    }
}
