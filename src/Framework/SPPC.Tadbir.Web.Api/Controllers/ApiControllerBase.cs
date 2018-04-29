using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.Web.Api.Extensions;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public abstract class ApiControllerBase<TViewModel> : Controller
        where TViewModel : class, new()
    {
        protected ApiControllerBase(IStringLocalizer<AppStrings> strings = null)
        {
            _strings = strings;
        }

        protected abstract string EntityNameKey
        {
            get;
        }

        protected GridOptions GetGridOptions()
        {
            var options = Request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return null;
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return Framework.Helpers.Json.To<GridOptions>(json);
        }

        protected void SetItemCount(int count)
        {
            Response.Headers.Add(AppConstants.TotalCountHeaderName, count.ToString());
        }

        protected IActionResult JsonReadResult(object data)
        {
            var result = (data != null)
                ? Json(data)
                : NotFound() as IActionResult;

            return result;
        }

        protected virtual IActionResult BasicValidationResult(TViewModel item, int itemId = 0)
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

        protected IStringLocalizer<AppStrings> _strings;
    }
}