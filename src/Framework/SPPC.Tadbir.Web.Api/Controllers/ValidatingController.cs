using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
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
            _strings = strings;
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