﻿using System;
using System.Web.Mvc;
using PagedList;
using SPPC.Framework.Service;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Web.Filters;
using SwForAll.Platform.Common;

namespace SPPC.Tadbir.Web.Areas.Accounting.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        public TransactionsController(ITransactionService service)
        {
            _service = service;
        }

        // GET: accounting/transactions[?page={page}]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public ActionResult Index(int? page = null)
        {
            var transactions = _service.GetTransactions(TempContext.CurrentFiscalPeriodId);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(transactions.ToPagedList(pageNumber, pageSize));
        }

        // GET: accounting/transactions/create
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public ViewResult Create()
        {
            var transaction = new TransactionViewModel()
            {
                FiscalPeriodId = TempContext.CurrentFiscalPeriodId,
                CreatorId = TempContext.CurrentUserId,
                LastModifierId = TempContext.CurrentUserId,
                Date = JalaliDateTime.Now.ToShortDateString()
            };

            return View(transaction);
        }

        // POST: accounting/transactions/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Create)]
        public ActionResult Create(TransactionViewModel transaction)
        {
            if (transaction == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            JalaliDateTime jalali;
            if (!JalaliDateTime.TryParse(transaction.Date, out jalali))
            {
                ModelState.AddModelError("Date", Strings.InvalidDate);
                return View(transaction);
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
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
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
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Edit)]
        public ActionResult Edit(TransactionFullViewModel fullTransaction)
        {
            if (fullTransaction == null)
            {
                return RedirectToAction("index", "error", new { area = String.Empty });
            }

            JalaliDateTime jalali;
            if (!JalaliDateTime.TryParse(fullTransaction.Transaction.Date, out jalali))
            {
                ModelState.AddModelError("Transaction.Date", Strings.InvalidDate);
                return View(fullTransaction);
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
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.View)]
        public ActionResult Details(int id)
        {
            var transaction = _service.GetDetailTransactionInfo(id);
            if (transaction == null)
            {
                return RedirectToAction("notfound", "error", new { area = String.Empty });
            }

            return View(transaction);
        }

        // GET: accounting/transactions/delete/id
        [AppAuthorize(SecureEntity.Transaction, (int)TransactionPermissions.Delete)]
        public ActionResult Delete(int id)
        {
            _service.DeleteTransaction(id);
            return RedirectToAction("index");
        }

        private ITransactionService _service;
    }
}
