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
            var openingVoucher = await GetCurrentOpeningVoucherAsync();
            if (openingVoucher == null)
            {
                openingVoucher = await IssueOpeningVoucherAsync();
            }

            return Mapper.Map<VoucherViewModel>(openingVoucher);
        }

        private async Task<Voucher> GetCurrentOpeningVoucherAsync()
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var openingVoucher = await repository.GetSingleByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId &&
                v.Type == (short)VoucherType.OpeningVoucher,
                v => v.Lines);
            return openingVoucher;
        }

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

            int rowNo = 1;
            foreach (var line in openingVoucher.Lines)
            {
                line.RowNo = rowNo++;
            }

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

        private async Task<IList<int>> GetBranchIdsAsync()
        {
            var branchRepository = UnitOfWork.GetAsyncRepository<Branch>();
            return await branchRepository
                .GetEntityQuery()
                .Select(br => br.Id)
                .ToListAsync();
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
            int rowNo = 1;
            var lines = new List<VoucherLine>();
            var assetAccounts = await GetBranchAssetAccountsAsync(branchId);
            foreach (var assetAccount in assetAccounts)
            {
                lines.AddRange(
                    GetAccountOpeningLines(assetAccount, branchId, AppStrings.OpeningAssetAccounts, rowNo));
            }

            var openingAccount = await GetBranchOpeningAccountAsync(branchId);
            lines.Add(GetAccountOpeningLine(
                openingAccount, branchId, AppStrings.OpeningAssetAccounts, rowNo));
            lines.Add(GetAccountOpeningLine(
                openingAccount, branchId, AppStrings.OpeningLiabilityCapitalAccounts, rowNo));

            var liabilityAccounts = await GetBranchCapitalLiabilityAccountsAsync(branchId);
            foreach (var liabilityAccount in liabilityAccounts)
            {
                lines.AddRange(GetAccountOpeningLines(
                    liabilityAccount, branchId, AppStrings.OpeningLiabilityCapitalAccounts, rowNo));
            }

            return lines;
        }

        private IEnumerable<VoucherLine> GetAccountOpeningLines(
            Account account, int branchId, string description, int nextRowNo)
        {
            var fullAccounts = GetFullAccounts(account);
            return fullAccounts.Select(fa => new VoucherLine()
            {
                AccountId = fa.Account.Id,
                BranchId = branchId,
                CostCenterId = fa.CostCenter.Id,
                CreatedById = UserContext.Id,
                Description = description,
                DetailId = fa.DetailAccount.Id,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                ProjectId = fa.Project.Id,
                RowNo = nextRowNo++,
                TypeId = (short)VoucherLineType.NormalLine
            });
        }

        private VoucherLine GetAccountOpeningLine(Account account, int branchId, string description, int nextRowNo)
        {
            return new VoucherLine()
            {
                AccountId = account.Id,
                BranchId = branchId,
                CreatedById = UserContext.Id,
                Description = description,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                RowNo = nextRowNo++,
                TypeId = (short)VoucherLineType.NormalLine
            };
        }

        private async Task<Account> GetBranchOpeningAccountAsync(int branchId)
        {
            var accounts = await GetCollectionItemsAsync((int)AccountCollectionId.OpeningAccount, branchId);
            return accounts.Single();
        }

        private async Task<Account> GetBranchClosingAccountAsync(int branchId)
        {
            var accounts = await GetCollectionItemsAsync((int)AccountCollectionId.ClosingAccount, branchId);
            return accounts.Single();
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
            var assetAccounts = await repository
                .GetEntityQuery()
                .Include(aca => aca.Account)
                    .ThenInclude(acc => acc.AccountDetailAccounts)
                .Include(aca => aca.Account)
                    .ThenInclude(acc => acc.AccountCostCenters)
                .Include(aca => aca.Account)
                    .ThenInclude(acc => acc.AccountProjects)
                .Where(aca => aca.FiscalPeriodId == UserContext.FiscalPeriodId &&
                    aca.BranchId == branchId &&
                    aca.CollectionId == collection)
                .Select(aca => aca.Account)
                .ToListAsync();
            return assetAccounts;
        }

        private IList<FullAccountViewModel> GetFullAccounts(Account account)
        {
            var fullAccounts = new List<FullAccountViewModel>();
            var accountBrief = new AccountItemBriefViewModel() { Id = account.Id };
            if (account.AccountDetailAccounts.Count > 0)
            {
                foreach (var detailAccount in account.AccountDetailAccounts.Select(ada => ada.DetailAccount))
                {
                    var detailAccountBrief = new AccountItemBriefViewModel() { Id = detailAccount.Id };
                    if (account.AccountCostCenters.Count > 0)
                    {
                        foreach (var costCenter in account.AccountCostCenters.Select(ac => ac.CostCenter))
                        {
                            var costCenterBrief = new AccountItemBriefViewModel() { Id = costCenter.Id };
                            if (account.AccountProjects.Count > 0)
                            {
                                foreach (var project in account.AccountProjects.Select(ap => ap.Project))
                                {
                                    fullAccounts.Add(new FullAccountViewModel()
                                    {
                                        Account = accountBrief,
                                        DetailAccount = detailAccountBrief,
                                        CostCenter = costCenterBrief,
                                        Project = new AccountItemBriefViewModel() { Id = project.Id }
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
                        foreach (var project in account.AccountProjects.Select(ap => ap.Project))
                        {
                            fullAccounts.Add(new FullAccountViewModel()
                            {
                                Account = accountBrief,
                                DetailAccount = detailAccountBrief,
                                Project = new AccountItemBriefViewModel() { Id = project.Id }
                            });
                        }
                    }
                }
            }
            else if (account.AccountCostCenters.Count > 0)
            {
                foreach (var costCenter in account.AccountCostCenters.Select(ac => ac.CostCenter))
                {
                    var costCenterBrief = new AccountItemBriefViewModel() { Id = costCenter.Id };
                    if (account.AccountProjects.Count > 0)
                    {
                        foreach (var project in account.AccountProjects.Select(ap => ap.Project))
                        {
                            fullAccounts.Add(new FullAccountViewModel()
                            {
                                Account = accountBrief,
                                CostCenter = costCenterBrief,
                                Project = new AccountItemBriefViewModel() { Id = project.Id }
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
                foreach (var project in account.AccountProjects.Select(ap => ap.Project))
                {
                    fullAccounts.Add(new FullAccountViewModel()
                    {
                        Account = accountBrief,
                        Project = new AccountItemBriefViewModel() { Id = project.Id }
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
    }
}
