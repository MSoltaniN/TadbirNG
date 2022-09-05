using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Reporting;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class DashboardController : ApiControllerBase
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="pathProvider">مسیرهای فایل های کاربردی مورد نیاز سرویس وب را فراهم می کند</param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public DashboardController(IDashboardRepository repository, IApiPathProvider pathProvider,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
            _pathProvider = pathProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/summaries
        [HttpGet]
        [Route(DashboardApi.SummariesUrl)]
        public async Task<IActionResult> GetSummariesAsync()
        {
            var summaries = await _repository.GetSummariesAsync(GetCurrentCalendar());
            Localize(summaries);
            return Json(summaries);
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/license
        [HttpGet]
        [Route(DashboardApi.LicenseInfoUrl)]
        public IActionResult GetLicenseInfo()
        {
            string licenseData = System.IO.File.ReadAllText(_pathProvider.License);
            var license = JsonHelper.To<LicenseFileModel>(licenseData);
            return Json(license);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/current
        [HttpGet]
        [Route(DashboardApi.CurrentDashboardUrl)]
        public IActionResult GetCurrentDashboard()
        {
            var dashboard = _repository.GetCurrentUserDashboard();
            Localize(dashboard);
            return Json(dashboard);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/lookup/functions
        [HttpGet]
        [Route(DashboardApi.WidgetFunctionsLookupUrl)]
        public async Task<IActionResult> GetWidgetFunctionsLookupAsync()
        {
            var lookup = await _repository.GetWidgetFunctionsLookupAsync();
            Array.ForEach(lookup.ToArray(), item => item.Name = _strings[item.Name]);
            return Json(lookup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/lookup/wtypes
        [HttpGet]
        [Route(DashboardApi.WidgetTypesLookupUrl)]
        public async Task<IActionResult> GetWidgetTypesLookupAsync()
        {
            var lookup = await _repository.GetWidgetTypesLookupAsync();
            Array.ForEach(lookup.ToArray(), item => item.Name = _strings[item.Name]);
            return Json(lookup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/lookup/widgets
        [HttpGet]
        [Route(DashboardApi.WidgetsLookupUrl)]
        public async Task<IActionResult> GetWidgetsLookupAsync()
        {
            var lookup = await _repository.GetWidgetsLookupAsync();
            Array.ForEach(lookup.ToArray(), widget =>
            {
                widget.FunctionName = _strings[widget.FunctionName];
                widget.TypeName = _strings[widget.TypeName];
            });
            return Json(lookup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/dashboard/widgets/{widgetId:min(1)}/data
        [HttpGet]
        [Route(DashboardApi.WidgetDataUrl)]
        public async Task<IActionResult> GetWidgetDataAsync(
            int widgetId, DateTime? from, DateTime? to, WidgetDateUnit? unit)
        {
            var widgetData = await _repository.GetWidgetDataAsync(widgetId, from, to, unit);
            return Json(widgetData);
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

        private void Localize(DashboardViewModel dashboard)
        {
            if (dashboard == null)
            {
                return;
            }

            foreach (var tab in dashboard.Tabs)
            {
                foreach (var widget in tab.Widgets)
                {
                    widget.WidgetFunctionName = _strings[widget.WidgetFunctionName];
                    widget.WidgetTypeName = _strings[widget.WidgetTypeName];
                }
            }
        }

        private readonly IDashboardRepository _repository;
        private readonly IApiPathProvider _pathProvider;
    }
}