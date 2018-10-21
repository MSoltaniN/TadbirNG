using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public abstract class ValidatingController<TViewModel> : ApiControllerBase
        where TViewModel : class, new()
    {
        protected ValidatingController(IStringLocalizer<AppStrings> strings = null)
            : base(strings)
        {
        }

        protected abstract string EntityNameKey
        {
            get;
        }

        protected virtual IActionResult BasicValidationResult(TViewModel item, int itemId = 0)
        {
            return GetBasicValidationResult(item, itemId);
        }

        protected virtual IActionResult BasicValidationResult<TOtherModel>(TOtherModel item, int itemId = 0)
        {
            return GetBasicValidationResult(item, itemId);
        }

        protected IActionResult BranchValidationResult<TFiscalView>(TFiscalView item)
            where TFiscalView : class, IFiscalEntityView
        {
            var currentContext = SecurityContext.User;
            if (item.BranchId != currentContext.BranchId)
            {
                return BadRequest(_strings.Format(AppStrings.OtherBranchEditNotAllowed));
            }

            return Ok();
        }

        protected IActionResult ConfigValidationResult<TTreeView>(TTreeView item, ViewTreeConfig treeConfig)
            where TTreeView : class, ITreeEntityView
        {
            Verify.ArgumentNotNull(treeConfig, "treeConfig");
            if (item.Level == treeConfig.MaxDepth)
            {
                string message = String.Format(_strings[AppStrings.TreeLevelsAreTooDeep],
                    treeConfig.MaxDepth, (string)_strings[EntityNameKey]);
                return BadRequest(message);
            }

            var levelConfig = treeConfig.Levels[item.Level];
            int codeLen = levelConfig.CodeLength;
            if (item.Code.Length != codeLen)
            {
                string message = String.Format(_strings[AppStrings.LevelCodeLengthIsIncorrect],
                    (string)_strings[EntityNameKey], levelConfig.Name, levelConfig.CodeLength);
                return BadRequest(message);
            }

            return Ok();
        }

        private IActionResult GetBasicValidationResult(object item, int itemId)
        {
            if (item == null)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedNoData, EntityNameKey));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int id = Int32.Parse(Reflector.GetProperty(item, "Id").ToString());
            if (itemId != id)
            {
                return BadRequest(_strings.Format(AppStrings.RequestFailedConflict, EntityNameKey));
            }

            return Ok();
        }
    }
}