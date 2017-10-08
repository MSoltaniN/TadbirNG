using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Tadbir.GridViewCRUD.Controllers
{

    public class Filter
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }


    public class Options
    {
        public int Start { get; set; }
        public int Count { get; set; }

        public IList<Filter> Filters { get; set; }
        public string Order { get; set; }
    }

    public class AccountController : Controller
    {
        public IAccountRepository AccountRepo;

        public AccountController(IAccountRepository Repo)
        {
            AccountRepo = Repo;
        }
               


        [HttpGet, Produces("application/json")]
        [Route("/Account/GetTotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            var data = await AccountRepo.GetAllAccount();
            return Json(new { result = data.Count });
        }
              

        
        
        



        [HttpPost, Produces("application/json")]       
        [Route("/Account/GetLazyAccounts")]
        public async Task<IActionResult> GetLazyAccounts([FromBody] Options option)
        { 
           


            var data = await AccountRepo.GetAllAccount();
            return Json(new { result = data.Skip(option.Start).Take(option.Count) });
        }
    }
}
