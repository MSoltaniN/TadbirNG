using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Tadbir.Service;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    public class TransactionsController : Controller
    {
        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        // GET: accounting/transactions[?page={page}]
        public ActionResult Index(int? page = null)
        {
            var transactions = _service.GetTransactions(TempContext.CurrentFiscalPeriodId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(transactions.ToPagedList(pageNumber, pageSize));
        }

        private ITransactionService _service;
    }
}
