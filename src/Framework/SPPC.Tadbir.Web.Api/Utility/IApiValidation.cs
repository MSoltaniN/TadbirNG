using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Persistence;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Web.Api.Utility
{
    public interface IApiValidation : IApiBehavior
    {
        string EntityNameKey { get; set; }

        IActionResult BasicValidationResult<TViewModel>(
            TViewModel item, ModelStateDictionary modelState, int itemId = 0)
            where TViewModel : class, new();

        IActionResult BranchValidationResult<TFiscalView>(TFiscalView item)
            where TFiscalView : class, IFiscalEntityView;

        IActionResult ConfigValidationResult<TTreeView>(TTreeView item, ViewTreeConfig treeConfig)
            where TTreeView : class, ITreeEntityView;

        Task<IActionResult> FullAccountValidationResult(
            FullAccountViewModel fullAccount, IRelationRepository repository);

        Task<IEnumerable<string>> ValidateGroupDeleteAsync(
            IEnumerable<int> items, DeleteValidatorDelegate deleteValidator);
    }

    public delegate Task<string> DeleteValidatorDelegate(int item);
}
