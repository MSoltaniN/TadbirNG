using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Corporate;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Inventory;

namespace SPPC.Tadbir.Persistence
{
    public partial class VoucherRepository
    {
        /// <summary>
        /// به روش آسنکرون، مشخص می کند که شعبه داده شده امکان صدور سندهای ویژه را دارد یا نه
        /// </summary>
        /// <param name="branchId">شناسه دیتابیسی شعبه داده شده</param>
        /// <returns>اگر شعبه داده شده امکان صدور سندهای ویژه را داشته باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> CanIssueSpecialVoucherAsync(int branchId)
        {
            var repository = UnitOfWork.GetAsyncRepository<Branch>();
            var parentId = await repository
                .GetEntityQuery()
                .Where(br => br.Id == branchId)
                .Select(br => br.ParentId)
                .SingleOrDefaultAsync();
            return parentId == null;
        }

        /// <summary>
        /// به روش آسنکرون، مشخص می کند که سند ویژه مشخص شده در دوره مالی جاری - در صورت وجود - ثبت شده است یا نه
        /// </summary>
        /// <param name="type">مأخذ سند ویژه مورد نظر</param>
        /// <returns>در صورتی که سند ویژه صادر و ثبت شده باشد، مقدار بولی "درست" و
        /// در غیر این صورت مقدار بولی "نادرست" را برمی گرداند</returns>
        public async Task<bool> IsCurrentSpecialVoucherCheckedAsync(VoucherOriginId type)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            int closingCount = await repository.GetCountByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId &&
                v.OriginId == (int)type &&
                v.StatusId >= (int)DocumentStatusId.Checked);
            return closingCount == 1;
        }

        /// <summary>
        /// به روش آسنکرون، سند افتتاحیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="isQuery">مشخص می کند که در صورت وجود نداشتن، باید
        /// از کاربر تأیید گرفته شود یا نه</param>
        /// <param name="isDefault">مشخص می کند که اولین سند افتتاحیه باید به صورت پیش فرض
        /// و با مبالغ صفر ایجاد شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند افتتاحیه در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetOpeningVoucherAsync(bool isQuery = false, bool isDefault = true)
        {
            var openingVoucher = await GetCurrentSpecialVoucherAsync(VoucherOriginId.OpeningVoucher);
            if (openingVoucher == null)
            {
                if (isQuery)
                {
                    return null;
                }
                else
                {
                    openingVoucher = await IssueOpeningVoucherAsync(isDefault);
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
                v.OriginId == (int)VoucherOriginId.ClosingVoucher);
            return count == 1;
        }

        /// <summary>
        /// به روش آسنکرون، سند بستن حساب های موقت مربوط به دوره مالی جاری را
        /// برای سیستم ثبت دائمی خوانده و برمی گرداند
        /// </summary>
        /// <param name="mustIssue">مشخص می کند که سند مورد نظر، در صورت وجود نداشتن، باید صادر شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetClosingTempAccountsVoucherAsync(bool mustIssue = true)
        {
            var closingVoucher = await GetCurrentSpecialVoucherAsync(VoucherOriginId.ClosingTempAccounts);
            if (closingVoucher == null && mustIssue)
            {
                var balanceItems = new List<AccountBalanceViewModel>();
                closingVoucher = await IssueClosingTempAccountsVoucherAsync(balanceItems);
            }

            return (closingVoucher != null)
                ? Mapper.Map<VoucherViewModel>(closingVoucher)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، سند بستن حساب های موقت مربوط به دوره مالی جاری را
        /// برای سیستم ثبت ادواری خوانده و برمی گرداند
        /// </summary>
        /// <param name="balanceItems">مجموعه مقادیر مانده موجودی انبار - برای سیستم ثبت ادواری</param>
        /// <param name="mustIssue">مشخص می کند که سند مورد نظر، در صورت وجود نداشتن، باید صادر شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند بستن حساب های موقت در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetPeriodicClosingTempAccountsVoucherAsync(
            IList<AccountBalanceViewModel> balanceItems, bool mustIssue = true)
        {
            var closingVoucher = await GetCurrentSpecialVoucherAsync(VoucherOriginId.ClosingTempAccounts);
            if (closingVoucher == null && mustIssue)
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

            return (closingVoucher != null)
                ? Mapper.Map<VoucherViewModel>(closingVoucher)
                : null;
        }

        /// <summary>
        /// به روش آسنکرون، سند اختتامیه مربوط به دوره مالی جاری را خوانده و برمی گرداند
        /// </summary>
        /// <param name="mustIssue">مشخص می کند که سند مورد نظر، در صورت وجود نداشتن، باید صادر شود یا نه</param>
        /// <returns>اطلاعات نمایشی سند اختتامیه در دوره مالی جاری</returns>
        public async Task<VoucherViewModel> GetClosingVoucherAsync(bool mustIssue = true)
        {
            var closingVoucher = await GetCurrentSpecialVoucherAsync(VoucherOriginId.ClosingVoucher);
            if (closingVoucher == null && mustIssue)
            {
                closingVoucher = await IssueClosingVoucherAsync();
            }

            return (closingVoucher != null)
                ? Mapper.Map<VoucherViewModel>(closingVoucher)
                : null;
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

        private static IList<FullAccountViewModel> GetFullAccounts(Account account)
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

        private async Task<Voucher> GetCurrentSpecialVoucherAsync(VoucherOriginId origin)
        {
            var repository = UnitOfWork.GetAsyncRepository<Voucher>();
            var openingVoucher = await repository.GetSingleByCriteriaAsync(
                v => v.FiscalPeriodId == UserContext.FiscalPeriodId &&
                v.OriginId == (int)origin);
            return openingVoucher;
        }

        private async Task<Account> GetBranchClosingAccountAsync(int branchId)
        {
            var accounts = await _utility.GetUsableAccountsAsync(AccountCollectionId.ClosingAccount, true, branchId);
            return accounts.SingleOrDefault();
        }

        private async Task<IList<Account>> GetBranchAssetAccountsAsync(int branchId)
        {
            var accounts = new List<Account>();
            accounts.AddRange(await _utility.GetUsableAccountsAsync(AccountCollectionId.LiquidAssets, true, branchId));
            accounts.AddRange(await _utility.GetUsableAccountsAsync(AccountCollectionId.NonLiquidAssets, true, branchId));
            return accounts;
        }

        private async Task<IList<Account>> GetBranchCapitalLiabilityAccountsAsync(int branchId)
        {
            var accounts = new List<Account>();
            accounts.AddRange(await _utility.GetUsableAccountsAsync(AccountCollectionId.LiquidLiabilities, true, branchId));
            accounts.AddRange(await _utility.GetUsableAccountsAsync(AccountCollectionId.NonLiquidLiabilities, true, branchId));
            accounts.AddRange(await _utility.GetUsableAccountsAsync(AccountCollectionId.OwnerEquities, true, branchId));
            return accounts;
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

        private IEnumerable<VoucherLine> GetAccountBalanceLines(
            Account account, int branchId, string description)
        {
            var branchLines = new List<VoucherLine>();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var query = String.Format(
                VoucherQuery.BalanceByItemsByCurrency, UserContext.FiscalPeriodId, branchId, account.Id);
            var result = DbConsole.ExecuteQuery(query);
            var lines = result.Rows
                .Cast<DataRow>()
                .Select(row => GetVoucherLine(row, account.Id, branchId, description));
            foreach (var line in lines)
            {
                SetReverseAmounts(line, line.Debit);
                branchLines.Add(line);
            }

            return branchLines;
        }

        private VoucherLine GetVoucherLine(DataRow row, int accountId, int branchId, string description)
        {
            int costCenterId = _report.ValueOrDefault<int>(row, "CostCenterId");
            int detailAccountId = _report.ValueOrDefault<int>(row, "DetailId");
            int projectId = _report.ValueOrDefault<int>(row, "ProjectId");
            int currencyId = _report.ValueOrDefault<int>(row, "CurrencyId");
            return new VoucherLine()
            {
                AccountId = accountId,
                BranchId = branchId,
                CostCenterId = costCenterId > 0 ? (int?)costCenterId : null,
                CreatedById = UserContext.Id,
                CurrencyId = currencyId > 0 ? (int?)currencyId : null,
                CurrencyValue = _report.ValueOrDefault<int>(row, "CurrencyValueSum"),
                Debit = _report.ValueOrDefault<decimal>(row, "Balance"),
                Description = description,
                DetailId = detailAccountId > 0 ? (int?)detailAccountId : null,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                ProjectId = projectId > 0 ? (int?)projectId : null,
                TypeId = (short)VoucherLineType.NormalLine
            };
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
            if (balance.AccountId == 0)
            {
                return null;
            }

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

        private async Task<Voucher> IssueOpeningVoucherAsync(bool isDefault)
        {
            Voucher openingVoucher;
            var lastClosingVoucher = await GetPreviousClosingVoucherAsync();
            if (lastClosingVoucher != null)
            {
                openingVoucher = await IssueOpeningFromLastBalanceAsync(lastClosingVoucher);
            }
            else
            {
                openingVoucher = await GetNewVoucherAsync(
                    AppStrings.OpeningVoucher, VoucherOriginId.OpeningVoucher);
                if (isDefault)
                {
                    var branches = await GetBranchIdsAsync();
                    foreach (int branchId in branches)
                    {
                        openingVoucher.Lines.AddRange(await GetBranchOpeningVoucherLinesAsync(branchId));
                    }
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
                v.OriginId == (int)VoucherOriginId.ClosingVoucher,
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
            var openingVoucher = await GetNewVoucherAsync(
                AppStrings.OpeningVoucher, VoucherOriginId.OpeningVoucher);
            var branches = await GetBranchIdsAsync();
            foreach (int branchId in branches)
            {
                var branchLines = new List<VoucherLine>();
                var closingAccount = await GetBranchClosingAccountAsync(branchId);
                int closingAccountId = (closingAccount != null)
                    ? closingAccount.Id
                    : 0;

                // Start with Asset lines, debit lines first, then credit lines...
                var assetLines = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.AccountId != closingAccountId &&
                        line.Description == AppStrings.ClosingAssetAccounts)
                    .OrderBy(line => line.RowNo);
                branchLines.AddRange(ReverseClosingToOpening(
                    assetLines, AppStrings.OpeningAssetAccounts));
                var assetOpening = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.AccountId == closingAccountId &&
                        line.Description == AppStrings.ClosingAssetAccounts)
                    .SingleOrDefault();
                var openingAccount = await GetBranchOpeningAccountAsync(branchId);
                int openingAccountId = (openingAccount != null)
                    ? openingAccount.Id
                    : 0;

                branchLines.Add(ReverseClosingToOpening(
                    assetOpening, openingAccountId, AppStrings.OpeningAssetAccounts));

                // Move on to Liability/Equity lines, debit lines first, then credit lines...
                var liabilityOpening = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.AccountId == closingAccountId &&
                        line.Description == AppStrings.ClosingLiabilityCapitalAccounts)
                    .SingleOrDefault();
                branchLines.Add(ReverseClosingToOpening(
                    liabilityOpening, openingAccountId, AppStrings.OpeningLiabilityCapitalAccounts));
                var liabilityLines = lastClosingVoucher.Lines
                    .Where(line => line.BranchId == branchId &&
                        line.AccountId != closingAccountId &&
                        line.Description == AppStrings.ClosingLiabilityCapitalAccounts)
                    .OrderBy(line => line.RowNo);
                branchLines.AddRange(ReverseClosingToOpening(
                    liabilityLines, AppStrings.OpeningLiabilityCapitalAccounts));

                openingVoucher.Lines.AddRange(branchLines.Where(line => line != null));
            }

            return openingVoucher;
        }

        private IEnumerable<VoucherLine> ReverseClosingToOpening(
            IEnumerable<VoucherLine> lines, string description)
        {
            var cloneLines = lines
                .Select(line => CloneVoucherLine(line))
                .ToArray();
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
            if (line == null)
            {
                return null;
            }

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
                RowNo = line.RowNo,
                TypeId = line.TypeId
            };
        }

        private async Task<IEnumerable<VoucherLine>> GetBranchOpeningVoucherLinesAsync(int branchId)
        {
            var lines = new List<VoucherLine>();
            var openingAccount = await GetBranchOpeningAccountAsync(branchId);
            int openingAccountId = (openingAccount != null)
                ? openingAccount.Id
                : 0;

            var assetAccounts = await GetBranchAssetAccountsAsync(branchId);
            foreach (var assetAccount in assetAccounts)
            {
                lines.AddRange(
                    GetAccountOpeningLines(assetAccount, branchId, AppStrings.OpeningAssetAccounts));
            }

            lines.Add(GetAccountOpeningLine(
                openingAccountId, branchId, AppStrings.OpeningAssetAccounts));
            lines.Add(GetAccountOpeningLine(
                openingAccountId, branchId, AppStrings.OpeningLiabilityCapitalAccounts));

            var liabilityAccounts = await GetBranchCapitalLiabilityAccountsAsync(branchId);
            foreach (var liabilityAccount in liabilityAccounts)
            {
                lines.AddRange(GetAccountOpeningLines(
                    liabilityAccount, branchId, AppStrings.OpeningLiabilityCapitalAccounts));
            }

            return lines.Where(line => line != null);
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

        private VoucherLine GetAccountOpeningLine(int accountId, int branchId, string description)
        {
            if (accountId == 0)
            {
                return null;
            }

            return new VoucherLine()
            {
                AccountId = accountId,
                BranchId = branchId,
                CreatedById = UserContext.Id,
                Description = description,
                FiscalPeriodId = UserContext.FiscalPeriodId,
                TypeId = (short)VoucherLineType.NormalLine
            };
        }

        private async Task<Account> GetBranchOpeningAccountAsync(int branchId)
        {
            var accounts = await _utility.GetUsableAccountsAsync(AccountCollectionId.OpeningAccount, true, branchId);
            return accounts.SingleOrDefault();
        }

        #endregion

        #region Closing Voucher Operations

        private async Task<Voucher> IssueClosingVoucherAsync()
        {
            var closingVoucher = await GetNewVoucherAsync(
                AppStrings.ClosingVoucher, VoucherOriginId.ClosingVoucher);
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
                assetLines.AddRange(GetAccountBalanceLines(
                    assetAccount, branchId, AppStrings.ClosingAssetAccounts));
            }

            if (assetAccounts.Count > 0)
            {
                decimal balance = assetLines.Sum(line => line.Debit - line.Credit);
                var balanceItem = GetBalanceItem(closingAccount.Id, branchId, balance);
                lines.Add(GetAccountClosingLine(balanceItem, AppStrings.ClosingAssetAccounts));
                lines.AddRange(assetLines);
            }

            var liabilityLines = new List<VoucherLine>();
            var liabilityAccounts = await GetBranchCapitalLiabilityAccountsAsync(branchId);
            foreach (var liabilityAccount in liabilityAccounts)
            {
                liabilityLines.AddRange(GetAccountBalanceLines(
                    liabilityAccount, branchId, AppStrings.ClosingLiabilityCapitalAccounts));
            }

            if (liabilityAccounts.Count > 0)
            {
                decimal balance = liabilityLines.Sum(line => line.Debit - line.Credit);
                var balanceItem = GetBalanceItem(closingAccount.Id, branchId, balance);
                lines.AddRange(liabilityLines);
                lines.Add(GetAccountClosingLine(balanceItem, AppStrings.ClosingLiabilityCapitalAccounts));
            }

            return lines.Where(line => line.Debit > 0.0M || line.Credit > 0.0M);
        }

        #endregion

        #region Closing Temp Accounts Voucher Operations

        private async Task<Voucher> IssueClosingTempAccountsVoucherAsync(
            IList<AccountBalanceViewModel> balanceItems)
        {
            var closingVoucher = await GetNewVoucherAsync(
                AppStrings.ClosingTempAccounts, VoucherOriginId.ClosingTempAccounts);
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
            int performanceAccountId = (performanceAccount != null)
                ? performanceAccount.Id
                : 0;
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.FinalSales],
                performanceAccount, AppStrings.ClosingSalesAccounts));

            // Performance account (Debit)...
            // Sales reducer accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.SalesRefundDiscount],
                performanceAccount, AppStrings.ClosingSalesReducerAccounts));

            // Performance account (Debit)...
            // Product cost accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.SoldProductCost],
                performanceAccount, AppStrings.ClosingCalculatedCostAccounts));

            // Performance account (Debit)
            // Current Profit-Loss (Credit)
            var profitLossAccount = specialAccounts[AccountCollectionId.CurrentProfitLoss].SingleOrDefault();
            int profitLossAccountId = (profitLossAccount != null)
                ? profitLossAccount.Id
                : 0;
            decimal performance = lines
                .Where(line => line.AccountId == performanceAccountId)
                .Sum(line => line.Debit - line.Credit);
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, performanceAccount, profitLossAccount,
                AppStrings.ClosingPerformanceToProfitLoss, performance));

            // Current Profit-Loss account (Debit)...
            // Operational cost accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.OperationalCosts],
                profitLossAccount, AppStrings.ClosingOperationalCostAccounts));

            // Current Profit-Loss account (Debit)...
            // Other Cost-Revenue accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.OtherCostRevenue],
                profitLossAccount, AppStrings.ClosingOtherCostRevenueAccounts));

            // Profit-Loss account (Debit)
            // Accumulated Profit-Loss account (Credit)
            var accumulatedAccount = specialAccounts[AccountCollectionId.AccumulatedProfitLoss].SingleOrDefault();
            decimal netProfitLoss = lines
                .Where(line => line.AccountId == profitLossAccountId)
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
            int performanceAccountId = (performanceAccount != null)
                ? performanceAccount.Id
                : 0;
            var branchItems = balanceItems.Where(item => item.BranchId == branchId);
            foreach (var item in branchItems)
            {
                lines.Add(GetAccountDirectLine(item, AppStrings.RegisterEndProductInventory));
            }

            decimal totalBalance = lines.Sum(line => line.Debit - line.Credit);
            var balanceItem = GetBalanceItem(performanceAccountId, branchId, totalBalance);
            var closingLine = GetAccountClosingLine(balanceItem, AppStrings.RegisterEndProductInventory);
            if (closingLine != null)
            {
                lines.Add(closingLine);
            }

            // Sales accounts by floating items and currency (Debit)...
            // Performance account (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.FinalSales],
                performanceAccount, AppStrings.ClosingSalesAccounts));

            // Performance account (Debit)...
            // Sales reducer accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.SalesRefundDiscount],
                performanceAccount, AppStrings.ClosingSalesReducerAccounts));

            // Performance account (Debit)...
            // Purchase accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.FinalPurchase],
                performanceAccount, AppStrings.ClosingPurchaseAccounts));

            // Purchase reducer accounts by floating items and currency (Debit)...
            // Performance account (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.PurchaseRefundDiscount],
                performanceAccount, AppStrings.ClosingPurchaseReducerAccounts));

            // Performance account (Debit)
            // Current Profit-Loss (Credit)
            var profitLossAccount = specialAccounts[AccountCollectionId.CurrentProfitLoss].SingleOrDefault();
            int profitLossAccountId = (profitLossAccount != null)
                ? profitLossAccount.Id
                : 0;
            decimal performance = lines
                .Where(line => line.AccountId == performanceAccountId)
                .Sum(line => line.Debit - line.Credit);
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, performanceAccount, profitLossAccount,
                AppStrings.ClosingPerformanceToProfitLoss, performance));

            // Current Profit-Loss account (Debit)...
            // Operational cost accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.OperationalCosts],
                profitLossAccount, AppStrings.ClosingOperationalCostAccounts));

            // Current Profit-Loss account (Debit)...
            // Other Cost-Revenue accounts by floating items and currency (Credit)...
            lines.AddRange(GetBranchBalancedAccountLines(
                branchId, specialAccounts[AccountCollectionId.OtherCostRevenue],
                profitLossAccount, AppStrings.ClosingOtherCostRevenueAccounts));

            // Profit-Loss account (Debit)
            // Accumulated Profit-Loss account (Credit)
            var accumulatedAccount = specialAccounts[AccountCollectionId.AccumulatedProfitLoss].SingleOrDefault();
            decimal netProfitLoss = lines
                .Where(line => line.AccountId == profitLossAccountId)
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
                [AccountCollectionId.FinalSales] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.FinalSales, true, branchId),
                [AccountCollectionId.Performance] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.Performance, true, branchId),
                [AccountCollectionId.SalesRefundDiscount] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.SalesRefundDiscount, true, branchId),
                [AccountCollectionId.SoldProductCost] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.SoldProductCost, true, branchId),
                [AccountCollectionId.CurrentProfitLoss] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.CurrentProfitLoss, true, branchId),
                [AccountCollectionId.OperationalCosts] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.OperationalCosts, true, branchId),
                [AccountCollectionId.OtherCostRevenue] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.OtherCostRevenue, true, branchId),
                [AccountCollectionId.AccumulatedProfitLoss] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.AccumulatedProfitLoss, true, branchId)
            };
            return specialAccounts;
        }

        private async Task<Dictionary<AccountCollectionId, IEnumerable<Account>>>
            GetPeriodicSpecialAccountsAsync(int branchId)
        {
            var specialAccounts = new Dictionary<AccountCollectionId, IEnumerable<Account>>
            {
                [AccountCollectionId.ProductInventory] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.ProductInventory, true, branchId),
                [AccountCollectionId.FinalSales] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.FinalSales, true, branchId),
                [AccountCollectionId.Performance] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.Performance, true, branchId),
                [AccountCollectionId.SalesRefundDiscount] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.SalesRefundDiscount, true, branchId),
                [AccountCollectionId.FinalPurchase] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.FinalPurchase, true, branchId),
                [AccountCollectionId.PurchaseRefundDiscount] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.PurchaseRefundDiscount, true, branchId),
                [AccountCollectionId.CurrentProfitLoss] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.CurrentProfitLoss, true, branchId),
                [AccountCollectionId.OperationalCosts] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.OperationalCosts, true, branchId),
                [AccountCollectionId.OtherCostRevenue] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.OtherCostRevenue, true, branchId),
                [AccountCollectionId.AccumulatedProfitLoss] = await _utility.GetUsableAccountsAsync(
                    AccountCollectionId.AccumulatedProfitLoss, true, branchId)
            };
            return specialAccounts;
        }

        private IEnumerable<VoucherLine> GetBranchBalancedAccountLines(
            int branchId, IEnumerable<Account> accounts, Account closureAccount, string description)
        {
            var lines = new List<VoucherLine>();
            if (!accounts.Any() || closureAccount == null)
            {
                return lines;
            }

            foreach (var account in accounts)
            {
                lines.AddRange(GetAccountBalanceLines(account, branchId, description));
            }

            decimal balance = lines.Sum(line => line.Debit - line.Credit);
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
            if (account == null || closureAccount == null || balance == 0.0M)
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
