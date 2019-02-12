using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    [Produces("application/json")]
    public class AccountCollectionsController : ValidatingController<AccountCollectionAccountViewModel>
    {
        public AccountCollectionsController(
            IAccountCollectionRepository repository, IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
            _repository = repository;
        }

        protected override string EntityNameKey
        {
            get { return AppStrings.AccountCollection; }
        }

        // GET: api/acccollections
        [Route(AccountCollectionApi.AccountCollectionsUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.View)]
        public async Task<IActionResult> GetAccountCollectionCategoriesAsync()
        {
            var accCollection = await _repository.GetAccountCollectionCategoriesAsync();
            return Json(accCollection);
        }

        // Get: api/acccollections/collection/{collectionId:min(1)}
        [Route(AccountCollectionApi.AccountCollectionAccountUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.View)]
        public async Task<IActionResult> GetAccountCollectionAccountAsync(int collectionId)
        {
            _repository.SetCurrentContext(SecurityContext.User);
            int itemCount = await _repository.GetCountAsync(GridOptions);
            SetItemCount(itemCount);
            var accounts = await _repository.GetCollectionAccountsAsync(collectionId, GridOptions);
            return Json(accounts);
        }

        // GET: api/acccollections/metadata
        [Route(AccountCollectionApi.AccountCollectionMetadataUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.View)]
        public async Task<IActionResult> GetAccountCollectionMetadataAsync()
        {
            var metadata = await _repository.GetAccountCollectionMetadataAsync();
            return JsonReadResult(metadata);
        }

        // POST: api/acccollections/collection/{collectionId:min(1)}
        [HttpPost]
        [Route(AccountCollectionApi.AccountCollectionAccountUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.Create)]
        public async Task<IActionResult> PostAccountCollectionAccountAsync(
            int collectionId, [FromBody]List<AccountCollectionAccountViewModel> accCollections)
        {
            _repository.SetCurrentContext(SecurityContext.User);
            await _repository.AddCollectionAccountsAsync(collectionId, accCollections);
            return Ok();
        }

        private readonly IAccountCollectionRepository _repository;
    }
}