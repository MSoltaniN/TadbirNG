using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPPC.CodeChallenge.Persistence;
using SPPC.CodeChallenge.Api;
using SPPC.CodeChallenge.ViewModel.Core;

namespace SPPC.CodeChallenge.WebApi.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات مدارس را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class SchoolsController : Controller
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات مدارس در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public SchoolsController(ISchoolRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده مدارس را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده مدارس</returns>
        // GET: api/schools
        [HttpGet]
        [Route(SchoolApi.SchoolsUrl)]
        public async Task<IActionResult> GetSchoolsAsync()
        {
            // Read all schools here...
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی مدرسه مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="schoolId">شناسه دیتابیسی مدرسه مورد نظر</param>
        /// <returns>اطلاعات نمایشی مدرسه مورد نظر</returns>
        // GET: api/schools/{schoolId:min(1)}
        [HttpGet]
        [Route(SchoolApi.SchoolUrl)]
        public async Task<IActionResult> GetSchoolAsync(int schoolId)
        {
            // Read a single school here...
            throw new NotImplementedException();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک مدرسه جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="school">اطلاعات نمایشی مدرسه جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای مدرسه</returns>
        // POST: api/schools
        [HttpPost]
        [Route(SchoolApi.SchoolsUrl)]
        public async Task<IActionResult> PostNewSchoolAsync([FromBody] SchoolViewModel school)
        {
            // Create a new school and return created item here...
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک مدرسه موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="schoolId">شناسه دیتابیسی مدرسه اصلاح شده</param>
        /// <param name="school">اطلاعات نمایشی اصلاح شده برای مدرسه</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای مدرسه</returns>
        // PUT: api/schools/{schoolId:min(1)}
        [HttpPut]
        [Route(SchoolApi.SchoolUrl)]
        public async Task<IActionResult> PutModifiedSchoolAsync(int schoolId, [FromBody] SchoolViewModel school)
        {
            // Update an existing school and return updated item here...
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مدرسه مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="schoolId">شناسه دیتابیسی مدرسه مورد نظر برای حذف</param>
        // DELETE: api/schools/{schoolId:min(1)}
        [HttpDelete]
        [Route(SchoolApi.SchoolUrl)]
        public async Task<IActionResult> DeleteExistingSchoolAsync(int schoolId)
        {
            // Delete an existing school here...
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private readonly ISchoolRepository _repository;
    }
}
