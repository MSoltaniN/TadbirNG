using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class DetailAccountsController : ValidatingController<DetailAccountViewModel>
    {
        public DetailAccountsController(
            IDetailAccountRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.DetailAccount; }
        }

        // GET: api/faccounts/fp/{fpId:min(1)}/branch/{branchId:min(1)}
        [Route(DetailAccountApi.FiscalPeriodBranchDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountsAsync(int fpId, int branchId)
        {
            int itemCount = await _repository.GetCountAsync(fpId, branchId, GridOptions);
            SetItemCount(itemCount);
            var detailAccounts = await _repository.GetDetailAccountsAsync(fpId, branchId, GridOptions);
            return Json(detailAccounts);
        }

        // GET: api/faccounts/{faccountId:min(1)}
        [Route(DetailAccountApi.DetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountAsync(int faccountId)
        {
            var detailAccount = await _repository.GetDetailAccountAsync(faccountId);
            return JsonReadResult(detailAccount);
        }

        // GET: api/faccounts/{faccountId:min(1)}/children
        [Route(DetailAccountApi.DetailAccountChildrenUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountChildrenAsync(int faccountId)
        {
            var children = await _repository.GetDetailAccountChildrenAsync(faccountId);
            return JsonReadResult(children);
        }

        // GET: api/faccounts/metadata
        [Route(DetailAccountApi.DetailAccountMetadataUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountMetadataAsync()
        {
            var metadata = await _repository.GetDetailAccountMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/faccounts
        [HttpPost]
        [Route(DetailAccountApi.DetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Create)]
        public async Task<IActionResult> PostNewDetailAccountAsync([FromBody] DetailAccountViewModel detailAccount)
        {
            var result = await ValidationResultAsync(detailAccount);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveDetailAccountAsync(detailAccount);
            return StatusCode(StatusCodes.Status201Created, outputItem);
        }

        // PUT: api/faccounts/{faccountId:min(1)}
        [HttpPut]
        [Route(DetailAccountApi.DetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Edit)]
        public async Task<IActionResult> PutModifiedDetailAccountAsync(
            int faccountId, [FromBody] DetailAccountViewModel detailAccount)
        {
            var result = await ValidationResultAsync(detailAccount, faccountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            var outputItem = await _repository.SaveDetailAccountAsync(detailAccount);
            result = (outputItem != null)
                ? Ok(outputItem)
                : NotFound() as IActionResult;
            return result;
        }

        // DELETE: api/faccounts/{faccountId:min(1)}
        [HttpDelete]
        [Route(DetailAccountApi.DetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Delete)]
        public async Task<IActionResult> DeleteExistingDetailAccountAsync(int faccountId)
        {
            string result = await ValidateDeleteAsync(faccountId);
            if (!String.IsNullOrEmpty(result))
            {
                return BadRequest(result);
            }

            await _repository.DeleteDetailAccountAsync(faccountId);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        private async Task<IActionResult> ValidationResultAsync(DetailAccountViewModel detailAccount, int faccountId = 0)
        {
            var result = BasicValidationResult(detailAccount, faccountId);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            if (await _repository.IsDuplicateDetailAccountAsync(detailAccount))
            {
                return BadRequest(_strings.Format(AppStrings.DuplicateCodeValue, AppStrings.DetailAccount));
            }

            return Ok();
        }

        private async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var detailItem = await _repository.GetDetailAccountAsync(item);
            if (detailItem == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.DetailAccount), item);
            }

            var detailInfo = String.Format("'{0} ({1})'", detailItem.Name, detailItem.Code);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteNonLeafItem], _strings[AppStrings.DetailAccount], detailInfo);
            }

            if (await _repository.IsUsedDetailAccountAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteUsedItem], _strings[AppStrings.DetailAccount], detailInfo);
            }

            return message;
        }

        private IDetailAccountRepository _repository;
    }
}