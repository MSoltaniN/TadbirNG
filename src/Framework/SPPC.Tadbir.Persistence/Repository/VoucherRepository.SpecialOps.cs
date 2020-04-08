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

namespace SPPC.Tadbir.Persistence
{
    public partial class VoucherRepository
    {
        /// <summary>
        /// به روش آسنکرون، سند افتتاحیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات نمایشی سند افتتاحیه در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetOpeningVoucherAsync()
        {
            var openingVoucher = await GetCurrentSpecialVoucherAsync(VoucherType.OpeningVoucher);
            if (openingVoucher == null)
            {
                openingVoucher = await IssueOpeningVoucherAsync();
            }

            return Mapper.Map<VoucherViewModel>(openingVoucher);
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
                v.Type == (short)voucherType,
                v => v.Lines);
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
                .Where(aca => aca.FiscalPeriodId == UserContext.FiscalPeriodId &&
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

        #endregion

        #region Opening Voucher Operations

        private async Task<Voucher> IssueOpeningVoucherAsync()
        {
            var openingVoucher = default(Voucher);
            var lastClosingVoucher = await GetLastClosingVoucherAsync();
            if (lastClosingVoucher != null)
            {
                openingVoucher = await IssueOpeningFromLastBalanceAsync(lastClosingVoucher);
            }
            else
            {
                openingVoucher = GetNewOpeningVoucher();
                var branches = await GetBranchIdsAsync();
                foreach (int branchId in branches)
                {
                    openingVoucher.Lines.AddRange(await GetBranchOpeningVoucherLinesAsync(branchId));
                }

                SetRowNumbers(openingVoucher.Lines);
            }

            await InsertAsync(openingVoucher, AppStrings.IssueOpeningVoucher);
            return openingVoucher;
        }

        private async Task<Voucher> GetLastClosingVoucherAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var lastClosing = await repository.GetSingleByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId - 1 &&
                v.Type == (short)VoucherType.ClosingVoucher,
                v => v.Lines);
            return lastClosing;
        }

        private async Task<Voucher> IssueOpeningFromLastBalanceAsync(Voucher lastClosingVoucher)
        {
            var openingVoucher = GetNewOpeningVoucher();
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

            SetRowNumbers(openingVoucher.Lines);
            await InsertAsync(openingVoucher, AppStrings.IssueOpeningVoucher);
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

        private Voucher GetNewOpeningVoucher()
        {
            return new Voucher()
            {
                BranchId = UserContext.BranchId,
                DailyNo = 1,
                Date = DateTime.Now.Date,
                Description = AppStrings.OpeningVoucher,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                IssuedById = UserContext.Id,
                IssuerName = UserContext.PersonLastName + ", " + UserContext.PersonFirstName,
                No = 1,
                StatusId = 1,
                SubjectType = 0,
                Type = (short)VoucherType.OpeningVoucher
            };
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
            var closingVoucher = await GetNewClosingVoucherAsync();
            var branches = await GetBranchIdsAsync();
            foreach (int branchId in branches)
            {
                closingVoucher.Lines.AddRange(await GetBranchClosingVoucherLinesAsync(branchId));
            }

            SetRowNumbers(closingVoucher.Lines);
            await InsertAsync(closingVoucher, AppStrings.IssueClosingVoucher);
            return closingVoucher;
        }

        private async Task<Voucher> GetNewClosingVoucherAsync()
        {
            var tempVoucher = new VoucherViewModel()
            {
                Date = DateTime.Now.Date,
                No = await GetLastVoucherNoAsync(),
                FiscalPeriodId = UserContext.FiscalPeriodId,
                SubjectType = 0
            };
            return new Voucher()
            {
                BranchId = UserContext.BranchId,
                DailyNo = await GetNextDailyNoAsync(tempVoucher),
                Date = DateTime.Now.Date,
                Description = AppStrings.ClosingVoucher,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                IssuedById = UserContext.Id,
                IssuerName = UserContext.PersonLastName + ", " + UserContext.PersonFirstName,
                No = tempVoucher.No + 1,
                StatusId = 1,
                SubjectType = 0,
                Type = (short)VoucherType.ClosingVoucher
            };
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
                assetLines.AddRange(await GetAccountClosingLinesAsync(
                    assetAccount, branchId, AppStrings.ClosingAssetAccounts, false));
            }

            decimal total = assetLines.Sum(line => line.Credit);
            lines.Add(GetAccountClosingLine(
                closingAccount, branchId, total, 0.0M, AppStrings.ClosingAssetAccounts));
            lines.AddRange(assetLines);

            var liabilityLines = new List<VoucherLine>();
            var liabilityAccounts = await GetBranchCapitalLiabilityAccountsAsync(branchId);
            foreach (var liabilityAccount in liabilityAccounts)
            {
                liabilityLines.AddRange(await GetAccountClosingLinesAsync(
                    liabilityAccount, branchId, AppStrings.ClosingLiabilityCapitalAccounts));
            }

            total = liabilityLines.Sum(line => line.Debit);
            lines.AddRange(liabilityLines);
            lines.Add(GetAccountClosingLine(
                closingAccount, branchId, 0.0M, total, AppStrings.ClosingLiabilityCapitalAccounts));

            return lines;
        }

        private async Task<IEnumerable<VoucherLine>> GetAccountClosingLinesAsync(
            Account account, int branchId, string description, bool isDebit = true)
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
                var balance = grouping.Sum(line => line.Debit - line.Credit);
                branchLines.Add(new VoucherLine()
                {
                    AccountId = account.Id,
                    BranchId = branchId,
                    CostCenterId = first.CostCenterId,
                    CreatedById = UserContext.Id,
                    CurrencyId = first.CurrencyId,
                    CurrencyValue = grouping.Sum(line => line.CurrencyValue),
                    Debit = isDebit ? balance : 0.0M,
                    Credit = isDebit ? 0.0M : balance,
                    Description = description,
                    DetailId = first.DetailId,
                    FiscalPeriodId = UserContext.FiscalPeriodId,
                    ProjectId = first.ProjectId,
                    TypeId = (short)VoucherLineType.NormalLine
                });
            }

            return branchLines;
        }

        private VoucherLine GetAccountClosingLine(
            Account account, int branchId, decimal debit, decimal credit, string description)
        {
            return new VoucherLine()
            {
                AccountId = account.Id,
                BranchId = branchId,
                CreatedById = UserContext.Id,
                Debit = debit,
                Credit = credit,
                Description = description,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                TypeId = (short)VoucherLineType.NormalLine
            };
        }

        #endregion
    }
}
