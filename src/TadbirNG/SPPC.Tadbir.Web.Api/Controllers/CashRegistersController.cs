using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.CashFlow;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    /// عملیات سرویس وب برای مدیریت اطلاعات صندوق ها را پیاده سازی می کند
    /// </summary>
    [Produces("application/json")]
    public class CashRegistersController : ValidatingController<CashRegisterViewModel>
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="repository">امکان ذخیره و بازیابی اطلاعات صندوق ها در دیتابیس را فراهم می کند</param>
        /// <param name="strings">امکان خواندن متن های چندزبانه را فراهم می کند</param>
        /// <param name="tokenManager">امکان کار با توکن امنیتی برنامه را فراهم می کند</param>
        public CashRegistersController(ICashRegisterRepository repository, IStringLocalizer<AppStrings> strings,
            ITokenManager tokenManager)
            : base(strings, tokenManager)
        {
            _repository = repository;
        }

        /// <summary>
        /// کلید متنی چندزبانه برای موجودیت صندوق
        /// </summary>
        protected override string EntityNameKey
        {
            get { return AppStrings.CashRegister; }
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده صندوق های فعال را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده صندوق های فعال</returns>
        // GET: api/cash-registers
        [HttpGet]
        [Route(CashRegisterApi.CashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.View)]
        public async Task<IActionResult> GetCashRegistersAsync()
        {
            var cashRegisters = await _repository.GetCashRegistersAsync(GridOptions);
            return JsonListResult(cashRegisters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده صندوق های غیر فعال را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده صندوق ها غیر فعال</returns>
        // GET: api/cash-registers/inactive
        [HttpGet]
        [Route(CashRegisterApi.InactiveCashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.View)]
        public async Task<IActionResult> GetInactiveCashRegistersAsync()
        {
            var cashRegisters = await _repository.GetCashRegistersAsync(
                GridOptions, ActiveState.Inactive);
            return JsonListResult(cashRegisters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صفحه بندی شده کلیه صندوق ها را خوانده و برمی گرداند
        /// </summary>
        /// <returns>اطلاعات صفحه بندی شده صندوق ها</returns>
        // GET: api/cash-registers/all
        [HttpGet]
        [Route(CashRegisterApi.AllCashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.View)]
        public async Task<IActionResult> GetAllCashRegistersAsync()
        {
            var cashRegisters = await _repository.GetCashRegistersAsync(
                GridOptions, ActiveState.All);
            return JsonListResult(cashRegisters);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی صندوق مشخص شده با شناسه دیتابیسی را خوانده و برمی گرداند
        /// </summary>
        /// <param name="cashRegisterId">شناسه دیتابیسی صندوق مورد نظر</param>
        /// <returns>اطلاعات نمایشی صندوق مورد نظر</returns>
        // GET: api/cash-registers/{cashRegisterId:min(1)}
        [HttpGet]
        [Route(CashRegisterApi.CashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.View)]
        public async Task<IActionResult> GetCashRegisterAsync(int cashRegisterId)
        {
            var cashRegister = await _repository.GetCashRegisterAsync(cashRegisterId);
            return JsonReadResult(cashRegister);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی یک صندوق جدید را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="cashRegister">اطلاعات نمایشی صندوق جدید</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای صندوق</returns>
        // POST: api/cash-registers
        [HttpPost]
        [Route(CashRegisterApi.CashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Create)]
        public async Task<IActionResult> PostNewCashRegisterAsync([FromBody] CashRegisterViewModel cashRegister)
        {
            var result = await ValidationResultAsync(cashRegister);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCashRegisterAsync(cashRegister);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات نمایشی اصلاح شده برای یک صندوق موجود را پس از اعتبارسنجی در دیتابیس ذخیره می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه دیتابیسی صندوق اصلاح شده</param>
        /// <param name="cashRegister">اطلاعات نمایشی اصلاح شده برای صندوق</param>
        /// <returns>اطلاعات نمایشی ذخیره شده برای صندوق</returns>
        // PUT: api/cash-registers/{cashRegisterId:min(1)}
        [HttpPut]
        [Route(CashRegisterApi.CashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Edit)]
        public async Task<IActionResult> PutModifiedCashRegisterAsync(
            int cashRegisterId, [FromBody] CashRegisterViewModel cashRegister)
        {
            var result = await ValidationResultAsync(cashRegister, cashRegisterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveCashRegisterAsync(cashRegister);
            return OkReadResult(outputItem);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق مشخص شده با شناسه دیتابیسی را غیرفعال می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه دیتابیسی صندوق مورد نظر برای غیرفعال کردن</param>
        // PUT: api/cash-registers/{cashRegisterId:min(1)}/deactivate
        [HttpPut]
        [Route(CashRegisterApi.DeactivateCashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Deactivate)]
        public async Task<IActionResult> PutCashRegisterAsDeactivated(int cashRegisterId)
        {
            return await UpdateActiveStateAsync(cashRegisterId, false);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق مشخص شده با شناسه دیتابیسی را فعال می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه دیتابیسی صندوق مورد نظر برای فعال کردن</param>
        // PUT: api/cash-registers/{cashRegisterId:min(1)}/reactivate
        [HttpPut]
        [Route(CashRegisterApi.ReactivateCashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Reactivate)]
        public async Task<IActionResult> PutCashRegisterAsReactivated(int cashRegisterId)
        {
            return await UpdateActiveStateAsync(cashRegisterId, true);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات صندوق مشخص شده با شناسه دیتابیسی را پس از اعتبارسنجی از دیتابیس حذف می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه دیتابیسی صندوق مورد نظر برای حذف</param>
        // DELETE: api/cash-registers/{cashRegisterId:min(1)}
        [HttpDelete]
        [Route(CashRegisterApi.CashRegisterUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingCashRegisterAsync(int cashRegisterId)
        {
            var result = await ValidateDeleteResultAsync(cashRegisterId);
            if (result != null)
            {
                return BadRequestResult(result.ErrorMessage);
            }

            await _repository.DeleteCashRegisterAsync(cashRegisterId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// به روش آسنکرون، صندوق های داده شده را - در صورت امکان - حذف می کند
        /// </summary>
        /// <param name="actionDetail">اطلاعات مورد نیاز برای عملیات حذف گروهی</param>
        /// <returns>در صورت بروز خطای اعتبارسنجی، کد وضعیتی 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 204 (به معنی نبود اطلاعات) را برمی گرداند</returns>
        // PUT: api/cash-registers
        [HttpPut]
        [Route(CashRegisterApi.CashRegistersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.Delete)]
        public async Task<IActionResult> PutExistingCashRegistersAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            return await GroupDeleteResultAsync(actionDetail, _repository.DeleteCashRegistersAsync);
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات خلاصه از کاربران اختصاص داده شده به صندوق را برمی گرداند
        /// </summary>
        /// <param name="cashRegisterId">شناسه دیتابیسی صندوق مورد نظر</param>
        /// <returns>اطلاعات خلاصه از کاربران اختصاص داده شده به صندوق</returns>
        // GET: api/cash-registers/{cashRegisterId:min(1)}/users
        [HttpGet]
        [Route(CashRegisterApi.CashRegisterUsersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.View)]
        public async Task<IActionResult> GetUserCashRegistersAsync(int cashRegisterId)
        {
            var users = await _repository.GetCashRegisterUsersAsync(cashRegisterId);
            return JsonReadResult(users);
        }

        /// <summary>
        /// به روش آسنکرون، کاربران اختصاص داده شده به صندوق را در دیتابیس اصلاح می کند
        /// </summary>
        /// <param name="cashRegisterId">شناسه دیتابیسی صندوق مورد نظر</param>
        /// <param name="userCashRegisters">اطلاعات جدید کاربران اختصاص داده شده به صندوق</param>
        /// <returns>در صورت بروز خطا، کد وضعیت 400 به همراه پیغام خطا و در غیر این صورت
        /// کد وضعیتی 200 را برمی گرداند</returns>
        // PUT: api/cash-registers/{cashRegisterId:min(1)}/users
        [HttpPut]
        [Route(CashRegisterApi.CashRegisterUsersUrl)]
        [AuthorizeRequest(SecureEntity.CashRegister, (int)CashRegisterPermissions.AssignCashRegisterUser)]
        public async Task<IActionResult> PutModifiedUserCashRegistersAsync(
            int cashRegisterId, [FromBody] RelatedItemsViewModel userCashRegisters)
        {
            var result = BasicValidationResult(userCashRegisters, cashRegisterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            await _repository.SaveCashRegisterUsersAsync(userCashRegisters);
            return Ok();
        }

        /// <summary>
        /// به روش آسنکرون، عمل حذف را برای یکی از صندوق ها اعتبارسنجی می کند
        /// </summary>
        /// <param name="item">شناسه دیتابیسی صندوق مورد نظر برای حذف</param>
        /// <returns>اگر خطای اعتبارسنجی برای حذف وجود داشته باشد، متن محلی شده خطا
        /// و در غیر این صورت رشته خالی را برمی گرداند</returns>
        protected override async Task<GroupActionResultViewModel> ValidateDeleteResultAsync(int item)
        {
            string message = String.Empty;
            var cashRegister = await _repository.GetCashRegisterAsync(item);
            if (cashRegister == null)
            {
                message = _strings.Format(AppStrings.ItemByIdNotFound, AppStrings.CashRegister, item.ToString());
                return GetGroupActionResult(message, cashRegister);
            }

            var result = BranchValidationResult(cashRegister);
            if (result is BadRequestObjectResult errorResult)
            {
                return GetGroupActionResult(errorResult.Value.ToString(), cashRegister);
            }

            bool assignedUsers = await _repository.HasAssignedUsersToCashRegAsync(item);
            if (assignedUsers)
            {
                message = _strings.Format(AppStrings.CantDeleteAssignedCashRegister, cashRegister.Name);
            }

            return GetGroupActionResult(message, cashRegister);
        }

        private async Task<IActionResult> ValidationResultAsync(CashRegisterViewModel cashRegister, int cashRegisterId = 0)
        {
            var result = BasicValidationResult(cashRegister, cashRegisterId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (cashRegisterId > 0)
            {
                result = BranchValidationResult(cashRegister);
                if (result is BadRequestObjectResult)
                {
                    return result;
                }
            }

            bool isDuplicate = await _repository.IsDuplicateCashRegisterName(cashRegister);
            if (isDuplicate)
            {
                return BadRequestResult(_strings.Format(
                    AppStrings.DuplicateNameValue, AppStrings.CashRegister, cashRegister.Name));
            }

            return Ok();
        }

        private async Task<IActionResult> UpdateActiveStateAsync(int cashRegisterId, bool isActive)
        {
            var cashRegister = await _repository.GetCashRegisterAsync(cashRegisterId);
            if (cashRegister == null)
            {
                string message = _strings.Format(
                    AppStrings.ItemByIdNotFound, EntityNameKey, cashRegisterId.ToString());
                return BadRequestResult(message);
            }

            var result = ActiveStateValidationResult(cashRegister);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var repository = _repository as IActiveStateRepository<CashRegisterViewModel>;
            await repository.SetActiveStatusAsync(cashRegister, isActive);
            return Ok();
        }

        private readonly ICashRegisterRepository _repository;
    }
}