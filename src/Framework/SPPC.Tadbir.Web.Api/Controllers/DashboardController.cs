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
        public IActionResult GetSummaries()
        {
            _repository.SetCurrentContext(SecurityContext.User);
            var summaries = _repository.GetSummaries();
            Localize(summaries);
            return Json(summaries);
        }

        private void Localize(DashboardSummariesViewModel summaries)
        {
            var calendar = new PersianCalendar();
            int currentYear = calendar.GetYear(DateTime.Now);
            summaries.GrossSales.Title = _strings[summaries.GrossSales.Title];
            summaries.GrossSales.Legend = _strings[summaries.GrossSales.Legend];
            summaries.NetSales.Title = _strings[summaries.NetSales.Title];
            summaries.NetSales.Legend = _strings[summaries.NetSales.Legend];
        }

        private readonly IDashboardRepository _repository;
    }
}