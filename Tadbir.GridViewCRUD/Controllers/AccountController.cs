using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
        [Route("/Account/GetTotalCount")]
        public async Task<IActionResult> GetTotalCount()
        {
            var data = await AccountRepo.GetAllAccount();
            return Json(new { result = data.Count });
        }

        [HttpGet, Produces("application/json")]
        [Route("/Account/GetAccounts/{start}/{count}")]
        public async Task<IActionResult> GetAccounts(int start,int count)
        {
            var data = await AccountRepo.GetAllAccount();
            return Json(new { result = data.Skip(start).Take(count)});
        }

        //[HttpGet, Produces("application/json")]
        //[Route("/Account/GetAccounts/{start}/{count}/order/{order}")]
        //public async Task<IActionResult> GetOrderedAccounts(int start, int count,string order)
        //{
        //    var data = await AccountRepo.GetAllAccount();
        //    return Json(new { result = data.Skip(start).Take(count) });
        //}

        //[HttpGet, Produces("application/json")]
        //[Route("/Account/GetAccounts/{start}/{count}/filter/{filter}")]
        //public async Task<IActionResult> GetFilteredAccounts(int start, int count, string filter)
        //{
        //    var data = await AccountRepo.GetAllAccount();
        //    return Json(new { result = data.Skip(start).Take(count) });
        //}

        public class Options
        {
            public int start { get; set; }
            public int count { get; set; }
            public string filter { get; set; }
            public string order { get; set; }
        }
        



        [HttpGet, Produces("application/json")]
        //[Route("/Account/GetAccounts/{start}/{count}/order/{order}")]
        //[Route("/Account/GetAccounts/{start}/{count}/filter/{filter}")]
        [Route("/Account/GetLazyAccounts/{option}")]
        public async Task<IActionResult> GetLazyAccounts(string option)
        {
            //var filterItems = JsonConvert.DeserializeObject<Dictionary<string, string>>(filter);
            var jsonString = option.ToString();
            Options result = JsonConvert.DeserializeObject<Options>(jsonString);


            var data = await AccountRepo.GetAllAccount();
            return Json(new { result = data.Skip(result.start).Take(result.count) });
        }
    }
}
