﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Finance;

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

        // GET: accounting/transactions/create
        public ViewResult Create()
        {
            var transaction = new TransactionViewModel()
            {
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                CreatorId = TempContext.CurrentUserId,
                LastModifierId = TempContext.CurrentUserId
            };

            return View(transaction);
        }

        // POST: accounting/transactions/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransactionViewModel transaction)
        {
            if (transaction == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveTransaction(transaction);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Date", response.Message);
                    return View(transaction);
                }

                return RedirectToAction("index");
            }

            return View(transaction);
        }

        // GET: accounting/transactions/edit/id
        public ActionResult Edit(int id)
        {
            var transaction = _service.GetDetailTransactionInfo(id);
            if (transaction == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(transaction);
        }

        // POST: accounting/transactions/edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransactionFullViewModel fullTransaction)
        {
            if (fullTransaction == null)
            {
                return RedirectToAction("index", "error");
            }

            if (ModelState.IsValid)
            {
                var response = _service.SaveTransaction(fullTransaction.Transaction);
                if (response.Result == ServiceResult.ValidationFailed)
                {
                    ModelState.AddModelError("Transaction.Date", response.Message);
                    return View(fullTransaction);
                }

                return RedirectToAction("index");
            }

            return View(fullTransaction);
        }

        // GET: accounting/transactions/details/id
        public ActionResult Details(int id)
        {
            var transaction = _service.GetDetailTransactionInfo(id);
            if (transaction == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(transaction);
        }

        private ITransactionService _service;
    }
}
