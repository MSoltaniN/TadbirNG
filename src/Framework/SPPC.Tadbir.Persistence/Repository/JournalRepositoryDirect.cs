using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using SPPC.Framework.Extensions;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Persistence
{
    public class JournalRepositoryDirect : LoggingRepositoryBase, IJournalRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="context">امکانات مشترک مورد نیاز را برای عملیات دیتابیسی فراهم می کند</param>
        /// <param name="system">امکانات مورد نیاز در دیتابیس های سیستمی را فراهم می کند</param>
        public JournalRepositoryDirect(IRepositoryContext context, ISystemRepository system)
            : base(context, system.Logger)
        {
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ</returns>
        public async Task<JournalViewModel> GetJournalByDateAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    sourceList = SourceListId.JournalByDateByRow;
                    journal = GetJournalByRow(parameters);
                    break;
                case JournalMode.ByRowsWithDetail:
                    sourceList = SourceListId.JournalByDateByRowDetail;
                    journal = GetJournalByRow(parameters);
                    break;
                case JournalMode.ByLedger:
                    sourceList = SourceListId.JournalByDateByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    sourceList = SourceListId.JournalByDateBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    sourceList = SourceListId.JournalByDateSummary;
                    break;
                case JournalMode.LedgerSummaryByDate:
                    sourceList = SourceListId.JournalByDateSummaryByDate;
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    sourceList = SourceListId.JournalByDateSummaryByMonth;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب تاریخ و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByDateByBranchAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = GetJournalByRow(parameters);
                    sourceList = SourceListId.JournalByDateByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = GetJournalByRow(parameters);
                    sourceList = SourceListId.JournalByDateByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    sourceList = SourceListId.JournalByDateByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    sourceList = SourceListId.JournalByDateBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    sourceList = SourceListId.JournalByDateSummary;
                    break;
                case JournalMode.LedgerSummaryByDate:
                    sourceList = SourceListId.JournalByDateSummaryByDate;
                    break;
                case JournalMode.MonthlyLedgerSummary:
                    sourceList = SourceListId.JournalByDateSummaryByMonth;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند</returns>
        public async Task<JournalViewModel> GetJournalByNoAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = GetJournalByRow(parameters, true);
                    sourceList = SourceListId.JournalByNoByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = GetJournalByRow(parameters, true);
                    sourceList = SourceListId.JournalByNoByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    sourceList = SourceListId.JournalByNoByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    sourceList = SourceListId.JournalByNoBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    sourceList = SourceListId.JournalByNoSummary;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات گزارش دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه را خوانده و برمی گرداند
        /// </summary>
        /// <param name="parameters">پارامترهای مورد نیاز برای گزارش</param>
        /// <returns>اطلاعات دفتر روزنامه بر حسب شماره سند و به تفکیک شعبه</returns>
        public async Task<JournalViewModel> GetJournalByNoByBranchAsync(JournalParameters parameters)
        {
            var journal = default(JournalViewModel);
            var sourceList = SourceListId.None;
            switch (parameters.Mode)
            {
                case JournalMode.ByRows:
                    journal = GetJournalByRow(parameters, true);
                    sourceList = SourceListId.JournalByNoByRow;
                    break;
                case JournalMode.ByRowsWithDetail:
                    journal = GetJournalByRow(parameters, true);
                    sourceList = SourceListId.JournalByNoByRowDetail;
                    break;
                case JournalMode.ByLedger:
                    sourceList = SourceListId.JournalByNoByLedger;
                    break;
                case JournalMode.BySubsidiary:
                    sourceList = SourceListId.JournalByNoBySubsidiary;
                    break;
                case JournalMode.LedgerSummary:
                    sourceList = SourceListId.JournalByNoSummary;
                    break;
                default:
                    break;
            }

            await OnSourceActionAsync(parameters.GridOptions, sourceList);
            return journal;
        }

        private JournalViewModel GetJournalByRow(JournalParameters parameters, bool byNo = false)
        {
            var journal = new JournalViewModel();
            DbConsole.ConnectionString = UnitOfWork.CompanyConnection;
            var paging = parameters.GridOptions.Paging;
            int fromRow = (paging.PageSize * (paging.PageIndex - 1)) + 1;
            int toRow = paging.PageSize * paging.PageIndex;
            var query = !byNo
                ? new ReportQuery(String.Format(JournalQuery.ByDateByRow,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false), fromRow, toRow))
                : new ReportQuery(String.Format(JournalQuery.ByNoByRow,
                    parameters.FromNo, parameters.ToNo, fromRow, toRow));
            query.ApplyOptions(parameters.GridOptions);
            var result = DbConsole.ExecuteQuery(query.Query);
            journal.Items.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => GetJournalItem(row)));

            query = !byNo
                ? new ReportQuery(String.Format(JournalQuery.MainByDateByRow,
                    parameters.FromDate.ToShortDateString(false), parameters.ToDate.ToShortDateString(false)))
                : new ReportQuery(String.Format(JournalQuery.MainByNoByRow,
                    parameters.FromNo, parameters.ToNo));
            query.ApplyOptions(parameters.GridOptions);
            result = DbConsole.ExecuteQuery(query.Query);
            journal.TotalCount = Int32.Parse(result.Rows[0]["TotalCount"].ToString());
            journal.DebitSum = Decimal.Parse(result.Rows[0]["DebitSum"].ToString());
            journal.CreditSum = Decimal.Parse(result.Rows[0]["CreditSum"].ToString());
            return journal;
        }

        private JournalItemViewModel GetJournalItem(DataRow row)
        {
            return new JournalItemViewModel()
            {
                RowNo = Int32.Parse(row["RowNum"].ToString()),
                VoucherDate = DateTime.Parse(row["Date"].ToString()),
                VoucherNo = Int32.Parse(row["No"].ToString()),
                AccountFullCode = row["FullCode"].ToString(),
                AccountName = row["Name"].ToString(),
                DetailAccountFullCode = row["DetailFullCode"].ToString(),
                DetailAccountName = row["DetailName"].ToString(),
                CostCenterFullCode = row["CostFullCode"].ToString(),
                CostCenterName = row["CostName"].ToString(),
                ProjectFullCode = row["ProjectFullCode"].ToString(),
                ProjectName = row["ProjectName"].ToString(),
                Description = row["Description"].ToString(),
                Debit = Decimal.Parse(row["Debit"].ToString()),
                Credit = Decimal.Parse(row["Credit"].ToString()),
                Mark = row["Mark"].ToString(),
                BranchName = row["BranchName"].ToString()
            };
        }
    }
}
