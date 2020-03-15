﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Core;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class DetailAccountsController : ValidatingController<DetailAccountViewModel>
    {
        public DetailAccountsController(
            IDetailAccountRepository repository, IConfigRepository config, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
            Verify.ArgumentNotNull(config, "config");
            _config = config;
            _treeConfig = _config.GetViewTreeConfigByViewAsync(ViewName.DetailAccount).Result;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.DetailAccount; }
        }

        // GET: api/faccounts
        [Route(DetailAccountApi.EnvironmentDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetEnvironmentDetailAccountsAsync()
        {
            int itemCount = await _repository.GetCountAsync<DetailAccountViewModel>(GridOptions);
            SetItemCount(itemCount);
            var detailAccounts = await _repository.GetDetailAccountsAsync(GridOptions);
            SetRowNumbers(detailAccounts);
            return Json(detailAccounts);
        }

        // GET: api/faccounts/lookup
        [Route(DetailAccountApi.EnvironmentDetailAccountsLookupUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetEnvironmentDetailAccountsLookupAsync()
        {
            var lookup = await _repository.GetDetailAccountsLookupAsync(GridOptions);
            return Json(lookup);
        }

        // GET: api/faccounts/{faccountId:min(1)}
        [Route(DetailAccountApi.DetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountAsync(int faccountId)
        {
            var detailAccount = await _repository.GetDetailAccountAsync(faccountId);
            return JsonReadResult(detailAccount);
        }

        // GET: api/faccounts/ledger
        [Route(DetailAccountApi.EnvironmentDetailAccountsLedgerUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetEnvironmentDetailAccountsLedgerAsync()
        {
            var detailAccounts = await _repository.GetRootDetailAccountsAsync();
            return JsonReadResult(detailAccounts);
        }

        // GET: api/faccounts/{faccountId:min(1)}/children
        [Route(DetailAccountApi.DetailAccountChildrenUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.View)]
        public async Task<IActionResult> GetDetailAccountChildrenAsync(int faccountId)
        {
            var children = await _repository.GetDetailAccountChildrenAsync(faccountId);
            return JsonReadResult(children);
        }

        // GET: api/faccounts/{faccountId:int}/children/new
        [Route(DetailAccountApi.EnvironmentNewChildDetailAccountUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Create)]
        public async Task<IActionResult> GetEnvironmentNewDetailAccountAsync(int faccountId)
        {
            var newDetail = await _repository.GetNewChildDetailAccountAsync(
                faccountId > 0 ? faccountId : (int?)null);
            if (newDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.ParentItemNotFound, AppStrings.DetailAccount));
            }

            if (newDetail.Level == -1)
            {
                return BadRequest(_strings.Format(AppStrings.ChildItemsNotAllowed, AppStrings.DetailAccount));
            }

            return Json(newDetail);
        }

        // GET: api/faccounts/fullcode/{parentId}
        [HttpGet]
        [Route(DetailAccountApi.DetailAccountFullCodeUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Create | (int)DetailAccountPermissions.Edit)]
        public async Task<IActionResult> GetFullCodeAsync(int parentId)
        {
            if (parentId <= 0)
            {
                return Ok(string.Empty);
            }

            string fullCode = await _repository.GetDetailAccountFullCodeAsync(parentId);

            return Ok(fullCode);
        }

        // POST: api/faccounts
        [HttpPost]
        [Route(DetailAccountApi.EnvironmentDetailAccountsUrl)]
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

        // PUT: api/faccounts
        [HttpPut]
        [Route(DetailAccountApi.EnvironmentDetailAccountsUrl)]
        [AuthorizeRequest(SecureEntity.DetailAccount, (int)DetailAccountPermissions.Delete)]
        public async Task<IActionResult> PutExistingDetailAccountsAsDeletedAsync(
            [FromBody] ActionDetailViewModel actionDetail)
        {
            if (actionDetail == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, AppStrings.GroupAction));
            }

            var result = await ValidateGroupDeleteAsync(actionDetail.Items);
            if (result.Count() > 0)
            {
                return BadRequest(result);
            }

            await _repository.DeleteDetailAccountsAsync(actionDetail.Items);
            return StatusCode(StatusCodes.Status204NoContent);
        }

        protected override async Task<string> ValidateDeleteAsync(int item)
        {
            string message = String.Empty;
            var detailAccount = await _repository.GetDetailAccountAsync(item);
            if (detailAccount == null)
            {
                message = String.Format(
                    _strings.Format(AppStrings.ItemByIdNotFound), _strings.Format(AppStrings.DetailAccount), item);
            }

            var result = BranchValidationResult(detailAccount);
            if (result is BadRequestObjectResult errorResult)
            {
                return errorResult.Value.ToString();
            }

            var detailInfo = String.Format("'{0} ({1})'", detailAccount.Name, detailAccount.Code);
            var hasChildren = await _repository.HasChildrenAsync(item);
            if (hasChildren == true)
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteNonLeafItem], _strings[AppStrings.DetailAccount], detailInfo);
            }
            else if (await _repository.IsUsedDetailAccountAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteUsedItem], _strings[AppStrings.DetailAccount], detailInfo);
            }
            else if (await _repository.IsRelatedDetailAccountAsync(item))
            {
                message = String.Format(
                    _strings[AppStrings.CannotDeleteRelatedItem], _strings[AppStrings.DetailAccount], detailInfo);
            }

            return message;
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
                return BadRequest(_strings.Format(AppStrings.DuplicateCodeValue, AppStrings.DetailAccount, detailAccount.FullCode));
            }

            result = BranchValidationResult(detailAccount);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            result = ConfigValidationResult(detailAccount, _treeConfig.Current);
            if (result is BadRequestObjectResult)
            {
                return result;
            }

            return Ok();
        }

        private readonly IDetailAccountRepository _repository;
        private readonly IConfigRepository _config;
        private readonly ViewTreeFullConfig _treeConfig;
    }
}