using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Utility;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای مدیریت ارزها را پیاده سازی میکند
    /// </summary>
    public class CurrencyRepository
        : ActiveStateRepository<Currency, CurrencyViewModel>, ICurrencyRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکان دسترسی به امکانات سیستمی برنامه را فراهم می کند</param>
        /// <param name="access">امکان کار با دیتابیس های برنامه اکسس را فراهم می کند</param>
        public CurrencyRepository(IRepositoryContext context, ISystemRepository system, IAccessRepository access)
            : base(context, system.Logger)
        {
            _system = system;
            _access = access;
        }

        /// <inheritdoc/>
        public async Task<PagedList<CurrencyViewModel>> GetCurrenciesAsync(
            GridOptions gridOptions, int activeState = (int)ActiveState.Active)
        {
            Verify.ArgumentNotNull(gridOptions, nameof(gridOptions));
            var currencies = new List<CurrencyViewModel>();
            if (gridOptions.Operation != (int)OperationId.Print)
            {
                currencies = await Repository
                    .GetAllQuery<Currency>(ViewId.Currency, curr => curr.Branch)
                    .Select(item => Mapper.Map<CurrencyViewModel>(item))
                    .ToListAsync();
                await UpdateInactiveItemsAsync(currencies);
                currencies = FilterAccountsByActiveState(currencies, activeState);
                Array.ForEach(currencies.ToArray(), curr => Localize(curr));
            }

            await ReadAsync(gridOptions);
            return new PagedList<CurrencyViewModel>(currencies, gridOptions);
        }

        /// <inheritdoc/>
        public async Task<CurrencyViewModel> GetCurrencyAsync(int currencyId)
        {
            CurrencyViewModel item = null;
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currency = await repository.GetByIDAsync(currencyId, curr => curr.Branch);
            if (currency != null)
            {
                item = Mapper.Map<CurrencyViewModel>(currency);
                var isDeactivated = await IsDeactivatedAsync(item.Id);
                item.State = isDeactivated
                    ? AppStrings.Inactive
                    : AppStrings.Active;
                Localize(item);
            }

            return item;
        }

        /// <inheritdoc/>
        public CurrencyViewModel GetCurrencyByName(string localDbPath, string nameKey)
        {
            var currencies = GetLocalCurrencyDatabase(localDbPath);
            var currency = currencies
                .Where(curr => curr.Currency.NameKey == nameKey)
                .Select(curr => Mapper.Map<CurrencyViewModel>(curr))
                .FirstOrDefault();
            Localize(currency);
            return currency;
        }

        /// <inheritdoc/>
        public IList<KeyValue> GetCurrencyNamesLookup(string localDbPath)
        {
            var currencies = GetLocalCurrencyDatabase(localDbPath);
            var names = currencies
                .Select(info => info.Currency.NameKey)
                .Distinct()
                .Select(name => new KeyValue(name, name))
                .ToList();
            return names;
        }

        /// <inheritdoc/>
        public async Task<CurrencyInfoViewModel> GetDefaultCurrencyAsync(int accountId, int faccountId)
        {
            var currency = default(CurrencyInfoViewModel);
            var detailRepository = UnitOfWork.GetAsyncRepository<DetailAccount>();
            var detailAccount = await detailRepository.GetByIDAsync(faccountId, facc => facc.Currency);
            if (detailAccount != null && detailAccount.CurrencyId.HasValue)
            {
                currency = Mapper.Map<CurrencyInfoViewModel>(detailAccount.Currency);
            }
            else
            {
                var accountCurrencyRepository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
                var accCurrency = await accountCurrencyRepository.GetSingleByCriteriaAsync(
                    accCurr => accCurr.AccountId == accountId && accCurr.BranchId == UserContext.BranchId,
                    accCurr => accCurr.Currency);
                if (accCurrency != null)
                {
                    currency = Mapper.Map<CurrencyInfoViewModel>(accCurrency.Currency);
                }
            }

            return currency;
        }

        /// <inheritdoc/>
        public async Task<IList<TaxCurrencyViewModel>> GetTaxCurrenciesAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<TaxCurrency>();
            return await repository
                .GetEntityQuery()
                .OrderBy(curr => curr.Name)
                .Select(curr => Mapper.Map<TaxCurrencyViewModel>(curr))
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateTaxCurrenciesAsync(string mdbPath)
        {
            var repository = UnitOfWork.GetAsyncRepository<TaxCurrency>();
            repository.ExecuteCommand("TRUNCATE TABLE [Finance].[TaxCurrency]");
            var taxItems = await _access.GetAllAsync<TaxCurrencyViewModel>(mdbPath, "Arz");
            foreach (var taxItem in taxItems)
            {
                repository.Insert(Mapper.Map<TaxCurrency>(taxItem));
            }

            await UnitOfWork.CommitAsync();
        }

        /// <inheritdoc/>
        public async Task<CurrencyViewModel> SaveCurrencyAsync(CurrencyViewModel currency)
        {
            Verify.ArgumentNotNull(currency, nameof(currency));
            Currency currencyModel = default(Currency);
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            if (currency.Id == 0)
            {
                currencyModel = Mapper.Map<Currency>(currency);
                SetBaseEntityInfo(currencyModel);
                await InsertAsync(repository, currencyModel);
            }
            else
            {
                currencyModel = await repository.GetByIDAsync(currency.Id);
                if (currencyModel != null)
                {
                    SetBaseEntityInfo(currencyModel);
                    await UpdateAsync(repository, currencyModel, currency);
                }
            }

            return Mapper.Map<CurrencyViewModel>(currencyModel);
        }

        /// <inheritdoc/>
        public async Task DeleteCurrencyAsync(int currencyId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currency = await repository.GetByIDWithTrackingAsync(currencyId, curr => curr.Rates);
            if (currency != null)
            {
                currency.Rates.Clear();
                await OnDeleteItemAsync(currency.Id);
                await DeleteAsync(repository, currency);
            }
        }

        /// <inheritdoc/>
        public async Task DeleteCurrenciesAsync(IEnumerable<int> items)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            foreach (int item in items)
            {
                var currency = await repository.GetByIDWithTrackingAsync(item, curr => curr.Rates);
                if (currency != null)
                {
                    currency.Rates.Clear();
                    await OnDeleteItemAsync(currency.Id);
                    await DeleteNoLogAsync(repository, currency);
                }
            }

            await OnEntityGroupDeleted(items);
        }

        /// <inheritdoc/>
        public async Task<bool> CanDeleteCurrencyAsync(int currencyId)
        {
            bool isUsed = await IsUsedInAccounts(currencyId);
            if (!isUsed)
            {
                isUsed = await IsUsedInVoucherLines(currencyId);
            }

            return !isUsed;
        }

        /// <inheritdoc/>
        public async Task<bool> IsDuplicateCurrencyAsync(string code, int currencyId = 0)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var existing = await repository.GetFirstByCriteriaAsync(
                curr => curr.Code == code && curr.Id != currencyId);
            return existing != null;
        }

        /// <inheritdoc/>
        public async Task<CurrencyViewModel> InsertDefaultCurrencyAsync(CurrencyViewModel currency)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            var currencyViewModel = default(CurrencyViewModel);
            var existingDefaultCurrency = await repository.GetFirstByCriteriaAsync(
                curr => curr.IsDefaultCurrency);

            if (existingDefaultCurrency != null)
            {
                if (existingDefaultCurrency.Code != currency.Code)
                {
                    currencyViewModel = Mapper.Map<CurrencyViewModel>(existingDefaultCurrency);
                    currencyViewModel.IsDefaultCurrency = false;

                    await UpdateAsync(repository, existingDefaultCurrency, currencyViewModel);

                    currencyViewModel = await SaveDefaultCurrencyAsync(currency);
                }
                else
                {
                    currencyViewModel = Mapper.Map<CurrencyViewModel>(existingDefaultCurrency);
                }
            }
            else
            {
                currencyViewModel = await SaveDefaultCurrencyAsync(currency);
            }

            return currencyViewModel;
        }

        internal override int? EntityType
        {
            get { return (int)EntityTypeId.Currency; }
        }

        /// <inheritdoc/>
        protected override void UpdateExisting(CurrencyViewModel currencyViewModel, Currency currency)
        {
            currency.BranchScope = currencyViewModel.BranchScope;
            currency.Name = currencyViewModel.Name;
            currency.Code = currencyViewModel.Code;
            currency.TaxCode = currencyViewModel.TaxCode;
            currency.MinorUnit = currencyViewModel.MinorUnit;
            currency.DecimalCount = currencyViewModel.DecimalCount;
            currency.Description = currencyViewModel.Description;
            currency.IsDefaultCurrency = currencyViewModel.IsDefaultCurrency;
        }

        /// <inheritdoc/>
        protected override string GetState(Currency entity)
        {
            return entity == null
                ? String.Empty
                : $"{AppStrings.Name} : {entity.Name} , " +
                  $"{AppStrings.Code} : {entity.Code} , " +
                  $"{AppStrings.MinorUnit} : {entity.MinorUnit} , " +
                  $"{AppStrings.DecimalCount} : {entity.DecimalCount} , " +
                  $"{AppStrings.Description} : {entity.Description}";
        }

        private ISecureRepository Repository
        {
            get { return _system.Repository; }
        }

        private List<CurrencyInfo> GetLocalCurrencyDatabase(string localDbPath)
        {
            return JsonHelper.To<List<CurrencyInfo>>(File.ReadAllText(localDbPath));
        }

        private async Task<bool> IsUsedInAccounts(int currencyId)
        {
            var accountCurrencyRepository = UnitOfWork.GetAsyncRepository<AccountCurrency>();
            int usageCount = await accountCurrencyRepository
                .GetEntityQuery()
                .Where(accCurr => accCurr.CurrencyId == currencyId)
                .CountAsync();
            return (usageCount != 0);
        }

        private async Task<bool> IsUsedInVoucherLines(int currencyId)
        {
            var lineRepository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            int usageCount = await lineRepository
                .GetEntityQuery()
                .Where(line => line.CurrencyId == currencyId)
                .CountAsync();
            return (usageCount != 0);
        }

        private async Task<CurrencyViewModel> SaveDefaultCurrencyAsync(CurrencyViewModel currency)
        {
            var repository = UnitOfWork.GetAsyncRepository<Currency>();
            Currency currencyModel = default(Currency);
            var existingCurrency = await repository.GetFirstByCriteriaAsync(
                          curr => curr.Code == currency.Code);

            if (existingCurrency != null)
            {
                await UpdateAsync(repository, existingCurrency, currency);
            }
            else
            {
                currencyModel = Mapper.Map<Currency>(currency);
                await InsertAsync(repository, currencyModel);
            }

            return Mapper.Map<CurrencyViewModel>(currencyModel);
        }

        private void Localize(CurrencyViewModel currency)
        {
            currency.Name = Context.Localize(currency.Name);
            currency.MinorUnit = Context.Localize(currency.MinorUnit);
            currency.State = Context.Localize(currency.State);
        }

        private readonly ISystemRepository _system;
        private readonly IAccessRepository _access;
    }
}
