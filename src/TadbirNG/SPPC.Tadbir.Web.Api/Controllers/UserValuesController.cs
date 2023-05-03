using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Core;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    public class UserValuesController : ValidatingController<UserValueViewModel>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public UserValuesController(IUserValueRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.UserValue; }
        }

        /// <summary>
        /// به روش آسنکرون، مجموعه دسته بندی های تعریف شده را خوانده و برمی گرداند
        /// </summary>
        /// <returns>مجموعه دسته بندی های تعریف شده</returns>
        // GET: api/user-values/categories
        [Route(UserValueApi.CategoriesUrl)]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _repository.GetCategoriesAsync();
            Array.ForEach(categories.ToArray(), category => category.Value = _strings[category.Value]);
            return Json(categories);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده مقادیر کاربری برای دسته بندی داده شده را خوانده و برمی گرداند
        /// </summary>
        /// <param name="categoryId">شناسه دیتابیسی دسته بندی مورد نظر</param>
        /// <returns>اطلاعات صفحه بندی شده مقادیر کاربری</returns>
        // GET: api/user-values/categories/{categoryId:min(1)}/values
        [Route(UserValueApi.CategoryValuesUrl)]
        public async Task<IActionResult> GetUserValuesAsync(int categoryId)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            var userValues = await _repository.GetUserValuesAsync(categoryId, gridOptions);
            return JsonListResult(userValues);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک مقدار کاربری جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="categoryId">شناسه دیتابیسی دسته بندی مورد نظر برای ذخیره مقدار کاربری</param>
        /// <param name="userValue">اطلاعات نمایشی مقدار کاربری جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای مقدار کاربری</returns>
        // POST: api/user-values/categories/{categoryId:min(1)}/values
        [HttpPost]
        [Route(UserValueApi.CategoryValuesUrl)]
        public async Task<IActionResult> PostNewUserValueAsync(
            int categoryId, [FromBody] UserValueViewModel userValue)
        {
            var result = BasicValidationResult(userValue);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (userValue.CategoryId != categoryId)
            {
                return BadRequestResult(_strings.Format(AppStrings.RequestFailedConflict, EntityNameKey));
            }

            var outputValue = await _repository.SaveUserValueAsync(userValue);
            return StatusCode(StatusCodes.Status201Created, outputValue);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات مقدار کاربری مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="valueId">شناسه دیتابیسی مقدار کاربری مورد نظر برای حذف</param>
        // DELETE: api/user-values/values/{valueId:min(1)}
        [HttpDelete]
        [Route(UserValueApi.UserValueUrl)]
        public async Task<IActionResult> DeleteExistingUserValueAsync(int valueId)
        {
            await _repository.DeleteUserValueAsync(valueId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، مقادیر کاربری داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/user-values/values
        [HttpPut]
        [Route(UserValueApi.UserValuesUrl)]
        public async Task<IActionResult> PutExistingUserValuesAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteUserValuesAsync);
        }

        private readonly IUserValueRepository _repository;
    }
}