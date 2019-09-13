using System;
using System.Globalization;
using System.Linq;
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
            var summaries = await _repository.GetSummariesAsync(GetCurrentCalendar());
            Localize(summaries);
            return Json(summaries);
        }

        private Calendar GetCurrentCalendar()
        {
            string language = GetPrimaryRequestLanguage();
            return language == "fa"
                ? new PersianCalendar()
                : new GregorianCalendar() as Calendar;
        }

        private void Localize(DashboardSummariesViewModel summaries)
        {
            summaries.GrossSales.Title = _strings[summaries.GrossSales.Title ?? String.Empty];
            summaries.GrossSales.Legend = _strings[summaries.GrossSales.Legend ?? String.Empty];
            summaries.NetSales.Title = _strings[summaries.NetSales.Title ?? String.Empty];
            summaries.NetSales.Legend = _strings[summaries.NetSales.Legend ?? String.Empty];
            Array.ForEach(summaries.NetSales.Points.ToArray(), point => point.XValue = _strings[point.XValue]);
            Array.ForEach(summaries.GrossSales.Points.ToArray(), point => point.XValue = _strings[point.XValue]);
        }

        private readonly IDashboardRepository _repository;
    }
}