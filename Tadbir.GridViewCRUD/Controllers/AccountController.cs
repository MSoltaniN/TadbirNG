using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Business;
using SPPC.Tadbir.DataAccess;

namespace Tadbir.GridViewCRUD.Controllers
{
    public class AccountController : Controller
    {
        private IRepository<Account> _repository;
        public AccountController(IRepository<Account> repo)
        {
            _repository = repo;
        }

        [HttpGet, Produces("application/json")]
        [Route("/Account/TotalCount")]
        public async Task<IActionResult> TotalCount()
        {
            return Json(await _repository.GetCount());
        }

        [HttpPost, Produces("application/json")]
        [Route("/Account/Count")]
        public async Task<IActionResult> Count([FromBody] GridOption options = null)
        {
            return Json(await _repository.GetCount(options));
        }

        ////[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Delete)]
        [HttpPost, Produces("application/json")]
        [Route("/Account/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Could not delete account because it does not exist.");
            }

            return Json(await _repository.Delete(id));
        }

        // POST: api/accounts
        [Route("/Account/Edit")]
        [HttpPost, Produces("application/json")]
        ////[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        public async Task<IActionResult> Edit([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest("Could not post new account because a 'null' value was provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(await _repository.Edit(account));
        }

        ////[AuthorizeRequest(SecureEntity.Account, (int)AccountPermissions.Create)]
        [HttpPost, Produces("application/json")]
        [Route("/Account/Insert")]
        public async Task<IActionResult> Insert([FromBody] Account account)
        {
            if (account == null)
            {
                return BadRequest("Could not post new account because a 'null' value was provided.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Json(await _repository.Insert(account));
        }

        // GET: api/accounts/fp/{fpId:int}/branch/{branchId:int}
        [Route("/Account/fp/{fpId}/branch/{branchId}")]
        [HttpPost, Produces("application/json")]
        public async Task<IActionResult> List(int fpId, int branchId, [FromBody] GridOption options = null)
        {
            if (fpId <= 0 || branchId <= 0)
            {
                return NotFound();
            }

            var accounts = await _repository.Get(fpId, branchId, options);
            return Json(accounts);
        }

        [Route("/Account/List")]
        [HttpPost, Produces("application/json")]
        public async Task<IActionResult> List([FromBody] GridOption options = null)
        {
            var accounts = await _repository.Get(options);
            return Json(accounts);
        }
    }
}
