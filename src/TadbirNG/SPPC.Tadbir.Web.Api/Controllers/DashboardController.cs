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
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
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
        // GET: api/dashboard/charts
        [HttpGet]
        [Route(DashboardApi.ChartsUrl)]
        public IActionResult GetChartsAsync()
        {
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chart"></param>
        /// <returns></returns>
        // POST: api/dashboard/charts
        [HttpPost]
        [Route(DashboardApi.ChartsUrl)]
        public IActionResult PostNewChartAsync([FromBody] ChartViewModel chart)
        {
            return Ok(chart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chartId"></param>
        /// <param name="chart"></param>
        /// <returns></returns>
        // PUT: api/dashboard/charts/{chartId:min(1)}
        [HttpPut]
        [Route(DashboardApi.ChartUrl)]
        public IActionResult PutModifiedChartAsync(int chartId, [FromBody] ChartViewModel chart)
        {
            return Ok(chart);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="chartId"></param>
        /// <returns></returns>
        // DELETE: api/dashboard/charts/{chartId:min(1)}
        [HttpDelete]
        [Route(DashboardApi.ChartUrl)]
        public IActionResult DeleteExistingChartAsync(int chartId)
        {
            return StatusCode(204);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDetail"></param>
        /// <returns></returns>
        // PUT: api/dashboard/charts
        [HttpPut]
        [Route(DashboardApi.ChartsUrl)]
        public IActionResult PutExistingChartsAsDeletedAsync([FromBody] ActionDetailViewModel actionDetail)
        {
            return Ok(actionDetail);
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
        private readonly IApiPathProvider _pathProvider;
    }
}