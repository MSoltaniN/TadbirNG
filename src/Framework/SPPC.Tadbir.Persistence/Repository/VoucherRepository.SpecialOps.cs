using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Persistence
{
    public partial class VoucherRepository
    {
        /// <summary>
        /// به روش آسنکرون، سند افتتاحیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند افتتاحیه در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetOpeningVoucherAsync(bool isQuery = false)
        {
            var openingVoucher = await GetCurrentSpecialVoucherAsync(VoucherType.OpeningVoucher);
            if (openingVoucher == null)
            {
                if (isQuery)
                {
                    return null;
                }
                else
                {
                    openingVoucher = await IssueOpeningVoucherAsync();
                }
            }

            return Mapper.Map<VoucherViewModel>(openingVoucher);
        }

        /// <summary>
        /// به روش آسنکرون مشخص می کند که برای دوره مالی قبل سند اختتامیه صادر شده یا نه
        /// </summary>
        /// <returns>در صورتی که دوره مالی قبل سند اختتامیه داشته باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> HasPreviousClosingVoucherAsync()
        {
            int previousId = await GetPreviousFiscalPeriodIdAsync();
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            int count = await repository.GetCountByCriteriaAsync(
                v => v.FiscalPeriodId == previousId &&
                v.Type == (short)VoucherType.ClosingVoucher);
            return count == 1;
        }

        /// <summary>
        /// به روش آسنکرون، سند بستن حساب های موقت مربوط به دوره مالی جاری را
        /// برای سیستم ثبت دائمی خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetClosingTempAccountsVoucherAsync()
        {
            var closingVoucher = await GetCurrentSpecialVoucherAsync(VoucherType.ClosingTempAccounts);
            if (closingVoucher == null)
            {
                var balanceItems = new List<AccountBalanceViewModel>();
                closingVoucher = await IssueClosingTempAccountsVoucherAsync(balanceItems);
            }

            return Mapper.Map<VoucherViewModel>(closingVoucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند بستن حساب های موقت مربوط به دوره مالی جاری را
        /// برای سیستم ثبت ادواری خوانده و برمی گرداند
        /// </summary>
        /// <param name="balanceItems">مجموعه مقادیر مانده موجودی انبار - برای سیستم ثبت ادواری</param>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetPeriodicClosingTempAccountsVoucherAsync(
            IList<AccountBalanceViewModel> balanceItems)
        {
            var closingVoucher = await GetCurrentSpecialVoucherAsync(VoucherType.ClosingTempAccounts);
            if (closingVoucher == null)
            {
                if (balanceItems.Count == 0)
                {
                    return null;
                }
                else
                {
                    closingVoucher = await IssueClosingTempAccountsVoucherAsync(balanceItems);
                }
            }

            return Mapper.Map<VoucherViewModel>(closingVoucher);
        }

        /// <summary>
        /// به روش آسنکرون، سند اختتامیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند اختتامیه در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetClosingVoucherAsync()
        {
            var closingVoucher = await GetCurrentSpecialVoucherAsync(VoucherType.ClosingVoucher);
            if (closingVoucher == null)
            {
                closingVoucher = await IssueClosingVoucherAsync();
            }

            return Mapper.Map<VoucherViewModel>(closingVoucher);
        }

        #region Common Operations

        private static void SetRowNumbers(IEnumerable<VoucherLine> lines)
        {
            int rowNo = 1;
            foreach (var line in lines)
            {
                line.RowNo = rowNo++;
            }
        }

        private static void SetReverseAmounts(VoucherLine line, decimal balance)
        {
            line.Debit = balance < 0 ? Math.Abs(balance) : 0.0M;
            line.Credit = balance > 0 ? balance : 0.0M;
        }

        private static AccountBalanceViewModel GetBalanceItem(int accountId, int branchId, decimal balance)
        {
            return new AccountBalanceViewModel()
            {
                AccountId = accountId,
                BranchId = branchId,
                DebitBalance = balance > 0 ? balance : 0.0M,
                CreditBalance = balance < 0 ? Math.Abs(balance) : 0.0M
            };
        }

        private static IEnumerable<IGrouping<int?, VoucherLine>> GetByCurrencyLineGroups(IEnumerable<VoucherLine> lines)
        {
            foreach (var byDetail in lines.GroupBy(line => line.DetailId))
            {
                foreach (var byCostCenter in byDetail.GroupBy(line => line.CostCenterId))
                {
                    foreach (var byProject in byCostCenter.GroupBy(line => line.ProjectId))
                    {
                        foreach (var byCurrency in byProject.GroupBy(line => line.CurrencyId))
                        {
                            yield return byCurrency;
                        }
                    }
                }
            }
        }

        private async Task<Voucher> GetCurrentSpecialVoucherAsync(VoucherType voucherType)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var openingVoucher = await repository.GetSingleByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId &&
                v.Type == (short)voucherType);
            return openingVoucher;
        }

        private async Task<Account> GetBranchClosingAccountAsync(int branchId)
        {
            var accounts = await GetCollectionItemsAsync((int)AccountCollectionId.ClosingAccount, branchId);
            return accounts.SingleOrDefault();
        }

        private async Task<IList<Account>> GetBranchAssetAccountsAsync(int branchId)
        {
            var accounts = new List<Account>();
            accounts.AddRange(await GetCollectionItemsAsync((int)AccountCollectionId.LiquidAssets, branchId));
            accounts.AddRange(await GetCollectionItemsAsync((int)AccountCollectionId.NonLiquidAssets, branchId));
            return accounts;
        }

        private async Task<IList<Account>> GetBranchCapitalLiabilityAccountsAsync(int branchId)
        {
            var accounts = new List<Account>();
            accounts.AddRange(await GetCollectionItemsAsync((int)AccountCollectionId.LiquidLiabilities, branchId));
            accounts.AddRange(await GetCollectionItemsAsync((int)AccountCollectionId.NonLiquidLiabilities, branchId));
            accounts.AddRange(await GetCollectionItemsAsync((int)AccountCollectionId.OwnerEquities, branchId));
            return accounts;
        }

        private async Task<IList<Account>> GetCollectionItemsAsync(int collection, int branchId)
        {
            var repository = UnitOfWork.GetAsyncRepository<AccountCollectionAccount>();
            var collectionAccounts = await repository
                .GetEntityQuery()
                .Include(aca => aca.Account)
                .Where(aca => aca.FiscalPeriodId <= UserContext.FiscalPeriodId &&
                    aca.BranchId == branchId &&
                    aca.CollectionId == collection)
                .Select(aca => aca.Account)
                .ToListAsync();

            var accountRepository = UnitOfWork.GetAsyncRepository<Account>();
            var leafAccounts = await accountRepository
                .GetEntityQuery(acc => acc.AccountDetailAccounts, acc => acc.AccountCostCenters,
                    acc => acc.AccountProjects)
                .Where(acc => acc.Children.Count == 0 &&
                    collectionAccounts.Any(item => acc.FullCode.StartsWith(item.FullCode)))
                .ToListAsync();
            return leafAccounts;
        }

        private IList<FullAccountViewModel> GetFullAccounts(Account account)
        {
            var fullAccounts = new List<FullAccountViewModel>();
            var accountBrief = new AccountItemBriefViewModel() { Id = account.Id };
            if (account.AccountDetailAccounts.Count > 0)
            {
                foreach (var detailAccountId in account.AccountDetailAccounts.Select(ada => ada.DetailId))
                {
                    var detailAccountBrief = new AccountItemBriefViewModel() { Id = detailAccountId };
                    if (account.AccountCostCenters.Count > 0)
                    {
                        foreach (var costCenterId in account.AccountCostCenters.Select(ac => ac.CostCenterId))
                        {
                            var costCenterBrief = new AccountItemBriefViewModel() { Id = costCenterId };
                            if (account.AccountProjects.Count > 0)
                            {
                                foreach (var projectId in account.AccountProjects.Select(ap => ap.ProjectId))
                                {
                                    fullAccounts.Add(new FullAccountViewModel()
                                    {
                                        Account = accountBrief,
                                        DetailAccount = detailAccountBrief,
                                        CostCenter = costCenterBrief,
                                        Project = new AccountItemBriefViewModel() { Id = projectId }
                                    });
                                }
                            }
                            else
                            {
                                fullAccounts.Add(new FullAccountViewModel()
                                {
                                    Account = accountBrief,
                                    DetailAccount = detailAccountBrief,
                                    CostCenter = costCenterBrief,
                                });
                            }
                        }
                    }
                    else if (account.AccountProjects.Count > 0)
                    {
                        foreach (var projectId in account.AccountProjects.Select(ap => ap.ProjectId))
                        {
                            fullAccounts.Add(new FullAccountViewModel()
                            {
                                Account = accountBrief,
                                DetailAccount = detailAccountBrief,
                                Project = new AccountItemBriefViewModel() { Id = projectId }
                            });
                        }
                    }
                    else
                    {
                        fullAccounts.Add(new FullAccountViewModel()
                        {
                            Account = accountBrief,
                            DetailAccount = detailAccountBrief
                        });
                    }
                }
            }
            else if (account.AccountCostCenters.Count > 0)
            {
                foreach (var costCenterId in account.AccountCostCenters.Select(ac => ac.CostCenterId))
                {
                    var costCenterBrief = new AccountItemBriefViewModel() { Id = costCenterId };
                    if (account.AccountProjects.Count > 0)
                    {
                        foreach (var projectId in account.AccountProjects.Select(ap => ap.ProjectId))
                        {
                            fullAccounts.Add(new FullAccountViewModel()
                            {
                                Account = accountBrief,
                                CostCenter = costCenterBrief,
                                Project = new AccountItemBriefViewModel() { Id = projectId }
                            });
                        }
                    }
                    else
                    {
                        fullAccounts.Add(new FullAccountViewModel()
                        {
                            Account = accountBrief,
                            CostCenter = costCenterBrief
                        });
                    }
                }
            }
            else if (account.AccountProjects.Count > 0)
            {
                foreach (var projectId in account.AccountProjects.Select(ap => ap.ProjectId))
                {
                    fullAccounts.Add(new FullAccountViewModel()
                    {
                        Account = accountBrief,
                        Project = new AccountItemBriefViewModel() { Id = projectId }
                    });
                }
            }
            else
            {
                fullAccounts.Add(new FullAccountViewModel() { Account = accountBrief });
            }

            return fullAccounts;
        }

        private async Task InsertAsync(Voucher voucher, string description)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            OnEntityAction(OperationId.Create);
            Log.Description = description;
            voucher.IsBalanced = voucher.Lines.Sum(line => line.Debit - line.Credit) == 0;
            repository.Insert(voucher, v => v.Lines);
            await FinalizeActionAsync(voucher);
        }

        private async Task<IList<int>> GetBranchIdsAsync()
        {
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
            return await branchRepository
                .GetEntityQuery()
                .Select(br => br.Id)
                .ToListAsync();
        }

        private async Task<IEnumerable<VoucherLine>> GetAccountBalanceLinesAsync(
            Account account, int branchId, string description)
        {
            var branchLines = new List<VoucherLine>();
            var repository = UnitOfWork.GetAsyncRepository<VoucherLine>();
            var accountLines = await repository.GetByCriteriaAsync(
                line => line.FiscalPeriodId == UserContext.FiscalPeriodId &&
                line.BranchId == branchId &&
                line.AccountId == account.Id &&
                line.Voucher.Date <= DateTime.Now.Date);
            foreach (var grouping in GetByCurrencyLineGroups(accountLines))
            {
                var first = grouping.First();
                var closingLine = new VoucherLine()
                {
                    AccountId = account.Id,
                    BranchId = branchId,
                    CostCenterId = first.CostCenterId,
                    CreatedById = UserContext.Id,
                    CurrencyId = first.CurrencyId,
                    CurrencyValue = grouping.Sum(line => line.CurrencyValue),
                    Description = description,
                    DetailId = first.DetailId,
                    FiscalPeriodId = UserContext.FiscalPeriodId,
                    ProjectId = first.ProjectId,
                    TypeId = (short)VoucherLineType.NormalLine
                };
                SetReverseAmounts(closingLine, grouping.Sum(line => line.Debit - line.Credit));
                branchLines.Add(closingLine);
            }

            return branchLines;
        }

        private VoucherLine GetAccountDirectLine(AccountBalanceViewModel balance, string description)
        {
            return new VoucherLine()
            {
                AccountId = balance.AccountId,
                BranchId = balance.BranchId,
                CreatedById = UserContext.Id,
                Debit = balance.DebitBalance,
                Credit = balance.CreditBalance,
                Description = description,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                TypeId = (short)VoucherLineType.NormalLine
            };
        }

        private VoucherLine GetAccountClosingLine(AccountBalanceViewModel balance, string description)
        {
            return new VoucherLine()
            {
                AccountId = balance.AccountId,
                BranchId = balance.BranchId,
                CreatedById = UserContext.Id,
                Debit = balance.CreditBalance,
                Credit = balance.DebitBalance,
                Description = description,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                TypeId = (short)VoucherLineType.NormalLine
            };
        }

        #endregion

        #region Opening Voucher Operations

        private async Task<Voucher> IssueOpeningVoucherAsync()
        {
            var openingVoucher = default(Voucher);
            var lastClosingVoucher = await GetPreviousClosingVoucherAsync();
            if (lastClosingVoucher != null)
            {
                openingVoucher = await IssueOpeningFromLastBalanceAsync(lastClosingVoucher);
            }
            else
            {
                openingVoucher = await GetNewVoucherAsync(AppStrings.OpeningVoucher, VoucherType.OpeningVoucher);
                var branches = await GetBranchIdsAsync();
                foreach (int branchId in branches)
                {
                    openingVoucher.Lines.AddRange(await GetBranchOpeningVoucherLinesAsync(branchId));
                }
            }

            SetRowNumbers(openingVoucher.Lines);
            await InsertAsync(openingVoucher, AppStrings.IssueOpeningVoucher);
            return openingVoucher;
        }

        private async Task<Voucher> GetPreviousClosingVoucherAsync()
        {
            int previousId = await GetPreviousFiscalPeriodIdAsync();
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastClosing = await repository.GetSingleByCriteriaAsync(
                v => v.FiscalPeriodId == previousId &&
                v.Type == (short)VoucherType.ClosingVoucher,
                v => v.Lines);
            return lastClosing;
        }

        private async Task<int> GetPreviousFiscalPeriodIdAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<FiscalPeriod>();
            return await repository
                .GetEntityQuery()
                .Where(fp => fp.Id < UserContext.FiscalPeriodId)
                .OrderByDescending(fp => fp.Id)
                .Select(fp => fp.Id)
                .FirstOrDefaultAsync();
        }

        private async Task<Voucher> IssueOpeningFromLastBalanceAsync(Voucher lastClosingVoucher)
        {
            var openingVoucher = await GetNewVoucherAsync(AppStrings.OpeningVoucher, VoucherType.OpeningVoucher);
            var branches = await GetBranchIdsAsync();
            foreach (int branchId in branches)
            {
                var branchLines = new List<VoucherLine>();
                var closingAccount = await GetBranchClosingAccountAsync(branchId);
                if (closingAccount == null)
                {
                    continue;
                }

                // Start with Asset lines, debit lines first, then credit lines...
                var assetLines = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.Description == AppStrings.ClosingAssetAccounts &&
                        line.Credit > 0.0M);
                branchLines.AddRange(ReverseClosingToOpening(
                    assetLines, AppStrings.OpeningAssetAccounts));
                var assetOpening = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.AccountId == closingAccount.Id &&
                        line.Debit > 0.0M)
                    .Single();
                var openingAccount = await GetBranchOpeningAccountAsync(branchId);
                if (openingAccount == null)
                {
                    continue;
                }

                branchLines.Add(ReverseClosingToOpening(
                    assetOpening, openingAccount.Id, AppStrings.OpeningAssetAccounts));

                // Move on to Liability/Equity lines, debit lines first, then credit lines...
                var liabilityOpening = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.AccountId == closingAccount.Id &&
                        line.Credit > 0.0M)
                    .Single();
                branchLines.Add(ReverseClosingToOpening(
                    liabilityOpening, openingAccount.Id, AppStrings.OpeningLiabilityCapitalAccounts));
                var liabilityLines = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.Description == AppStrings.ClosingLiabilityCapitalAccounts &&
                        line.Debit > 0.0M);
                branchLines.AddRange(ReverseClosingToOpening(
                    liabilityLines, AppStrings.OpeningLiabilityCapitalAccounts));

                openingVoucher.Lines.AddRange(branchLines);
            }

            return openingVoucher;
        }

        private IEnumerable<VoucherLine> ReverseClosingToOpening(
            IEnumerable<VoucherLine> lines, string description)
        {
            var cloneLines = lines
                .Select(line => CloneVoucherLine(line))
                .ToList();
            foreach (var line in cloneLines)
            {
                var temp = line.Debit;
                line.Debit = line.Credit;
                line.Credit = temp;
                line.Description = description;
            }

            return cloneLines;
        }

        private VoucherLine ReverseClosingToOpening(VoucherLine line, int accountId, string description)
        {
            var clone = CloneVoucherLine(line);
            clone.AccountId = accountId;
            var temp = clone.Debit;
            clone.Debit = clone.Credit;
            clone.Credit = temp;
            clone.Description = description;
            return clone;
        }

        private VoucherLine CloneVoucherLine(VoucherLine line)
        {
            return new VoucherLine()
            {
                AccountId = line.AccountId,
                Amount = line.Amount,
                BranchId = line.BranchId,
                CostCenterId = line.CostCenterId,
                CreatedById = UserContext.Id,
                Credit = line.Credit,
                CurrencyId = line.CurrencyId,
                CurrencyValue = line.CurrencyValue,
                Debit = line.Debit,
                Description = line.Description,
                DetailId = line.DetailId,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                FollowupNo = line.FollowupNo,
                ProjectId = line.ProjectId,
                TypeId = line.TypeId
            };
        }

        private async Task<IEnumerable<VoucherLine>> GetBranchOpeningVoucherLinesAsync(int branchId)
        {
            var lines = new List<VoucherLine>();
            var openingAccount = await GetBranchOpeningAccountAsync(branchId);
            if (openingAccount == null)
            {
                return lines;
            }

            var assetAccounts = await GetBranchAssetAccountsAsync(branchId);
            foreach (var assetAccount in assetAccounts)
            {
                lines.AddRange(
                    GetAccountOpeningLines(assetAccount, branchId, AppStrings.OpeningAssetAccounts));
            }

            lines.Add(GetAccountOpeningLine(
                openingAccount, branchId, AppStrings.OpeningAssetAccounts));
            lines.Add(GetAccountOpeningLine(
                openingAccount, branchId, AppStrings.OpeningLiabilityCapitalAccounts));

            var liabilityAccounts = await GetBranchCapitalLiabilityAccountsAsync(branchId);
            foreach (var liabilityAccount in liabilityAccounts)
            {
                lines.AddRange(GetAccountOpeningLines(
                    liabilityAccount, branchId, AppStrings.OpeningLiabilityCapitalAccounts));
            }

            return lines;
        }

        private IEnumerable<VoucherLine> GetAccountOpeningLines(
            Account account, int branchId, string description)
        {
            var fullAccounts = GetFullAccounts(account);
            return fullAccounts.Select(fa => new VoucherLine()
            {
                AccountId = fa.Account.Id,
                BranchId = branchId,
                CostCenterId = fa.CostCenter.Id > 0 ? (int?)fa.CostCenter.Id : null,
                CreatedById = UserContext.Id,
                Description = description,
                DetailId = fa.DetailAccount.Id > 0 ? (int?)fa.DetailAccount.Id : null,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                ProjectId = fa.Project.Id > 0 ? (int?)fa.Project.Id : null,
                TypeId = (short)VoucherLineType.NormalLine
            });
        }

        private VoucherLine GetAccountOpeningLine(Account account, int branchId, string description)
        {
            return new VoucherLine()
            {
                AccountId = account.Id,
                BranchId = branchId,
                CreatedById = UserContext.Id,
                Description = description,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                TypeId = (short)VoucherLineType.NormalLine
            };
        }

        private async Task<Account> GetBranchOpeningAccountAsync(int branchId)
        {
            var accounts = await GetCollectionItemsAsync((int)AccountCollectionId.OpeningAccount, branchId);
            return accounts.SingleOrDefault();
        }

        #endregion

        #region Closing Voucher Operations

        private async Task<Voucher> IssueClosingVoucherAsync()
        {
            var closingVoucher = await GetNewVoucherAsync(AppStrings.ClosingVoucher, VoucherType.ClosingVoucher);
            var branches = await GetBranchIdsAsync();
            foreach (int branchId in branches)
            {
                closingVoucher.Lines.AddRange(await GetBranchClosingVoucherLinesAsync(branchId));
            }

            SetRowNumbers(closingVoucher.Lines);
            await InsertAsync(closingVoucher, AppStrings.IssueClosingVoucher);
            return closingVoucher;
        }

        private async Task<IEnumerable<VoucherLine>> GetBranchClosingVoucherLinesAsync(int branchId)
        {
            var lines = new List<VoucherLine>();
            var closingAccount = await GetBranchClosingAccountAsync(branchId);
            if (closingAccount == null)
            {
                return lines;
            }

            var assetLines = new List<VoucherLine>();
            var assetAccounts = await GetBranchAssetAccountsAsync(branchId);
            foreach (var assetAccount in assetAccounts)
            {
                assetLines.AddRange(await GetAccountBalanceLinesAsync(
                    assetAccount, branchId, AppStrings.ClosingAssetAccounts));
            }

            decimal balance = assetLines.Sum(line => line.Debit - line.Credit);
            var balanceItem = GetBalanceItem(closingAccount.Id, branchId, balance);
            lines.Add(GetAccountClosingLine(balanceItem, AppStrings.ClosingAssetAccounts));
            lines.AddRange(assetLines);

            var liabilityLines = new List<VoucherLine>();
            var liabilityAccounts = await GetBranchCapitalLiabilityAccountsAsync(branchId);
            foreach (var liabilityAccount in liabilityAccounts)
            {
                liabilityLines.AddRange(await GetAccountBalanceLinesAsync(
                    liabilityAccount, branchId, AppStrings.ClosingLiabilityCapitalAccounts));
            }

            balance = liabilityLines.Sum(line => line.Debit - line.Credit);
            balanceItem = GetBalanceItem(closingAccount.Id, branchId, balance);
            lines.AddRange(liabilityLines);
            lines.Add(GetAccountClosingLine(balanceItem, AppStrings.ClosingLiabilityCapitalAccounts));

            return lines;
        }

        #endregion

        #region Closing Temp Accounts Voucher Operations

        private async Task<Voucher> IssueClosingTempAccountsVoucherAsync(
            IList<AccountBalanceViewModel> balanceItems)
        {
            var closingVoucher = await GetNewVoucherAsync(
                AppStrings.ClosingTempAccounts, VoucherType.ClosingTempAccounts);
            var branches = await GetBranchIdsAsync();
            foreach (int branchId in branches)
            {
                closingVoucher.Lines.AddRange(await GetBranchClosingTempAccountLinesAsync(
                    branchId, balanceItems));
            }

            SetRowNumbers(closingVoucher.Lines);
            await InsertAsync(closingVoucher, AppStrings.IssueClosingTempAccountsVoucher);
            return closingVoucher;
        }

        private async Task<IEnumerable<VoucherLine>> GetBranchClosingTempAccountLinesAsync(
            int branchId, IList<AccountBalanceViewModel> balanceItems)
        {
            return balanceItems.Count == 0
                ? await GetBranchPerpetualLinesAsync(branchId)
                : await GetBranchPeriodicLinesAsync(branchId, balanceItems);
        }

        private async Task<IEnumerable<VoucherLine>> GetBranchPerpetualLinesAsync(int branchId)
        {
            var lines = new List<VoucherLine>();
            var specialAccounts = await GetPerpetualSpecialAccountsAsync(branchId);

            // Sales accounts by floating items and currency (Debit)...
            // Performance account (Credit)...
            var performanceAccount = specialAccounts[AccountCollectionId.Performance].SingleOrDefault();
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.FinalSales],
                performanceAccount, AppStrings.ClosingSalesAccounts));

            // Performance account (Debit)...
            // Sales reducer accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.SalesRefundDiscount],
                performanceAccount, AppStrings.ClosingSalesReducerAccounts));

            // Performance account (Debit)...
            // Product cost accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.SoldProductCost],
                performanceAccount, AppStrings.ClosingCalculatedCostAccounts));

            // Performance account (Debit)
            // Current Profit-Loss (Credit)
            var profitLossAccount = specialAccounts[AccountCollectionId.CurrentProfitLoss].SingleOrDefault();
            decimal performance = lines
                .Where(line => line.AccountId == performanceAccount.Id)
                .Sum(line => line.Debit - line.Credit);
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, performanceAccount, profitLossAccount,
                AppStrings.ClosingPerformanceToProfitLoss, performance));

            // Current Profit-Loss account (Debit)...
            // Operational cost accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.OperationalCosts],
                profitLossAccount, AppStrings.ClosingOperationalCostAccounts));

            // Current Profit-Loss account (Debit)...
            // Other Cost-Revenue accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.OtherCostRevenue],
                profitLossAccount, AppStrings.ClosingOtherCostRevenueAccounts));

            // Profit-Loss account (Debit)
            // Accumulated Profit-Loss account (Credit)
            var accumulatedAccount = specialAccounts[AccountCollectionId.AccumulatedProfitLoss].SingleOrDefault();
            decimal netProfitLoss = lines
                .Where(line => line.AccountId == profitLossAccount.Id)
                .Sum(line => line.Debit - line.Credit);
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, profitLossAccount, accumulatedAccount,
                AppStrings.ClosingProfitLossToAccumulated, netProfitLoss));

            return lines
                .Where(line => line.Debit > 0.0M || line.Credit > 0.0M);
        }

        private async Task<IEnumerable<VoucherLine>> GetBranchPeriodicLinesAsync(
            int branchId, IList<AccountBalanceViewModel> balanceItems)
        {
            var lines = new List<VoucherLine>();
            var specialAccounts = await GetPeriodicSpecialAccountsAsync(branchId);

            // Product Inventory accounts -- supplied by user (Debit)...
            // Performance account (Credit)...
            var performanceAccount = specialAccounts[AccountCollectionId.Performance].SingleOrDefault();
            if (performanceAccount == null)
            {
                return lines;
            }

            var branchItems = balanceItems.Where(item => item.BranchId == branchId);
            foreach (var item in branchItems)
            {
                lines.Add(GetAccountDirectLine(item, AppStrings.RegisterEndProductInventory));
            }

            decimal totalBalance = lines.Sum(line => line.Debit - line.Credit);
            var balanceItem = GetBalanceItem(performanceAccount.Id, branchId, totalBalance);
            lines.Add(GetAccountClosingLine(balanceItem, AppStrings.RegisterEndProductInventory));

            // Sales accounts by floating items and currency (Debit)...
            // Performance account (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.FinalSales],
                performanceAccount, AppStrings.ClosingSalesAccounts));

            // Performance account (Debit)...
            // Sales reducer accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.SalesRefundDiscount],
                performanceAccount, AppStrings.ClosingSalesReducerAccounts));

            // Performance account (Debit)...
            // Purchase accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.FinalPurchase],
                performanceAccount, AppStrings.ClosingPurchaseAccounts));

            // Purchase reducer accounts by floating items and currency (Debit)...
            // Performance account (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.PurchaseRefundDiscount],
                performanceAccount, AppStrings.ClosingPurchaseReducerAccounts));

            // Performance account (Debit)
            // Current Profit-Loss (Credit)
            var profitLossAccount = specialAccounts[AccountCollectionId.CurrentProfitLoss].SingleOrDefault();
            decimal performance = lines
                .Where(line => line.AccountId == performanceAccount.Id)
                .Sum(line => line.Debit - line.Credit);
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, performanceAccount, profitLossAccount,
                AppStrings.ClosingPerformanceToProfitLoss, performance));

            // Current Profit-Loss account (Debit)...
            // Operational cost accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.OperationalCosts],
                profitLossAccount, AppStrings.ClosingOperationalCostAccounts));

            // Current Profit-Loss account (Debit)...
            // Other Cost-Revenue accounts by floating items and currency (Credit)...
            lines.AddRange(await GetBranchBalancedAccountLinesAsync(
                branchId, specialAccounts[AccountCollectionId.OtherCostRevenue],
                profitLossAccount, AppStrings.ClosingOtherCostRevenueAccounts));

            // Profit-Loss account (Debit)
            // Accumulated Profit-Loss account (Credit)
            var accumulatedAccount = specialAccounts[AccountCollectionId.AccumulatedProfitLoss].SingleOrDefault();
            decimal netProfitLoss = lines
                .Where(line => line.AccountId == profitLossAccount.Id)
                .Sum(line => line.Debit - line.Credit);
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, profitLossAccount, accumulatedAccount,
                AppStrings.ClosingProfitLossToAccumulated, netProfitLoss));

            return lines
                .Where(line => line.Debit > 0.0M || line.Credit > 0.0M);
        }

        private async Task<Dictionary<AccountCollectionId, IEnumerable<Account>>>
            GetPerpetualSpecialAccountsAsync(int branchId)
        {
            var specialAccounts = new Dictionary<AccountCollectionId, IEnumerable<Account>>
            {
                [AccountCollectionId.FinalSales] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.FinalSales, branchId),
                [AccountCollectionId.Performance] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.Performance, branchId),
                [AccountCollectionId.SalesRefundDiscount] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.SalesRefundDiscount, branchId),
                [AccountCollectionId.SoldProductCost] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.SoldProductCost, branchId),
                [AccountCollectionId.CurrentProfitLoss] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.CurrentProfitLoss, branchId),
                [AccountCollectionId.OperationalCosts] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.OperationalCosts, branchId),
                [AccountCollectionId.OtherCostRevenue] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.OtherCostRevenue, branchId),
                [AccountCollectionId.AccumulatedProfitLoss] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.AccumulatedProfitLoss, branchId)
            };
            return specialAccounts;
        }

        private async Task<Dictionary<AccountCollectionId, IEnumerable<Account>>>
            GetPeriodicSpecialAccountsAsync(int branchId)
        {
            var specialAccounts = new Dictionary<AccountCollectionId, IEnumerable<Account>>
            {
                [AccountCollectionId.ProductInventory] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.ProductInventory, branchId),
                [AccountCollectionId.Performance] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.Performance, branchId),
                [AccountCollectionId.FinalSales] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.FinalSales, branchId),
                [AccountCollectionId.SalesRefundDiscount] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.SalesRefundDiscount, branchId),
                [AccountCollectionId.FinalPurchase] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.FinalPurchase, branchId),
                [AccountCollectionId.PurchaseRefundDiscount] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.PurchaseRefundDiscount, branchId),
                [AccountCollectionId.CurrentProfitLoss] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.CurrentProfitLoss, branchId),
                [AccountCollectionId.OperationalCosts] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.OperationalCosts, branchId),
                [AccountCollectionId.OtherCostRevenue] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.OtherCostRevenue, branchId),
                [AccountCollectionId.AccumulatedProfitLoss] = await GetCollectionItemsAsync(
                    (int)AccountCollectionId.AccumulatedProfitLoss, branchId)
            };
            return specialAccounts;
        }

        private async Task<IEnumerable<VoucherLine>> GetBranchBalancedAccountLinesAsync(
            int branchId, IEnumerable<Account> accounts, Account closureAccount, string description)
        {
            var lines = new List<VoucherLine>();
            if (closureAccount == null)
            {
                return lines;
            }

            var accountLines = new List<VoucherLine>();
            foreach (var account in accounts)
            {
                accountLines.AddRange(
                    await GetAccountBalanceLinesAsync(account, branchId, description));
            }

            lines.AddRange(accountLines);
            decimal balance = accountLines.Sum(line => line.Debit - line.Credit);
            var balanceItem = GetBalanceItem(closureAccount.Id, branchId, balance);
            var closureLine = GetAccountClosingLine(balanceItem, description);
            if (balance < 0)
            {
                lines.Insert(0, closureLine);
            }
            else
            {
                lines.Add(closureLine);
            }

            return lines;
        }

        private IEnumerable<VoucherLine> GetBranchBalancedAccountLines(
            int branchId, Account account, Account closureAccount, string description, decimal balance)
        {
            var lines = new List<VoucherLine>();
            if (closureAccount == null)
            {
                return lines;
            }

            var balanceItem = GetBalanceItem(account.Id, branchId, balance);
            lines.Add(GetAccountClosingLine(balanceItem, description));
            balanceItem = GetBalanceItem(closureAccount.Id, branchId, -balance);
            var closureLine = GetAccountClosingLine(balanceItem, description);
            if (balance > 0)
            {
                lines.Insert(0, closureLine);
            }
            else
            {
                lines.Add(closureLine);
            }

            return lines;
        }

        #endregion
    }
}
