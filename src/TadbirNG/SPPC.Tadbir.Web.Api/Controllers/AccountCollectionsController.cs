﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Produces("application/json")]
    public class AccountCollectionsController : ValidatingController<AccountCollectionAccountViewModel>
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="strings"></param>
        /// <param name="tokenManager"></param>
        public AccountCollectionsController(
            IAccountCollectionRepository repository, IStringLocalizer<AppStrings> strings,
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
            get { return AppStrings.AccountCollection; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        // GET: api/acccollections
        [HttpGet]
        [Route(AccountCollectionApi.AccountCollectionsUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.View)]
        public async Task<IActionResult> GetAccountCollectionCategoriesAsync()
        {
            var accCollection = await _repository.GetCollectionCategoriesAsync();
            return Json(accCollection);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collectionId"></param>
        /// <returns></returns>
        // Get: api/acccollections/{collectionId:min(1)}/accounts
        [HttpGet]
        [Route(AccountCollectionApi.AccountCollectionAccountsUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.View)]
        public async Task<IActionResult> GetAccountCollectionAccountAsync(int collectionId)
        {
            var gridOptions = GridOptions ?? new GridOptions();
            var accounts = await _repository.GetCollectionAccountsAsync(collectionId, gridOptions);
            SetRowNumbers(accounts);
            return Json(accounts);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="collectionId"></param>
        /// <param name="accCollections"></param>
        /// <returns></returns>
        // POST: api/acccollections/{collectionId:min(1)}/accounts
        [HttpPost]
        [Route(AccountCollectionApi.AccountCollectionAccountsUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.Save)]
        public async Task<IActionResult> PostAccountCollectionAccountAsync(
            int collectionId, [FromBody]List<AccountCollectionAccountViewModel> accCollections)
        {
            bool canManage = await _repository.CanBranchManageCollectionAsync(
                SecurityContext.User.BranchId, collectionId);
            if (!canManage)
            {
                return BadRequestResult(_strings[AppStrings.CantManageCollectionFromBranch]);
            }

            await _repository.AddCollectionAccountsAsync(collectionId, accCollections);
            return Ok();
        }

        /// <summary>
        ///به روش آسنکرون، لیست حساب های  مجموعه حساب صندوق و بانک را برمی گرداند.
        /// </summary>
        /// <returns>لیست حساب های متعلق به مجموعه حساب صندوق و بانک را بر میگرداند.</returns>
        // GET: api/acccollections-cashbank
        [HttpGet]
        [Route(AccountCollectionApi.AccountCollectionsCashBankUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.View)]
        public async Task<IActionResult> GetAccountCollectionsSourceAppAsync()
        {
            var accCollection = await _repository.GetCollectionsCashBankAsync();
            return Json(accCollection);
        }

        private readonly IAccountCollectionRepository _repository;
    }
}