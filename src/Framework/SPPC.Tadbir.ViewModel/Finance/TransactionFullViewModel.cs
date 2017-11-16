using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Workflow;

namespace SPPC.Tadbir.ViewModel.Finance
{
    /// <summary>
    /// اطلاعات کامل یک سند مالی، شامل آرتیکل ها و سوابق اقدامات انجام شده، را نشان می دهد
    /// </summary>
    public class TransactionFullViewModel
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس ایجاد می کند
        /// </summary>
        public TransactionFullViewModel()
        {
            Transaction = new TransactionViewModel();
            Lines = new List<TransactionLineViewModel>();
            Actions = new List<HistoryItemViewModel>();
        }

        /// <summary>
        /// آبجکت دربرگیرنده اطلاعات اصلی سند مالی
        /// </summary>
        public TransactionViewModel Transaction { get; set; }

        /// <summary>
        /// نام دوره مالی مرتبط با سند مالی
        /// </summary>
        [Display(Name = Entities.FiscalPeriod)]
        public string FiscalPeriodName { get; set; }

        /// <summary>
        /// نام شعبه ای که سند مالی برای آن ایجاد شده است
        /// </summary>
        [Display(Name = Entities.Branch)]
        public string BranchName { get; set; }

        /// <summary>
        /// نام شرکتی که سند مالی برای یکی از شعبات آن ایجاد شده است
        /// </summary>
        [Display(Name = Entities.Company)]
        public string BranchCompanyName { get; set; }

        /// <summary>
        /// مجموعه ای از آرتیکل های موجود در سند مالی
        /// </summary>
        public IList<TransactionLineViewModel> Lines { get; private set; }

        /// <summary>
        /// مجموعه ای از اقدامات انجام شده روی سند مالی
        /// </summary>
        public IList<HistoryItemViewModel> Actions { get; private set; }
    }
}
