﻿using System;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using SPPC.Framework.Common;
using SPPC.Framework.Helpers;
using SPPC.Framework.Presentation;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.Values;
using SPPC.Tadbir.ViewModel.Auth;
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

        protected UserAccessViewModel UserAccess
        {
            get { return GetUserAccess(); }
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

        private UserAccessViewModel GetUserAccess()
        {
            var context = GetSecurityContext();
            var userAccess = new UserAccessViewModel() { Id = context.User.Id };
            Array.ForEach(context.User.Roles.ToArray(), role => userAccess.Roles.Add(role));
            return userAccess;
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