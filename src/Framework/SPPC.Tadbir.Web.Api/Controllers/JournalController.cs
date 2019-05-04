using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Extensions;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class JournalController : ApiControllerBase
    {
        public JournalController(IJournalRepository repository, IConfigRepository config,
            IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
            _configRepository = config;
        }

        // GET: api/reports/journal/by-date/by-row
        [Route(JournalApi.JournalByDateByRowUrl)]
        public async Task<IActionResult> GetJournalByDateByRowAsync(DateTime? from, DateTime? to)
        {
            Sanitize(ref from, ref to);
            var gridOptions = GridOptions ?? new GridOptions();
            _repository.SetCurrentContext(SecurityContext.User);
            var journal = await _repository.GetJournalByDateAsync(JournalMode.ByRows, from.Value, to.Value);
            PrepareJournal(journal, gridOptions);
            return Json(journal);
        }

        private void Sanitize(ref DateTime? from, ref DateTime? to)
        {
            if (from == null || to == null)
            {
                DateTime rangeFrom, rangeTo;
                _configRepository.SetCurrentContext(SecurityContext.User);
                _configRepository.GetCurrentFiscalDateRange(out rangeFrom, out rangeTo);
                from = from ?? rangeFrom;
                to = to ?? rangeTo;
            }
        }

        private void PrepareJournal(JournalViewModel journal, GridOptions gridOptions)
        {
            var userItems = journal.Items.Apply(gridOptions, false);
            journal.DebitSum = userItems.Select(item => item.Debit).Sum();
            journal.CreditSum = userItems.Select(item => item.Credit).Sum();
            SetItemCount(userItems.Count());
            journal.SetItems(journal.Items.Apply(gridOptions).ToList());
            int rowNo = (gridOptions.Paging.PageSize * (gridOptions.Paging.PageIndex - 1)) + 1;
            foreach (var journalItem in journal.Items)
            {
                journalItem.RowNo = rowNo++;
            }
        }

        private readonly IJournalRepository _repository;
        private readonly IConfigRepository _configRepository;
    }
}