using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Tadbir.Api;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Api.Filters;

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
            var accounts = await _repository.GetCollectionAccountsAsync(collectionId);
            SetRowNumbers(accounts);
            return Json(accounts);
        }

        // POST: api/acccollections/collection/{collectionId:min(1)}
        [HttpPost]
        [Route(AccountCollectionApi.AccountCollectionAccountUrl)]
        [AuthorizeRequest(SecureEntity.AccountCollection, (int)AccountCollectionPermissions.Create)]
        public async Task<IActionResult> PostAccountCollectionAccountAsync(
            int collectionId, [FromBody]List<AccountCollectionAccountViewModel> accCollections)
        {
            await _repository.AddCollectionAccountsAsync(collectionId, accCollections);
            return Ok();
        }

        private readonly IAccountCollectionRepository _repository;
    }
}