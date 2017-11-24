
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SPPC.Tadbir.Business;
using SPPC.Tadbir.DataAccess;


namespace Tadbir.GridViewCRUD.Controllers
{
    public class FullAccountController : Controller
    {
        private IFullAccountRepository _repository;
        public FullAccountController(IFullAccountRepository repo)
        {
            _repository = repo;
        }

        // GET: api/accounts/fp/{fpId:int}/branch/{branchId:int}
        [Route("/FullAccount/{accountId}")]
        [HttpPost, Produces("application/json")]
        public async Task<IActionResult> List(int accountId)
        {
            var accounts = await _repository.GetFullAccount(accountId);
            return Json(accounts);
        }
    }
}
