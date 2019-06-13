using System;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.Web.Api.Resources.Types;

namespace SPPC.Tadbir.Web.Api.Controllers
{
    public abstract class ApiControllerBase : Controller
    {
        protected ApiControllerBase(IStringLocalizer<AppStrings> strings = null)
        {
            _strings = strings;
        }

        protected SecurityContext SecurityContext
        {
            get { return GetSecurityContext(); }
        }

        protected GridOptions GridOptions
        {
            get { return GetGridOptions(); }
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

        protected IActionResult OkReadResult(object data)
        {
            var result = (data != null)
                ? Ok(data)
                : NotFound() as IActionResult;

            return result;
        }

        protected string GetAcceptLanguages()
        {
            var acceptLanguages = "fa-IR,fa";
            var header = Request.Headers["Accept-Language"];
            if (!String.IsNullOrEmpty(header))
            {
                acceptLanguages = header;
            }

            return acceptLanguages;
        }

        private SecurityContext GetSecurityContext()
        {
            var context = Request.Headers[AppConstants.ContextHeaderName];
            if (String.IsNullOrEmpty(context))
            {
                return null;
            }

            var json = Encoding.UTF8.GetString(Transform.FromBase64String(context));
            return JsonHelper.To<SecurityContext>(json);
        }

        private GridOptions GetGridOptions()
        {
            var options = Request.Headers[AppConstants.GridOptionsHeaderName];
            if (String.IsNullOrEmpty(options))
            {
                return null;
            }

            var urlEncoded = Encoding.UTF8.GetString(Transform.FromBase64String(options));
            var json = WebUtility.UrlDecode(urlEncoded);
            return JsonHelper.To<GridOptions>(json);
        }

        protected IStringLocalizer<AppStrings> _strings;
    }
}