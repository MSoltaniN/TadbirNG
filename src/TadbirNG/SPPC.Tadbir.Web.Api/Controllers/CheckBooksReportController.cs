using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Check;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات دفتر دسته های چک را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class CheckBooksReportController : ValidatingController<CheckBookReportViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات دفتر دسته های چک در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public CheckBooksReportController(ICheckBookReportRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت دفتر دسته چک
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.CheckBookReport; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده دفتر دسته های چک را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده دفتر دسته های چک</returns>
        // GET: api/check-books-report
        [HttpGet]
        [Route(CheckBookReportApi.CheckBooksReportUrl)]
        [AuthorizeRequest(SecureEntity.CheckBookReport, (int)CheckBookReportPermissions.View)]
        public async Task<IActionResult> GetCheckBooksReportAsync()
        {
            var checkBooksReport = await _repository.GetCheckBooksReportAsync(GridOptions);
            return JsonListResult(checkBooksReport);
        }

        private readonly ICheckBookReportRepository _repository;
    }
}