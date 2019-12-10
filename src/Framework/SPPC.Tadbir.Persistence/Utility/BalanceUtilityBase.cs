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
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence.Utility
{
    internal class BalanceUtilityBase : ReportUtilityBase
    {
        protected BalanceUtilityBase(IRepositoryContext context, ISecureRepository repository, IConfigRepository config)
            : base(config, context.Mapper)
        {
            _context = context;
            Repository = repository;
        }

        public async Task<TModel> GetAccountItemAsync<TModel>(int itemId)
            where TModel : class, ITreeEntity
        {
            var repository = UnitOfWork.GetAsyncRepository<TModel>();
            return await repository.GetByIDAsync(itemId);
        }

        protected IAppUnitOfWork UnitOfWork
        {
            get { return _context.UnitOfWork; }
        }

        protected UserContextViewModel UserContext
        {
            get { return _context.UserContext; }
        }

        protected ISecureRepository Repository { get; }

        protected async Task<IEnumerable<TestBalanceModeInfo>> GetLevelBalanceTypesAsync(int viewId)
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = 0;
            for (int index = 0; index < usedLevels.Count; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No,
                    IsDetail = false
                });
            }

            return lookup;
        }

        protected async Task<IEnumerable<TestBalanceModeInfo>> GetChildBalanceTypesAsync(int viewId)
        {
            var lookup = new List<TestBalanceModeInfo>();
            var fullConfig = await Config.GetViewTreeConfigByViewAsync(viewId);
            var usedLevels = fullConfig.Current
                .Levels
                .Where(level => level.IsEnabled && level.IsUsed)
                .ToList();
            int typeId = 0;
            for (int index = 2; index < usedLevels.Count - 1; index++)
            {
                lookup.Add(new TestBalanceModeInfo()
                {
                    Id = typeId++,
                    Name = usedLevels[index].Name,
                    Level = usedLevels[index].No + 1,
                    IsDetail = true
                });
            }

            return lookup;
        }

        protected async Task<decimal> GetItemBalanceAsync(
            DateTime date, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.Date.CompareWith(date) < 0, itemCriteria);
        }

        protected async Task<decimal> GetItemBalanceAsync(
            int number, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.No < number, itemCriteria);
        }

        protected async Task<decimal> GetSpecialVoucherBalanceAsync(
            VoucherType type, Expression<Func<VoucherLine, bool>> itemCriteria)
        {
            return await GetBalanceAsync(
                line => line.Voucher.Type == (short)type, itemCriteria);
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

        private IRepositoryContext _context;
    }
}
