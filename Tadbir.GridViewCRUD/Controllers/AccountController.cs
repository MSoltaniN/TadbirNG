using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tadbir.GridViewCRUD.Controllers
{
    
    public class AccountController : Controller
    {
        public IAccountRepository AccountRepo;

        public AccountController(IAccountRepository Repo)
        {
            AccountRepo = Repo;
        }

        [HttpGet, Produces("application/json")]
        public async Task<IActionResult> GetAccounts()
        { 
            var data = await AccountRepo.GetAllAccount();
            return Json(new { result = data });
        }


        [HttpGet, Produces("application/json")]
        [Route("/Account/GetAccounts/{start}/{count}")]
        public async Task<IActionResult> GetAccounts(int start,int count)
        {
            var data = await AccountRepo.GetAllAccount();
            return Json(new { result = data.Skip(start).Take(count)});
        }
    }
}
