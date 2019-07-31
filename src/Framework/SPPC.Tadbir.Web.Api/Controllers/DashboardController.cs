using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class DashboardController : ApiControllerBase
    {
        public DashboardController(IDashboardRepository repository, IStringLocalizer<AppStrings> strings)
            : base(strings)
        {
            _repository = repository;
        }

        // GET: api/dashboard/summaries
        [Route(DashboardApi.SummariesUrl)]
        public async Task<IActionResult> GetSummariesAsync()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var summaries = await _repository.GetSummariesAsync();
            Localize(summaries);
            return Json(summaries);
        }

        private void Localize(DashboardSummariesViewModel summaries)
        {
            var calendar = new PersianCalendar();
            int currentYear = calendar.GetYear(DateTime.Now);
            summaries.GrossSales.Title = _strings[summaries.GrossSales.Title ?? String.Empty];
            summaries.GrossSales.Legend = _strings[summaries.GrossSales.Legend ?? String.Empty];
            summaries.NetSales.Title = _strings[summaries.NetSales.Title ?? String.Empty];
            summaries.NetSales.Legend = _strings[summaries.NetSales.Legend ?? String.Empty];
        }

        private readonly IDashboardRepository _repository;
    }
}