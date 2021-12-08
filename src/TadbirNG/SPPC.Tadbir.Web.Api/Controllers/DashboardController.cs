using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Helpers;
using SPPC.Licensing.Model;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;

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
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public DashboardController(IDashboardRepository repository,
            IStringLocalizer<AppStrings> strings, ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
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
            string licenseData = System.IO.File.ReadAllText(_licensePath);
            var license = JsonHelper.To<LicenseFileModel>(licenseData);
            return Json(license);
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
#if DEBUG
        private readonly string _licensePath = @".\wwwroot\license.Development.json";
#else
        private readonly string _licensePath = @".\wwwroot\license";
#endif
    }
}